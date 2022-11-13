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
    public partial class Form1 : Form
    {


        //инициализация элементов textbox формы 
        public Form1()
        {
            InitializeComponent();
            textBox1.MaxLength = 5;
            textBox2.MaxLength = 5;
            textBox3.MaxLength = 15;
        }


        //функция перевода числа из одной системы счисления в другую
        public void perevod(List<int> number, int p, int q) //основная ф-ция
        {
            textBox4.Text += "Значение разрядов правильные!\r\n";
            
            double sum_razr = 0;

            for (int i = number.Count - 1; i >= 0; i--)
                sum_razr += Convert.ToInt32(number[i] * Math.Pow(p, number.Count - i - 1));

            textBox4.Text += $"Введенное число равно: {sum_razr}\r\n";

            string answer = "";
            bool cont = true;

            while (cont)
            {
                if (sum_razr < q)
                {                   
                    answer = $"{sum_razr} {answer}";
                    cont = false;
                }
                else
                {
                    answer = $"{sum_razr % q} {answer}";
                    sum_razr = Math.Truncate(sum_razr / q);
                }
            }

            textBox4.Text += $"Разряды переведенного числа: {answer}";
            textBox4.Text += "\r\n";
        }


        private void button4_Click(object sender, EventArgs e)
        {


            //проверка полей textbox на наличие в них значений
            if (!textBox1.Text.Equals("") && !textBox2.Text.Equals("") && !textBox3.Text.Equals(""))
            {
                bool stop = false;//для фиксации ошибок в записи разрядов
                int p = 0, q = 0;//Основания систем счисления
                List<int> number = new List<int>();


                //проверка формата вводимых данных
                //если правильно, то
                try
                {
                    p = Int32.Parse(textBox1.Text);//перевод значения основания из строкового представления в 32-битную
                    q = Int32.Parse(textBox2.Text);//перевод значения нового основания из строкового представления в 32-битную

                    if (textBox3.Text.EndsWith(" "))
                        textBox3.Text = textBox3.Text.Substring(0, textBox3.Text.Length - 1);

                    if (textBox1.Text.EndsWith(" "))
                        textBox1.Text = textBox1.Text.Substring(0, textBox1.Text.Length - 1);

                    if (textBox2.Text.EndsWith(" "))
                        textBox2.Text = textBox2.Text.Substring(0, textBox2.Text.Length - 1);

                    string[] tmp = textBox3.Text.Split(Convert.ToChar(" "));


                    //перевести строковую представление числа в эквивалентное ему 32-битовое целое число со знаком
                    foreach (string i in tmp)
                    {
                        if (i != " ")
                            number.Add(Int32.Parse(i));
                    }
                }
                //если неправильно, то
                catch
                {
                    textBox4.Text += "Вводимые данные должны быть цифрами!\r\n";
                    stop = true;
                }


                if (!stop)
                {


                    //значения оснований систем счисления должны находится в пределах от 2 до 10000
                    if ((2 > p | p > 10000) | (2 > q | q > 10000))
                    {
                        textBox4.Text += "Неверные значения! См. Условие!\r\n";
                        stop = true;
                    }

                    if (!stop)
                    {


                        //сравнение каждого разряда числа со значением первой системы счисления. Если хоть один разряд больше значения первой системы счисления, вместо перевода выводится сообщение об ошибке
                        for (int i = 0; i < number.Count; i++)
                        {
                            if (number[i] > p)
                            {
                                textBox4.Text += "Значение разряда превосходит основание!\r\n";
                                textBox4.Text += $"Номер разряда: {i + 1}\r\n";
                                stop = true;
                                break;
                            }
                        }

                        
                        //если каждыдй из разрядов меньше или равен значению первой системы счисления, то делается перевод в новую систему счисления
                        if (!stop)
                        {


                          //вызов функции перевода числа из одной системы счисления в другую
                          perevod(number, p, q);
                        }


                    }
                }
            }


            //если не все 3 необходимых поля заполнены, то об этом выводится сообщение
            else
            {
                textBox4.Text += "Не все поля заполнены!\r\n";
            }
        }


        //удаление всех значений и результатов из всех полей
        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
        }


        //при нажатии на кнопку Условие выводится сообщение ниже
        private void условиеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Основания системы счисления должны быть 2<=p(q)<=10000.\n\nЗначения для перевода вводятся через пробел.");
        }


        //выход из программы
        private void выходToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
