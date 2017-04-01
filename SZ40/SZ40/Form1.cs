using System;
using System.Windows.Forms;
using System.IO;
using System.Text;

namespace SZ40
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            numericUpDownArr = new NumericUpDown[] {
            numericUpDown1, numericUpDown2, numericUpDown3, numericUpDown4, numericUpDown5, numericUpDown6,
            numericUpDown7, numericUpDown8, numericUpDown9, numericUpDown10, numericUpDown11, numericUpDown12, };
        }

        private bool isKeyDown;

        private int countSymbols = 0;

        private Teletype teletype = new Teletype();

        private NumericUpDown[] numericUpDownArr;
        
        /// <summary>
        /// Этот метод берет последний символ из текст бокса и подает его на телетайп.
        /// P.S.: Если не использовать isKeyDown, то метод зацикливается, реагируя на свои же внесенные изменения. 
        /// А так только по нажатию клавиши.
        /// </summary>
        private void textBox1_TextChanged(object sender, EventArgs e)
        {        
            if (isKeyDown)
            {
                isKeyDown = false;

                // Ввод символа в телетайп.
                for (int i = this.countSymbols; i < this.textBox1.Text.Length; ++i)
                {
                    teletype.Input(textBox1.Text[i]); 
                }

                // Присваиваем строку из телетайпа в текст бокс.
                textBox1.Text = teletype.GetText();

                // Блокировка ключей, вывод идентификатора в textBox2.
                if (textBox1.Text.Length == 1)
                {
                    maskedTextBox1.ReadOnly = true;

                    foreach (NumericUpDown numericUpDown in numericUpDownArr)
                    {
                        numericUpDown.Enabled = false;
                        textBox2.Text += numericUpDown.Value.ToString();
                        textBox2.Text += ' ';
                    }
                }
                //textBox2.Text = teletype.GetText();

                // Установка курсора в конец.
                textBox1.Select(textBox1.Text.Length, 0);
                this.countSymbols = this.textBox1.Text.Length;                    
            }

           
        }

        /// <summary>
        /// Реакция на нажатие клавиши.
        /// </summary>
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            this.isKeyDown = true;
        }

        /// <summary>
        /// Load Button
        /// Загружает долговременный ключ, сдвиги колес и текст из файла
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)  // Load 1
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.ShowDialog(); // Открывает диалоговое окно

            string path = openFileDialog.FileName; // Сохраняет путь выбранного файла
            if (path == "")
                return;

            using (StreamReader sr = new StreamReader(path)) 
            {
                maskedTextBox1.Text = sr.ReadLine();    // Считывает долговременный ключ
                //maskedTextBox2.Text = sr.ReadLine();

                StartRotatesPosition startRotatesPosition = new StartRotatesPosition(sr.ReadLine());
                int[] startPositions = startRotatesPosition.GetStartPositions();
                for (int i = 0; i < 12; ++i)
                    numericUpDownArr[i].Value = startPositions[i];

                while (sr.Peek() >= 0)
                    textBox1.Text += sr.ReadLine() + " ";
            }
        }


        /// <summary>
        /// Save button.
        /// Сохраняет долговременный ключ, сдвиги и текст из textBox1 в файл.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.DefaultExt = ".txt"; // Расширение по умолчанию
            saveFileDialog.ShowDialog(); // Открывает диалоговое окно

            string path = saveFileDialog.FileName; // Сохраняет путь выбранного файла            
            if (path == "")
                return;

            StringBuilder startPosition = new StringBuilder();
            foreach (NumericUpDown numericUpDown in numericUpDownArr)
            {                
                startPosition.Append(numericUpDown.Value.ToString());
                startPosition.Append(" ");
            }

            string[] lines = { maskedTextBox1.Text, startPosition.ToString(), textBox1.Text }; // Масив содержимого maskedTextBox1 NumericUpDown[] и textBox1

            File.WriteAllLines(path, lines);  // Записывает в файл.
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        
        /// <summary>
        /// Reset button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void resetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            maskedTextBox1.ReadOnly = false;
            foreach (NumericUpDown numericUpDown in numericUpDownArr)
                numericUpDown.Enabled = true;

            textBox1.Text = "";
            textBox2.Text = "";
            teletype = new Teletype();
        }

        /// <summary>
        /// Генерируем случайные начальные положения.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label2_Click(object sender, EventArgs e)
        {
            // Запрет на случайную генерацию, если есть текст.
            if (textBox1.Text.Length > 0)
                return;

            Random random = new Random();

            foreach (NumericUpDown numericUpDown in numericUpDownArr)
            {
                int max = Convert.ToInt32(numericUpDown.Maximum);
                decimal number = Convert.ToDecimal(random.Next(max));
                numericUpDown.Value = number;
            }
        }

    }

}