using System;
using System.Windows.Forms;
using System.IO;

namespace SZ40
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

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
                for (int i = this.countSymbols; i < this.textBox1.Text.Length; ++i)
                {
                    teletype.Input(textBox1.Text[i]); // Ввод символа в телетайп.
                }

                textBox1.Text = teletype.GetText(); // Присваиваем строку из телетайпа в текст бокс.
                textBox1.Select(textBox1.Text.Length, 0); // Установка курсора в конец.
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

        private bool isKeyDown;

        private int countSymbols = 0;

        private Teletype teletype = new Teletype();

        /// <summary>
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

            string[] lines = { maskedTextBox1.Text, maskedTextBox2.Text, textBox1.Text }; // Масив содержимого maskedTextBox1 maskedTextBox2 и textBox1

            File.WriteAllLines(path, lines);  // Записывает в файл.
        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void maskedTextBox2_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        /// <summary>
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

            using (StreamReader sr = new StreamReader(path)) // Считывает ключ
            {
                maskedTextBox1.Text = sr.ReadLine();
                maskedTextBox2.Text = sr.ReadLine();

                while (sr.Peek() >= 0)
                    textBox1.Text += sr.ReadLine() + " ";
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        
        private void label2_Click(object sender, EventArgs e)
        {
            maskedTextBox2.Text = "";
            Random random = new Random();

            for (int i = 0; i < 6; ++i)
            {
                maskedTextBox2.Text += random.Next(4);
                maskedTextBox2.Text += random.Next(10);
            }
        }
    }

    public partial class Form2 : Form
    {
    }
}