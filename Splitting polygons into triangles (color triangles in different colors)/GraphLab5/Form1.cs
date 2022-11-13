using System;
using System.Drawing;
using System.Windows.Forms;

namespace GraphLab5 {
    public partial class Form1 : Form {
        private readonly Graphics graphics;
        private readonly Pen pen = new Pen(Color.Green, 2);
        private readonly Pen trianglePen = new Pen(Color.Black, 2);
        private readonly Random random = new Random();
        private PointF[] vertecies = new PointF[0];
        private Triangle[] triangles;
        private bool[] takenVertecies;
        private int vertexCount;


        //инициализация формы
        public Form1() {
            InitializeComponent();
            pictureBox.BackColor = Color.White;
            graphics = Graphics.FromHwnd(pictureBox.Handle);
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            trianglePen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            trianglePen.DashPattern = new float[] { 5, 3 };
        }


        //кнопка генерации полигонов
        private void GenerateButtonClick(object sender, EventArgs e) {
            graphics.Clear(Color.White);
            vertexCount = (int)vertexCountNumericUpDown.Value;
            vertecies = GeneratePolygonVertecies();
            graphics.DrawPolygon(pen, vertecies);
        }


        //функция генерации точек полигона
        public PointF[] GeneratePolygonVertecies() {
            double[] radii = new double[vertexCount];
            const double minRadiusWeight = 0.5;
            const double maxRadiusWeight = 1.0;

            double[] angleWeights = new double[vertexCount];
            const double minAngleWeight = 1.0;
            const double maxAngleWeight = 10.0;
            double totalAngleWeight = 0;

            for (int i = 0; i < vertexCount; i++) {
                radii[i] = NextDouble(minRadiusWeight, maxRadiusWeight);
                angleWeights[i] = NextDouble(minAngleWeight, maxAngleWeight);
                totalAngleWeight += angleWeights[i];
            }

            double[] angles = new double[vertexCount];
            for (int i = 0; i < vertexCount; i++) {
                angles[i] = ToRadians(angleWeights[i], totalAngleWeight);
            }

            PointF[] points = new PointF[vertexCount];
            float x = pictureBox.Width / 2;
            float y = pictureBox.Height / 2;
            double theta = 0;
            for (int i = 0; i < vertexCount; i++) {
                points[i] = new PointF(
                    x + (int)(x * radii[i] * Math.Cos(theta)),
                    y + (int)(y * radii[i] * Math.Sin(theta)));
                theta -= angles[i];
            }
            return points;
        }


        //функция генерации случайного числа двойной точности
        public double NextDouble(double a, double b) {
            double x = random.NextDouble();
            return x * a + (1 - x) * b;
        }


        //функция перевода угла в радианы
        private double ToRadians(double angleWeight, double totalAngleWeight) {
            return angleWeight * 2 * Math.PI / totalAngleWeight;
        }


        //кнопка разбиения полигона на треугольники
        private void TriangulateButtonClick(object sender, EventArgs e) {
            if (vertecies.Length > 0) {
                triangles = new Triangle[vertecies.Length - 2];
                takenVertecies = new bool[vertecies.Length];
                Triangulate();
                DrawTriangles();
            } else {
                MessageBox.Show("Сначала необходимо построить полигон");
            }
        }


        //функция разбиения полигона на треугольники
        private void Triangulate() {
            int trainPos = 0;
            int verteciesLeft = vertecies.Length;

            int ai = FindNextNotTakenVertex(0);
            int bi = FindNextNotTakenVertex(ai + 1);
            int ci = FindNextNotTakenVertex(bi + 1);

            int step = 0;


            //минимальное количество вершин для разбиения полигона на части - 4
            while (verteciesLeft > 3) {
                if (IsLeft(vertecies[ai], vertecies[bi], vertecies[ci]) && CanBuildTriangle(ai, bi, ci)) {
                    triangles[trainPos++] = new Triangle(vertecies[ai], vertecies[bi], vertecies[ci]);
                    takenVertecies[bi] = true;
                    verteciesLeft--;
                    bi = ci;
                    ci = FindNextNotTakenVertex(ci + 1);
                } else {
                    ai = FindNextNotTakenVertex(ai + 1);
                    bi = FindNextNotTakenVertex(ai + 1);
                    ci = FindNextNotTakenVertex(bi + 1);
                }

                if (step > vertecies.Length * vertecies.Length) {
                    triangles = null;
                    break;
                }

                step++;
            }


            if (triangles != null) {
                triangles[trainPos] = new Triangle(vertecies[ai], vertecies[bi], vertecies[ci]);
            }   
        }


        //функция поиска следующей не занятой вершины
        private int FindNextNotTakenVertex(int startPos) {
            startPos %= vertecies.Length;
            if (!takenVertecies[startPos])
                return startPos;

            int i = (startPos + 1) % vertecies.Length;
            while (i != startPos) {
                if (!takenVertecies[i])
                    return i;
                i = (i + 1) % vertecies.Length;
            }
            return -1;
        }


        private bool IsLeft(PointF a, PointF b, PointF c) {
            float abX = b.X - a.X;
            float abY = b.Y - a.Y;
            float acX = c.X - a.X;
            float acY = c.Y - a.Y;

            return abX * acY - acX * abY < 0;
        }


        //функция, определяющая, можно ли построить треугольник внутри полигона
        private bool CanBuildTriangle(int ai, int bi, int ci) {
            for (int i = 0; i < vertecies.Length; i++) {
                if (i != ai && i != bi && i != ci) {
                    if (IsPointInside(vertecies[ai], vertecies[bi], vertecies[ci], vertecies[i])) {
                        return false;
                    }
                }
            }
            return true;
        }


        //функция, определяющая, находится ли точка внутри полигона
        private bool IsPointInside(PointF a, PointF b, PointF c, PointF p) {
            float ab = (a.X - p.X) * (b.Y - a.Y) - (b.X - a.X) * (a.Y - p.Y);
            float bc = (b.X - p.X) * (c.Y - b.Y) - (c.X - b.X) * (b.Y - p.Y);
            float ca = (c.X - p.X) * (a.Y - c.Y) - (a.X - c.X) * (c.Y - p.Y);

            return (ab >= 0 && bc >= 0 && ca >= 0) || (ab <= 0 && bc <= 0 && ca <= 0);
        }


        //функция рисования треугольников
        private void DrawTriangles() {
            foreach (Triangle triangle in triangles) {
                graphics.FillPolygon(new SolidBrush(Color.FromArgb(random.Next(256), random.Next(256), random.Next(256))), triangle.GetVertecies());
                graphics.DrawLine(trianglePen, triangle.GetA(), triangle.GetB());
                graphics.DrawLine(trianglePen, triangle.GetB(), triangle.GetC());
                graphics.DrawLine(trianglePen, triangle.GetC(), triangle.GetA());
            }
        }
    }


    //класс Треугольник
    public class Triangle {
        private PointF a, b, c;

        public Triangle(PointF a, PointF b, PointF c) {
            this.a = a;
            this.b = b;
            this.c = c;
        }

        public PointF GetA() {
            return a;
        }

        public PointF GetB() {
            return b;
        }

        public PointF GetC() {
            return c;
        }

        public PointF[] GetVertecies() {
            return new PointF[] { a, b, c };
        }
    }
}
