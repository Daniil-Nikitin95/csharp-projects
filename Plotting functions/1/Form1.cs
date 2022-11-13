using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace _1
{
    public partial class Form1 : Form
    {
        public int NPoints = 6;
        int index_pix = 20;//Отступ нужен для помещения надписей по осям
        public float x_max = 0.1f;//Значенния x_max и x_min заданы в условии задания
        public float x_min = 0.00001f;
        int N_step_grid_x = 7;//Чтобы было 6 вертик. линий в сетке по условию
        float step_grid_x;
        public int step_grid_x_pix;//шаг изменения значений аргумента в пикселах
        public int step_grid_y_pix;//шаг изменения значений функции в пикселах
        int M_x = 365;
        int M_y = 290;
        int x_point_end_pix;
        int y_max = 20;//Значения функции y_max и y_min. Для функции примера y_max=5
        //int y_min = 1;//y_max и y_min определяются заданием к лабораторной работе
        double[] arg = new double[11];
        double[] fun = new double[66];
        double[] eps = new double[66];
        int[] itern = new int[66];
        private bool button1WasClicked = false;
        private bool flag = false;


        //Инициализация формы, установка ее параметров и создание таблицы
        public Form1()
        {
            InitializeComponent();
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            dataGridView1.ColumnCount = 4;
            dataGridView1.ColumnHeadersVisible = true;
            dataGridView1.AllowUserToAddRows = false;


            //данные для построения графика
            dataGridView1.Columns[0].Name = "X";//Значение аргумента
            dataGridView1.Columns[1].Name = "Value";//Значение функции
            dataGridView1.Columns[2].Name = "Eps";//Значение точности
            dataGridView1.Columns[3].Name = "Iteration Count";//Значение числа итераций
            pictureBox1.Paint += new PaintEventHandler(pictureBox1_Paint);
        }


        //кнопка для чтения data.txt и записи полученных данных в таблицу
        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
            string[,] arr = new string[60, 4];
            string path = @"C:\Users\Daniil\Documents\data.txt";
            int line_num = 0;
            using (StreamReader sr = new StreamReader(path))
            {
                string line;
                string[] splitted_line = new string[4];
                while ((line = sr.ReadLine()) != null)
                {
                    if (line != "")
                    {
                        splitted_line = line.Split(' ');
                        for (int i = 0; i < splitted_line.Length; i++)
                        {
                            arr[line_num, i] = splitted_line[i] + " ";

                            if (i == splitted_line.Length - 1)
                            {
                                arr[line_num, i] += "\r\n";
                            }
                        }
                        line_num++;
                    }
                }
            }
            var rows = this.dataGridView1.Rows;
            for (int i = 0; i < 60; i++)
            {
                DataGridViewRow new_row = new DataGridViewRow();
                for (int j = 0; j < 4; j++)
                {
                    DataGridViewCell value = new DataGridViewTextBoxCell();
                    value.Value = arr[i, j];
                    new_row.Cells.Add(value);
                }
                dataGridView1.Rows.Add(new_row);
            }

            for (int i = 0, j = 5; (i < 10) && (j < 60); i++, j+=6)
            {
                    arg[i] = Convert.ToDouble(dataGridView1.Rows[j].Cells[0].Value.ToString());
                    comboBox1.Items.Add(arg[i]);
            }

            for (int i = 0; i < 60; i++)
            {
                fun[i] = Convert.ToDouble(dataGridView1.Rows[i].Cells[1].Value.ToString());
                eps[i] = Convert.ToDouble(dataGridView1.Rows[i].Cells[2].Value.ToString());
                itern[i] = Convert.ToInt32(dataGridView1.Rows[i].Cells[3].Value.ToString());
            }
        }


        //рисование осей координат, сетки, точек и линий
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {


            //Создаем свой экземпляр класса Graphics
            Graphics myGraphics = e.Graphics;
            //Можно создать экземпляр Graphics иначе (см. следующий оператор)
            //Graphics myGraphics = pictureBox1.CreateGraphics();
            
            
            //определяем положение осей координат в пикселях
            int ox_pix = index_pix;//координата x начала координат
            int oy_pix = pictureBox1.Height - index_pix;//коорд. y начала координат
            float x_point_end;//Объявление конечной точки по оси x
            x_point_end = 1.0f;//Значение конечной точки по оси x по условию


            //Вычисление значения конечной точки оси x в пикселах c использованием
            // масштабного коэффициента и с явным преобразованием типа float в тип int
            x_point_end_pix = (int)x_point_end * M_x - index_pix;


            //Выбираем зеленое перо толщиной 2;
            Pen greenPen_x = new Pen(Color.Green, 2);


            //Задаем координаты двух граничных точек оси:
            Point point1 = new Point(ox_pix, oy_pix);
            Point point2 = new Point(x_point_end_pix, oy_pix);


            //Строим линию через две заданные граничные точки:
            myGraphics.DrawLine(greenPen_x, point1, point2);


            //Для нашей задачи число горизонтальных линий в сетке равно y_max
            int N_step_grid_y = y_max;


            //Шаг сетки в направлении оси "y" (высота всей сетки равна 1 единице)
            float step_grid_y;//шаг изменения значений функции
            int step_grid_y_pix;//шаг изменения значений функции в пикселах
            step_grid_y = 1.0f / N_step_grid_y;//значение шага по оси y
            step_grid_y_pix = (int)(step_grid_y * M_y);//шага по оси y в пикселах
            
            
            //Выбираем красное перо толщиной 1:
            Pen redPen = new Pen(Color.Red, 1);


            //Строим в цикле горизонтальные линии сетки от нулевой линии (оси х) вверх:
            //Для этого определяем координаты граничных точек сетки
            int j_y;//счетчик для цикла
            int y1_pix = oy_pix;
            for (j_y = 1; j_y <= N_step_grid_y; j_y++)
            {
                y1_pix = y1_pix - (int)step_grid_y_pix;


                //Задаем координаты двух граничных точек линии сетки:
                Point point3 = new Point(ox_pix, y1_pix);// левая точка
                Point point4 = new Point(x_point_end_pix, y1_pix);//правая точка


                //Строим прямую линию через две заданные точки:
                myGraphics.DrawLine(redPen, point3, point4);
            }


            //Строим ось ординат "oy" от y = 0 до y = 1;
            //Объявляем и задаем ординату последней точки оси ординат "y" при y = 1:
            float y_point_end_pix;
            y_point_end_pix = oy_pix;


            //Выбираем зеленое перо толщиной 2;
            Pen greenPen = new Pen(Color.Green, 2);


            //Задаем координаты двух граничных точек оси y:
            Point point5 = new Point(ox_pix, index_pix);//Верхн. тчк в pictureBox
            Point point6 = new Point(ox_pix, (int)y_point_end_pix);//нижняя


            //Строим линию через две заданные граничные точки:
            myGraphics.DrawLine(greenPen, point5, point6);


            //Строим вертикальные линии сетки от оси y вправо
            int j_x; //float x1;
            int x1_pix;
            step_grid_x = (float)(1.0f / N_step_grid_x);//шаг измен. знач. аргум.
            step_grid_x_pix = (int)(step_grid_x * M_x);//шаг измен. в пикселах
            x1_pix = ox_pix;
            for (j_x = 1; j_x < N_step_grid_x; j_x++)
            {
                x1_pix = x1_pix + (int)step_grid_x_pix;


                //Задаем координаты двух граничных точек линии сетки:
                Point point7 = new Point(x1_pix, index_pix);// левая точка
                Point point8 = new Point(x1_pix, (int)y_point_end_pix);//правая


                //Строим прямую линию через две заданные точки:
                myGraphics.DrawLine(redPen, point7, point8);
            }


            //Записываем числа по осям координат, пользуясь функцией DrawString(msg, . . .) :
            //Объявляем локальные переменные:
            int n; float p1 = 1.0f; float p2; string msg;


            //Записываем числа по оси "+oy":
            for (n = 0; n <= N_step_grid_y - 1; n++)
            {


                //p2 = p1 - n * 0.1F; эта формула выводит знач. в виде беск. дроби, поэтому
                // умножаем вычисленное значение на 100.0f, округляем его с помощью
                //функции Math.Round и делим результат на 100.0f
                //p2 = (float)(Math.Round((p1 - n * (float)1 / N_step_grid_y) * 10.0F)) / 10.0F;
                p2 = (float)(Math.Round((p1 - n * (float)1 / N_step_grid_y) * 100.0F)) / 100.0F;
                msg = Math.Round(p2 * N_step_grid_y).ToString();//Получаем значение
                myGraphics.DrawString(msg, this.Font, Brushes.Blue, ox_pix - 20, oy_pix - 285 + n * step_grid_y_pix);
            }


            //Записываем числа по оси "+ox":
            float kf = 1f;//Коэф. для вывода в удобном виде
            p2 = 1f;//Нач. значение для вывода нуля в начале координат

            for (n = 1; n <= 7; n++)
            {
                msg = p2.ToString();
                myGraphics.DrawString(msg, this.Font, Brushes.Blue, ox_pix - 65 + n * step_grid_x_pix, oy_pix + 5);


                //Чтобы без бесконечной дроби вывелись все значения по оси "ox",
                //умножать и делить приходится на 1000000.0f
                p2 = (float)(Math.Round((0.1F * kf) * 1000000.0f)) / 1000000.0f;
                kf = kf / 10.0f;
            }
            int nnn = 0;
            if (button1WasClicked && flag)
            {

                double xx_old = 0;
                int yy_old = 0;
                double xx = 0;
                int yy = 0;

                if (comboBox1.Text != "")
                {

                    double xxx = Convert.ToDouble(comboBox1.Text);
                    for (int i = 0; i <= 10; i++)
                    {
                        if (xxx == arg[i]) nnn = i;

                    }

                    double xxxx = 0;
                    int yyyy = 0;

                    int seg_x = (pictureBox1.Width - 163) / 6 - 2;
                    int seg_y = pictureBox1.Height / 20 - 2;

                    xx_old = 70;
                    yy_old = 0 + 28;
                    Pen PPen = new Pen(Color.Black, 6);
                    redPen = new Pen(Color.BlueViolet, 3);
                    for (int i = 0; i < 6; i++)
                    {
                        xx = 70 + seg_x * i;
                        yy = seg_y * (20 - (itern[nnn * 6 + i])) + 28;
                        xxxx = xx - 5;
                        yyyy = yy + 5;

                        if (i > 0)
                        {


                            //рисуем линию между точками
                            Point point10 = new Point((int)xx_old, yy_old);
                            Point point11 = new Point((int)xx, yy);
                            Point point12 = new Point((int)xxxx, yyyy);
                            myGraphics.DrawLine(PPen, point11, point12);
                            myGraphics.DrawLine(redPen, point10, point11);


                        }
                        else
                        {
                            Point point10 = new Point((int)xx, yy);
                            xxxx = xx - 5;
                            yyyy = yy + 5;
                            Point point11 = new Point((int)xxxx, yyyy);
                            myGraphics.DrawLine(PPen, point10, point11);
                        }
                        xx_old = xx;
                        yy_old = yy;
                    }

                    button1WasClicked = false;

                }

            }
        }


        //список значений на выбор для построения линии
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            pictureBox1.Invalidate();//Метод перерисовывает только "инвалидную" часть
        }


        //кнопка, запускающая построение графика(линии) на основе выбранного из списка значения
        private void button2_Click(object sender, EventArgs e)
        {
            button1WasClicked = true;
            int nnn = 0;
            if (comboBox1.Text != "")
            {
                textBox1.Text = "Значение аргумента: " + comboBox1.Text + "\n\r";
                double xxx = Convert.ToDouble(comboBox1.Text);
                for (int i = 0; i <= 10; i++)
                {
                    if (xxx == arg[i]) nnn = i;

                }
                textBox1.Text += "\n\rЧисло итераций для разных значений точности: ";
                for (int i = 0; i <= 5; i++)
                {
                    textBox1.Text += itern[nnn * 6 + i] + " ";
                }
                textBox1.Text += "\n\r";
                flag = true;
            }
            else textBox1.Text = "Выберите значение";
            pictureBox1.Invalidate();
        }
    }
}