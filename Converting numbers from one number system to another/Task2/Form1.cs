using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp3_2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        int len;
        int[] myData;


//функция генерации случайных данных
        private int[] DataInput (int lenData, out int[] mas)
        {
            mas = new int[len];
            Random num = new Random();
            for (int i=0;i<len;i++)
            {
                mas[i] = num.Next(20);
            }
            return mas;
        }


//функция вычисления суммы сгенерированных значений
        private void summa(int[] Data, int l, out int result)
        {
            result = 0;
            for(int i=0;i<l;i++)
            {
                result += Data[i];
            }
        }


//функция изменения значения массива со сгенерированными значениями
        private void Change(ref int[] RefData)
        {
            int l = RefData.GetUpperBound(0);
            for(int i=0;i<=l;i++)
            {
                RefData[i] = 5;
            }
        }


//функция нахождения минимального значения из сгенерированных значений
        private int minVal(params int[] numbers)
        {
            int min;
            min = numbers[0];
            for(int i = 1;i<numbers.Length;i++)
            
                if (numbers[i] < min) min = numbers[i];
                return min;
            
        }


//кнопка Данные генерирует количество чисел, указанное в окне выше(textbox1) либо меняет список изначально сгенерированных чисел на другие и выводит их в окне textbox2
        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "") { MessageBox.Show("Введите число элементов!"); }
            else
            {
                len = Convert.ToInt32(textBox1.Text);
                myData = new int[len];
                Random num = new Random();
                for(int i=0;i<len;i++)
                {
                    myData[i] = num.Next(20);
                }
            }
            string value = "";
            for(int i=0;i<len;i++)
            {
                value += myData[i].ToString() + " ";
            }
            textBox2.Text = value + "\r\n" + "\r\n";
        }


//кнопка Сумма вычисляет сумму сгенерированных чисел и выводит ее в окне label1
        private void button3_Click(object sender, EventArgs e)
        {
            int result;
            summa(myData, len, out result);
            label1.Text = "sum= " + result.ToString();
        }


//кнопка PARAMS определяет минимальное число из сгенерированных и выводит его в окне textbox2
        private void button5_Click(object sender, EventArgs e)
        {
            Change(ref myData);
            string value = "";
            for(int i =0;i<len;i++)
            {
                value += myData[i].ToString() + " ";
            }
            textBox2.Text += textBox2.Text + "Измененный массив: " + "\r\n";
            textBox2.Text += value;
        }


//кнопка REF меняет сгенерированные числа на другие и выводит их в окне textbox2
        private void button4_Click(object sender, EventArgs e)
        {
            int a = 5; int b = 10;
            int minNum = minVal(a, b);
            textBox2.Text += "Из значений а=5 и b=10 минимум равен: " + minNum.ToString() + "\r\n" + "\r\n";
            minNum = minVal(myData);
            textBox2.Text += "Из значений масиива минимум равен: " + minNum.ToString() + "\r\n" + "\r\n";
        }


//кнопка "Ввод данных" генерирует количество чисел, указанное в окне выше(textbox1) либо меняет список изначально сгенерированных чисел на другие и выводит их в окне textbox2
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Введите число элементов");
            }
            else
            {
                len = Convert.ToInt32(textBox1.Text);
                DataInput(len, out myData);
                string value = "";
                for (int i = 0; i<len;i++)
                {
                    value += myData[i].ToString() + " ";
                }
                textBox2.Text = value + "\r\n" + "\r\n";
            }
        }
    }
}
