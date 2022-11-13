//Со служебным словом using описываются модули, используемые в проекте
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1//пространством имен namespace
{
    public partial class Form1 : Form//Объявление класса
    {
        

        //при запуске программы удаляется файл data.txt с предыдущимими результатами
        public Form1()//Получение экземпляра класса – нашей формы
        {
            InitializeComponent();
            System.IO.File.Delete("data.txt");
        }


        //Обработчик события – нажатие на кнопку «Вычислить»
        private void button1_Click(object sender, EventArgs e)
        {
            textBox3.Text = "";
            double sum = 0.0;//Переменная для значения суммы (функции)
            double eps = 0.1;//Переменная для значения точности
            double aStart = 0.0, aEnd = 0.0;//Левая и правая границы аргумента 
            
            
            //все другие необходимые объявления
            double x = 0.0;
            int n = 0;
            double step;
            double ch;


            string val;//Имя строки для вывода результата

            try
            {


                //Создание файла Out.txt для вывода
                StreamWriter outFile = new StreamWriter("D:\\Мое IT резюме\\Github\\Для загрузки\\csharp-projects\\Lab1 Iterative calculation of infinite sums (series)\\data.txt", true); //Заменить путь в кавычках на свой
                aStart = 0.0; aEnd = 0.0;
                

                //проверка на правильность формата вводимых данных
                try
                {


                    //Чтение значений из элементов textBox с использованием метода Convert
                    aStart = Convert.ToDouble(textBox1.Text);
                    aEnd = Convert.ToDouble(textBox2.Text);


                    x = aStart;


                    //проверка на правильность вводимых данных(начальное значение должно быть меньше конечного) с автоматическим исправлением ошибки
                    if (aStart > aEnd) { double t; t = aStart; aStart = aEnd; aEnd = t; }


                    //Вычисление диапазона изменения аргумента
                    //Вычисление шага изменения аргумента
                    //инициализация значения аргумента
                    //цикл for для задания одного из 10 значений аргумента
                    step = (aEnd - aStart) / 10;


                    for (int j = 0; j <= 10; j++)
                    {


                        //Инициализация значения точности
                        //Цикл for для задания одного из 6 значений точности
                        eps = 0.1;


                        for (int i = 0; i <= 5; i++)
                        {


                            //Инициализация для вычислений
                            //члена ряда, суммы и числа итераций
                            sum = 0.0;
                            ch = 0.0;
                            n = 1;
                            ch = 1;


                            // Цикл while вычисления суммы при
                            //заданных аргументе и точности
                            while (Math.Abs(ch) >= eps)//Абсолютное значение члена ряда >= epsilon
                            {


                                //Вычисление члена ряда, суммы и числа итераций
                                ch = n * Math.Pow(x, n);
                                sum += ch;
                                n++;
                            }


                            //Формирование строки для вывода
                            val = "X: " + x.ToString() + " SUM: " + sum.ToString() + " EPS: " + eps.ToString() + " ITER: " + n.ToString() + "\r\n";


                            //Запись строки в TextBox
                            textBox3.Text = textBox3.Text + val;
                            val = x.ToString() + " " + sum.ToString() + " " + eps.ToString() + " " + n.ToString();//+ "\r\n";


                            //Запись строки в файл outFile
                            outFile.WriteLine(val);


                            //Изменение epsilon
                            eps = eps / 10;
                        }


                        //Изменение аргумента
                        x += step;
                    }
                    outFile.Close();
                }


                //если начальное и конечное значения введены неправильно, то выводим надпись "Неверный формат ввода"
                catch { textBox3.Text = "Неверный формат ввода"; }
                // Application.Restart() ; } 
                outFile.Close();//Так как файл в программе создается,
                                //перед каждым запуском его нужно удалять
            }
            catch { textBox3.Text = "Файл занят"; }

        }


        //Обработчик события нажатия на кнопку "Чтение из файла"
        //Создается для того, чтобы научиться читать данные из файла
        private void button2_Click(object sender, EventArgs e)
        {
            string strLine;
            try
            {
                StreamReader sr = new StreamReader("D:\\Мое IT резюме\\Github\\Для загрузки\\csharp-projects\\Lab1 Iterative calculation of infinite sums (series)\\data.txt"); //Заменить путь в кавычках на свой
                strLine = sr.ReadLine();
                textBox3.Text = "Чтение из файла" + "\r\n";
                while (strLine != null)
                {
                    textBox3.Text = textBox3.Text + strLine + "\r\n";
                    strLine = sr.ReadLine();
                }
                sr.Close();
            }
            catch { textBox3.Text = "Файл не найден"; }

        }


        //Обработчик события нажатия на кнопку "Выход"
        //Завершение работы проекта
        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
