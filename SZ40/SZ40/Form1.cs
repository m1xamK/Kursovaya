using System;
using System.Windows.Forms;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace SZ40
{
    public partial class SZ40 : Form
    {
        public SZ40()
        {
            InitializeComponent();

            numericUpDownArr = new NumericUpDown[] {
            numericUpDown1, numericUpDown2, numericUpDown3, numericUpDown4, numericUpDown5, numericUpDown6,
            numericUpDown7, numericUpDown8, numericUpDown9, numericUpDown10, numericUpDown11, numericUpDown12, };

            teletype = new Teletype();
        }

        private bool isKeyDown;

        private int countSymbols = 0;

        private bool isFirstChange = true;  // чтобы знать когда создавать нужные объекты (которые ниже), считывать из файла и прочее.

        private Teletype teletype;

        private NumericUpDown[] numericUpDownArr;

        private Rotors.Mashine mashine;       
        
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

                if (maskedTextBox1.Text.Length != 5)
                {
                    MessageBox.Show("Не верный долговременный ключ", "Ошибка!");
                    textBox1.Text = "";
                    return;
                }

                // Блокировка ключей, вывод идентификатора в textBox2, создаем телетайп, машину
                if (isFirstChange)
                {
                    isFirstChange = false;

                    string path = "../../../Files/KEYS/";

                    maskedTextBox1.ReadOnly = true;
					//checkBox1.ReadOnly = true;

                    int[] startPos = new int[12];
                    int j = 0;
                    foreach (NumericUpDown numericUpDown in numericUpDownArr)
                    {
                        numericUpDown.Enabled = false;

                        startPos[j] = (int)numericUpDown.Value;
                        ++j;
                    }
                    
                    try
                    {
                        mashine = new Rotors.Mashine(path, maskedTextBox1.Text, startPos);
                    }
                    catch (FileNotFoundException)
                    {
                        MessageBox.Show("Долговременный ключ с таким номером отсутствует", "Ошибка!");
                        Reset();
                        return;
                    }
                }

				if (!checkBox1.Checked)				
				{
					// Ввод символа в телетайп.
					for (int i = this.countSymbols; i < this.textBox1.Text.Length; ++i)
					{
                        if (textBox1.Text[i] == ' ') // пропускаем пробелы
                            continue;

						teletype.Input(textBox1.Text[i]);
					}

					// Присваиваем строку из телетайпа в текст бокс.
					textBox1.Text = teletype.GetText();
				}
                                
                // Ввод символа в textBox2.
                for (int i = this.countSymbols; i < this.textBox1.Text.Length; ++i)
                {
                    if (textBox1.Text[i] == ' ')
                        continue;

					var ch = mashine.GetNextSymbol(textBox1.Text[i]).ToString();
					textBox2.Text += ch;
                }

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
                
                StartRotatesPosition startRotatesPosition = new StartRotatesPosition(sr.ReadLine());
                int[] startPositions = startRotatesPosition.GetStartPositions();
                for (int i = 0; i < 12; i++)
                    numericUpDownArr[i].Value = startPositions[i];

                while (sr.Peek() >= 0)
                    textBox1.Text += sr.ReadLine();
                                
                // чтобы сразу началось шифроваться
                isKeyDown = true;
                textBox1_TextChanged(textBox1, e);                
            }            
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
                decimal number = Convert.ToDecimal(random.Next(1, max));
                numericUpDown.Value = number;
            }
        }


        /// <summary>
        /// Save button_1.
        /// Сохраняет долговременный ключ, сдвиги и текст из textBox1 в файл.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveButton1_Click(object sender, EventArgs e)
        {
            SaveFromTextBoxToFile(textBox1);
        }

        /// <summary>
        /// Save button_2.
        /// Сохраняет долговременный ключ, сдвиги и текст из textBox2 в файл.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveButton2_Click(object sender, EventArgs e)
        {
            SaveFromTextBoxToFile(textBox2);
        }

        /// <summary>
        /// Save button.
        /// Сохраняет долговременный ключ, сдвиги и текст из textBox в файл.
        /// </summary>
        /// <param name="textBox">ТексБокс содержимое которого сохраняем</param>        
        private void SaveFromTextBoxToFile(TextBox textBox)
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

            // Масив содержимого maskedTextBox1 NumericUpDown[] и textBox
            string[] lines = { maskedTextBox1.Text, startPosition.ToString(), textBox.Text };

            File.WriteAllLines(path, lines);  // Записывает в файл.
        }

        // MENU
        
        /// Кнопка "сбросить"
        private void resetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reset();
        }

        // Кнопка очищения форм (в меню)
        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reset();

            maskedTextBox1.Text = "";
            foreach (NumericUpDown numericUpDown in numericUpDownArr)
                numericUpDown.Value = 1;
        }

        // Кнопка выхода (в меню)
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// делает доступными все поля формы.
        /// </summary>
        private void Reset()
        {
            isFirstChange = true;
            maskedTextBox1.ReadOnly = false;
            foreach (NumericUpDown numericUpDown in numericUpDownArr)
                numericUpDown.Enabled = true;

            textBox1.Text = "";            
            textBox2.Text = "";

            countSymbols = 0;
            teletype = new Teletype();
        }

		private void button3_Click(object sender, EventArgs e)
		{
			string str = Translator.Translit.GetTranslit(textBox2.Text);
			MessageBox.Show(str, "Переведенный текст");
		}
    }

}