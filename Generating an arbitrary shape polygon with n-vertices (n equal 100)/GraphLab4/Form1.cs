using System;
using System.Drawing;
using System.Windows.Forms;

namespace GraphLab4 {
    public partial class Form1 : Form {
        private readonly Graphics graphics;
        private readonly Pen pen = new Pen(Color.DarkRed, 2);
        private readonly Random random = new Random();
        private int vertexCount;


        //инициализация формы
        public Form1() {
            InitializeComponent();
            pictureBox.BackColor = Color.White;
            graphics = Graphics.FromHwnd(pictureBox.Handle);
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
        }


        //кнопка генерации полигонов
        private void GenerateButtonClick(object sender, EventArgs e) {
            graphics.Clear(Color.White);
            vertexCount = (int)vertexCountNumericUpDown.Value;
            PointF[] points = GeneratePolygonVertecies();
            graphics.DrawPolygon(pen, points);
        }


        //функция генерации вершин полигона
        public PointF[] GeneratePolygonVertecies() {
            double[] radii = new double[vertexCount];
            const double minRadiusWeight = 0.5;//минимально возможное значение радиуса
            const double maxRadiusWeight = 1.0;//максимально возможное значение радиуса

            double[] angleWeights = new double[vertexCount];
            const double minAngleWeight = 1.0;//минимально возможное значение радиуса
            const double maxAngleWeight = 10.0;//максимально возможное значение радиуса
            double totalAngleWeight = 0;//сумма углов полигона

            for (int i = 0; i < vertexCount; i++) {
                radii[i] = NextDouble(minRadiusWeight, maxRadiusWeight);//генерация случайного значения радиуса
                angleWeights[i] = NextDouble(minAngleWeight, maxAngleWeight);//генерация случайного значения угла
                totalAngleWeight += angleWeights[i];
            }

            double[] angles = new double[vertexCount];
            for (int i = 0; i < vertexCount; i++) {
                angles[i] = ToRadians(angleWeights[i], totalAngleWeight);//перевод угла в радианы
            }

            PointF[] points = new PointF[vertexCount];
            float x = pictureBox.Width / 2;
            float y = pictureBox.Height / 2;
            double theta = 0;
            for (int i = 0; i < vertexCount; i++) {
                points[i] = new PointF(
                    x + (int)(x * radii[i] * Math.Cos(theta)),
                    y + (int)(y * radii[i] * Math.Sin(theta)));
                theta += angles[i];
            }
            return points;
        }


        //функция генерации случайных чисел
        public double NextDouble(double a, double b) {
            double x = random.NextDouble();
            return x * a + (1 - x) * b;
        }


        //перевод углов в радианы
        private double ToRadians(double angleWeight, double totalAngleWeight) {
            return angleWeight * 2 * Math.PI / totalAngleWeight;
        }
    }
}
