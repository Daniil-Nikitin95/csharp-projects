using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace _1
{
    public partial class Form1 : Form
    {


        //объявление массивов для сортировки
        int[] Rand_array;
        int[] Bubble_array;
        int[] Select_array;
        int[] Insert_array;
        int[] Merge_array;

        public Form1()
        {


            //инициализация формы, элементов textbox и установка параметров для этих элементов
            InitializeComponent();

            textBox1.Multiline = true;
            textBox1.ScrollBars = ScrollBars.Vertical;
            textBox1.ReadOnly = true;
            textBox1.TabStop = false;

            textBox2.MaxLength = 6;

            textBox3.Multiline = true;
            textBox3.ScrollBars = ScrollBars.Vertical;
            textBox3.ReadOnly = true;
            textBox3.TabStop = false;

            textBox4.Multiline = true;
            textBox4.ScrollBars = ScrollBars.Vertical;
            textBox4.ReadOnly = true;
            textBox4.TabStop = false;

            textBox5.Multiline = true;
            textBox5.ScrollBars = ScrollBars.Vertical;
            textBox5.ReadOnly = true;
            textBox5.TabStop = false;

            textBox6.Multiline = true;
            textBox6.ScrollBars = ScrollBars.Vertical;
            textBox6.ReadOnly = true;
            textBox6.TabStop = false;

            textBox7.Multiline = true;

            textBox7.ReadOnly = true;
            textBox7.TabStop = false;

            textBox8.Multiline = true;
            textBox8.ReadOnly = true;
            textBox8.TabStop = false;

            textBox9.Multiline = true;
            textBox9.ReadOnly = true;
            textBox9.TabStop = false;


            //элемент формы, на котором, при наведении на него мыши, появляется текст
            label6.MouseEnter += (s, e) =>
            {
                label6.Text = "Реализовать 3 алгоритма сортировки массивов и исследовать\r\nзависимость врмени выполнения упорядочения от длины массива.\r\nИсходные соглашения:\r\n- задан целочисленный массив, содержащий n элементов;\r\n- массив заполняется случайными числами из интервала от -999 до 999;\r\n- выполняется упорядочение по возрастанию;\r\n- результаты работы алгоритмов подвергаются автоматической проверке.";
            };
            label6.MouseLeave += (s, e) =>
            {
                label6.Text = "";
            };
        }


        //функция генерации случайных чисел от -999 до 1000
        public void Random_generate(int len)
        {
            if (len == 0)
            {
                textBox1.Text = "Введите не пустую длину";
            }
            else
            {
                int number;
                Rand_array = new int[len];
                Bubble_array = new int[len];
                Select_array = new int[len];
                Insert_array = new int[len];
                Merge_array = new int[len];
                string out_string = "";


                //Описываем объект класса Random
                //с помощью оператора new создаем объект
                //и инициализируем его по умолчанию
                Random rnd = new Random();


                //В цикле заполняем массив случайными числами из
                //требуемого диапазона, используя метод Next
                for (int i = 0; i < len; i++)
                {
                    number = rnd.Next(-999, 1000);
                    Rand_array[i] = number;
                    Bubble_array[i] = number;
                    Select_array[i] = number;
                    Insert_array[i] = number;
                    Merge_array[i] = number;
                    if (checkBox1.Checked)
                    {
                        out_string += $"{Rand_array[i].ToString()}\r\n";
                    }
                }


                if (!checkBox1.Checked)
                {
                    out_string += "Готово";
                }

                textBox1.Text = out_string;

                //триггер запуска функций сортировки и тд
                //sort(Rand_array);
            }
        }


        //функция сортировки слиянием
        public int[] merge_sort(int[] arr4, int low, int high)
        {
            if (low < high)
            {
                var middle = (low + high) / 2;
                merge_sort(arr4, low, middle);
                merge_sort(arr4, middle + 1, high);
                merge(arr4, low, middle, high);
            }
            return arr4;
        }


        //подфункция функции сортировки слиянием
        public void merge(int[] arr4, int low, int middle, int high)
        {
            int left = low, right = middle + 1, index = 0;
            int[] tmp = new int[high - low + 1];

            while ((left <= middle) && (right <= high))
            {
                if (arr4[left] < arr4[right])
                {
                    tmp[index] = arr4[left];
                    left++;
                }
                else
                {
                    tmp[index] = arr4[right];
                    right++;
                }

                index++;
            }

            for (int i = left; i <= middle; i++)
            {
                tmp[index] = arr4[i];
                index++;
            }

            for (int i = right; i <= high; i++)
            {
                tmp[index] = arr4[i];
                index++;
            }

            for (int i = 0; i < tmp.Length; i++)
            {
                arr4[low + i] = tmp[i];
            }
        }


        //Проверка правильности сортировки
        public bool fun_check (int[] arr, int[] sorted_arr)
        {
            if (arr.Length != sorted_arr.Length)
            {
                return false;
            }

            int i = 0;


            //Цикл для проверки упорядоченности по возрастанию массива sorted_arr
            while (i < arr.Length - 2 && sorted_arr[i] <= sorted_arr[i + 1])
                i++;
            if (i != arr.Length - 2)
                return false;//Если упорядоченность не обнаружена

            int[] a = new int[1999];//Создаем массивы-счетчики значений
            int[] sa = new int[1999];


            //Следующий цикл не обязателен, т.к. при создании
            //массивы инициализирутся нулями.
            for (i = 0; i < a.Length; i++)
            {
                a[i] = 0;
                sa[i] = 0;
            }


            //Увеличиваем на 1 соответствующие значения
            //элементов массивов-счетчиков.
            //Массивы-счетчики нумеруются с 0
            for (i = 0; i < arr.Length; i++)
            {
                a[arr[i] + 999]++;
                sa[sorted_arr[i] + 999]++;
            }


            //Сравниваем попарно все значения массивов-счетчиков
            //Формируем возвращаемые логические значения-результаты
            for (i = 0; i < a.Length; i++)
            {
                if (a[i] != sa[i])
                    return false;
            }
            
            return true;
        }


        //окно для ввода количества элементов для сортировки
        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != Convert.ToChar(8))
            {
                e.Handled = true;
            }
        }


        //кнопка Очистить все поля
        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";

            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";

            textBox7.Text = "";
            textBox8.Text = "";
            textBox9.Text = "";
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }


        //кнопка для генерации случайных значений
        private void button1_Click_1(object sender, EventArgs e)
        {
            if (textBox2.Text != "")
            {


                //вызов функции для генерации случайных значений
                Random_generate(Int32.Parse(textBox2.Text));


            }
            else
            {
                Random_generate(0);
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                textBox6.Text = "";

                textBox7.Text = "";
                textBox8.Text = "";
                textBox9.Text = "";
            }
        }


        //кнопка вызова обменной сортировки
        private void button2_Click_1(object sender, EventArgs e)
        {
            int tmp = 0;
            string out_string = "", out_time = "", fout_time = "";

            DateTime start = DateTime.Now;

            for (int i = 0; i < Bubble_array.Length; i++)
            {
                for (int j = i + 1; j < Bubble_array.Length; j++)
                {
                    if (Bubble_array[i] > Bubble_array[j])
                    {
                        tmp = Bubble_array[i];
                        Bubble_array[i] = Bubble_array[j];
                        Bubble_array[j] = tmp;
                    }
                }

                if (i == 9 || i == 99 || i == 999 || i == 9999)
                {
                    out_time += $"{i + 1} - {(DateTime.Now - start).Milliseconds}\r\n";
                }
                if (i == 9 || i== 99 || i == 999 || i == 9999)
                {
                    fout_time = $"{(DateTime.Now - start).Milliseconds}\r\n";
                }
            }

            for (int i = 0; i < Bubble_array.Length; i++)
            {
                if (checkBox1.Checked)
                {
                    out_string += $"{Bubble_array[i]}\r\n";
                }
            }

            if (!checkBox1.Checked)
            {
                out_string += "Готово";
            }

            textBox3.Text = out_string;
            textBox7.Text = out_time;


            //проверка на правильность сортировки
            if (fun_check(Rand_array, Bubble_array))
            {
                label2.Text = "Сортировка верна\nДлительность = " + fout_time;
            }
            else
            {
                label2.Text = "Сортировка не верна";
            }
        }


        //вызов сортировки выбором
        private void button3_Click_1(object sender, EventArgs e)
        {
            int tmp = 0;
            string out_string = "", out_time = "", fout_time = "";

            DateTime start = DateTime.Now;

            for (int i = 0; i < Select_array.Length; i++)
            {
                int min = i;
                for (int j = i + 1; j < Select_array.Length; j++)
                {
                    if (Select_array[j] < Select_array[min])
                    {
                        min = j;
                    }
                }
                tmp = Select_array[min];
                Select_array[min] = Select_array[i];
                Select_array[i] = tmp;

                if (i == 9 || i == 99 || i == 999 || i == 9999)
                {
                    out_time += $"{i + 1} - {(DateTime.Now - start).Milliseconds}\r\n";
                }
                if (i == 9 || i == 99 || i == 999 || i == 9999)
                {
                    fout_time = $"{(DateTime.Now - start).Milliseconds}\r\n";
                }
            }

            for (int i = 0; i < Select_array.Length; i++)
            {
                if (checkBox1.Checked)
                {
                    out_string += $"{Select_array[i]}\r\n";
                }
            }
            if (!checkBox1.Checked)
            {
                out_string += "Готово";
            }

            textBox4.Text = out_string;
            textBox8.Text = out_time;


            //проверка на правильность сортировки
            if (fun_check(Rand_array, Select_array))
            {
                label3.Text = "Сортировка верна\nДлительность = " + fout_time;
            }
            else
            {
                label3.Text = "Сортировка не верна";
            }
        }


        //вызов сортировки вставками
        private void button6_Click(object sender, EventArgs e)
        {
            int tmp, location = 0;
            string out_string = "", out_time = "", fout_time = "";

            DateTime start = DateTime.Now;

            for (int i = 1; i < Insert_array.Length; i++)
            {
                tmp = Insert_array[i];
                location = i - 1;
                while (location >= 0 && Insert_array[location] > tmp)
                {
                    Insert_array[location + 1] = Insert_array[location];
                    location -= 1;
                }
                Insert_array[location + 1] = tmp;

                if (i == 9 || i == 99 || i == 999 || i == 9999)
                {
                    out_time += $"{i + 1} - {(DateTime.Now - start).Milliseconds}\r\n";
                }
                if (i == 9 || i == 99 || i == 999 || i == 9999)
                {
                    fout_time = $"{(DateTime.Now - start).Milliseconds}\r\n";
                }
                // баг со временем - не убирать
                Console.WriteLine((DateTime.Now - start).Milliseconds);
            }

            for (int i = 0; i < Insert_array.Length; i++)
            {
                if (checkBox1.Checked)
                {
                    out_string += $"{Insert_array[i]}\r\n";
                }
            }
            if (!checkBox1.Checked)
            {
                out_string += "Готово";
            }

            textBox5.Text = out_string;
            textBox9.Text = out_time;


            //проверка на правильность сортировки
            if (fun_check(Rand_array, Insert_array))
            {
                label4.Text = "Сортировка верна\nДлительность = " + fout_time;
            }
            else
            {
                label4.Text = "Сортировка не верна";
            }
        }


        //вызов сортировки слиянием
        private void button7_Click(object sender, EventArgs e)
        {
            string out_string = "", out_time = "", fout_time = "";
            DateTime start = DateTime.Now;

            merge_sort(Merge_array, 0, Merge_array.Length - 1);

            for (int i = 0; i < Merge_array.Length; i++)
            {
                if (i == 9 || i == 99 || i == 999 || i == 9999)
                {
                    out_time += $"{i + 1} - {(DateTime.Now - start).Milliseconds}\r\n";
                }
                if (i == 9 || i == 99 || i == 999 || i == 9999)
                {
                    fout_time = $"{(DateTime.Now - start).Milliseconds}\r\n";
                }
                if (checkBox1.Checked)
                {
                    out_string += $"{Merge_array[i]}\r\n";
                }
            }
            if (!checkBox1.Checked)
            {
                out_string += "Готово";
            }

            textBox6.Text = out_string;
            textBox10.Text = out_time;


            //проверка на правильность сортировки
            if (fun_check(Rand_array, Merge_array))
            {
                label5.Text = "Сортировка верна\nДлительность = " + fout_time;
            }
            else
            {
                label5.Text = "Сортировка не верна";
            }
        }


        //окно со спрятанным текстом. Текст появляется при наведении на него курсора
        private void label6_MouseEnter(object sender, EventArgs e)
        {

        }
    }
}
