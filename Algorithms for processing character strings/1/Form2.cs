using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _1
{ 
    public partial class Form2 : Form
    {
    Form1 main;//Создаем переменную, для хранения ссылки на первую форму
        public Form2(Form1 f1, string[] text)//Меняем конструктор – указываем параметр
                                             //- имя для ссылки на первую форму
        {
            InitializeComponent();

            main = f1;//переменной main присваиваем значение ссылки
            main_fun(text.Distinct().ToArray());
            word_counter();
        }

        public void main_fun(string[] text)
        {

            for (int i = 0; i < text.Length; i++)
            {
                listBox1.Items.Add(text[i] + "\n");
                checkedListBox1.Items.Add(text[i] + "\n");
            }

            List<string> list = new List<string>(text);
            list_to_textbox(list, true);
        }


        //функция для добавления новых слов в третье окно словаря
        public void list_to_textbox(List<string> list, bool is_first)
        {
            if (is_first)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    textBox1.Text += list[i].ToString() + "\r\n";
                }
            }
            else
            {
                textBox1.Text += list[0].ToString() + "\r\n";
            }
        }


        //функция подсчета слов в словаре или счетчик
        public void word_counter()
        {
            label1.Text = "Кол-во слов в\nсловаре: " + (textBox1.Text.Replace('\n', ' ').Replace('\r', ' ').Split(' ').Length / 2).ToString();
        }


        //выбор во втором окне словаря слова для подчеркивания его в тексте в окне первой формы
        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (checkedListBox1.CheckedItems.Count == 2)
            {
                for (int i = 0; i < checkedListBox1.Items.Count; i++)
                {
                    checkedListBox1.SetItemChecked(i, false);
                }
                checkedListBox1.SetItemChecked(checkedListBox1.SelectedIndex, true);
            }

            else if (checkedListBox1.CheckedItems.Count == 1)
            {
                main.last_thing(checkedListBox1.CheckedItems[0].ToString());
            }
        }


        //кнопка для добавления новых слов в третье окно словаря
        private void button1_Click(object sender, EventArgs e)
        {
            bool is_valid = true;

            string tmp = textBox1.Text.Replace('\n', ' ').Replace('\r', ' ');
            string[] arr = tmp.Split(' ');

            for (int i = 0; i < arr.Length; i++)
                if (arr[i] == textBox2.Text)
                    is_valid = false;

            if (is_valid)
            {
                if (textBox2.Text.Length != 0)
                {
                    List<string> tmp1 = new List<string>(0);
                    tmp1.Add(textBox2.Text);


                    //вызов функции для добавления новых слов в третье окно словаря
                    list_to_textbox(tmp1, false);
                }

                word_counter();
            }
        }
    }
}
