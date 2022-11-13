using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            label3.Text = $"Кол-во символов в подстроке: {textBox1.Text.Length.ToString()}";

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            main_fun(text_get());
        }

        public void main_fun(string text)
        {
            label1.Text += $"{text.Length.ToString()}";
        }


        //чтение файла text.txt
        public string text_get()
        {
            string text = "";
            string path = @"C:\Users\Daniil\Documents\text.txt"; //Заменить путь в кавычках на свой
            var srcEncoding = Encoding.UTF8;

            using (StreamReader sr = new StreamReader(path, encoding: srcEncoding))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    text += $"{line}\n";
                }
            }
            richTextBox1.Text = text;
            return text;
        }


        //функция для подчеркивания найденных при поиске слов красным цветом
        public void last_thing(string text)
        {
            richTextBox1.SelectionStart = 0;
            richTextBox1.SelectionLength = richTextBox1.Text.Length - 1;
            richTextBox1.SelectionColor = Color.Black;

            textBox1.Clear();
            label3.Text = "Кол-во символов в подстроке: ";
            label4.Text = "Кол-во вхождений строки: ";

            text = text.Substring(0, text.Length - 1);

            label6.Text = "Выбранное слово: " + text;

            Console.WriteLine(text[0]);

            string sub_str = text;
            int i = 0;
            int count = 0;

            while (i <= richTextBox1.Text.Length - sub_str.Length)
            {
                i = richTextBox1.Text.ToLower().IndexOf(sub_str.ToLower(), i);
                if (i < 0)
                    break;

                richTextBox1.SelectionStart = i;
                richTextBox1.SelectionLength = sub_str.Length;
                richTextBox1.SelectionColor = Color.Red;
                i += sub_str.Length;
                count++;
            }

            label7.Text = "Число вхождений слова:" + count.ToString();
        }


        //функция вывода количества символов в подстроке
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            label3.Text = $"Кол-во символов в подстроке: {textBox1.Text.Length.ToString()}";
        }


        //кнопка поиска слова в тексте
        private void button1_Click(object sender, EventArgs e)
        {
            label6.Text = "Выбранное слово: ";
            label7.Text = "Число вхождений слова:";

            bool is_empty = false;

            textBox1.Text = textBox1.Text.ToString().Replace(" ", "");

            if (textBox1.Text.Length == 0)
                is_empty = true;
            else
                is_empty = false;

            richTextBox1.SelectionStart = 0;
            richTextBox1.SelectionLength = richTextBox1.Text.Length - 1;
            richTextBox1.SelectionColor = Color.Black;

            if (!is_empty)
            {
                string sub_str = textBox1.Text;
                int i = 0;
                int count = 0;

                while (i <= richTextBox1.Text.Length - sub_str.Length)
                {
                    i = richTextBox1.Text.ToLower().IndexOf(sub_str.ToLower(), i);
                    if (i < 0)
                        break;

                    richTextBox1.SelectionStart = i;
                    richTextBox1.SelectionLength = sub_str.Length;
                    richTextBox1.SelectionColor = Color.Red;
                    i += sub_str.Length;
                    count++;
                }

                label4.Text = $"Кол-во вхождений строки: {count}";
            }
        }


        //кнопка перехода к словарю
        private void button2_Click(object sender, EventArgs e)
        {
            char[] sep = new char[] {',', '.', '\n'};

            string tmp = richTextBox1.Text.ToLower().ToString().Replace(",", "").Replace(".", "").Replace(":", "").Replace("\n", "");
            string[] text_arr = tmp.Split(' ');

            Form FormDict = new Form2(this, text_arr);
            FormDict.Show();
        }
    }
}
