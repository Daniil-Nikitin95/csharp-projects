using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Windows.Forms;

namespace GraphLab2 {


    //структура Вектор
    struct Vector {
        public float x;
        public float y;

        public Vector(float x, float y) {
            this.x = x;
            this.y = y;
        }

        public static float operator *(Vector a, Vector b) {
            return a.x * b.x + a.y * b.y;
        }
    }


    //структура Точка
    struct Point {
        public float x;
        public float y;

        public Point(float x, float y) {
            this.x = x;
            this.y = y;
        }

        public static Vector operator -(Point p1, Point p2) {
            return new Vector(p1.x - p2.x, p1.y - p2.y);
        }
    }


    //структура Прямая
    struct Segment {
        public Point A;
        public Point B;

        public Segment(Point a, Point b) {
            A = a;
            B = b;
        }

        public Segment(float x1, float y1, float x2, float y2) {
            A = new Point(x1, y1);
            B = new Point(x2, y2);
        }

        public Point Center() {
            return new Point((A.x + B.x) / 2, (A.y + B.y) / 2);
        }

        public float Length() {
            return (float)Math.Sqrt(Math.Pow(B.x - A.x, 2) + Math.Pow(B.y - A.y, 2));
        }

        public bool HasPoint(float x, float y) {
            float AP = (float)Math.Sqrt(Math.Pow(x - A.x, 2) + Math.Pow(y - A.y, 2));
            float PB = (float)Math.Sqrt(Math.Pow(B.x - x, 2) + Math.Pow(B.y - y, 2));
            float AB = Length();
            return AB >= AP + PB - 0.1 && AB <= AP + PB + 0.1;
        }
    }


    //структура Квадрат
    struct Square {
        public Segment a;
        public Segment b;
        public Segment c;
        public Segment d;

        public Vector aNormal;
        public Vector bNormal;
        public Vector cNormal;
        public Vector dNormal;

        public Dictionary<Segment, Vector> ribNormalDictionary;

        public Square(Point A, Point B, Point C, Point D, Point center) {
            a = new Segment(A, B);
            b = new Segment(B, C);
            c = new Segment(C, D);
            d = new Segment(D, A);
            aNormal = center - a.Center();
            bNormal = center - b.Center();
            cNormal = center - c.Center();
            dNormal = center - d.Center();
            ribNormalDictionary = new Dictionary<Segment, Vector>() { { a, aNormal }, { b, bNormal }, { c, cNormal }, { d, dNormal } };
        }

        public Dictionary<Segment, Vector>.KeyCollection Segments() => ribNormalDictionary.Keys;
    }

    public partial class Form1 : Form {
        private readonly Random random = new Random();
        private readonly Pen pen = new Pen(Color.Blue, 1.5f);
        private readonly Pen clipPen = new Pen(Color.White, 1.6f);

        private readonly Point center;

        private readonly List<Square> squareList = new List<Square>();
        private readonly List<float> tLowerList = new List<float>();
        private readonly List<float> tUpperList = new List<float>();

        public Form1() {
            InitializeComponent();
            center = new Point(pictureBox.Width / 2, pictureBox.Height / 2);//центр изображения
            Image image = new Bitmap(pictureBox.Width, pictureBox.Height);
            Graphics graphics = Graphics.FromImage(image);
            graphics.Clear(Color.White);
            pictureBox.Image = image;
        }


        //кнопка генерации прямоугольника
        private void GenerateButtonClick(object sender, EventArgs e) {
            squareList.Clear();
            Image image = pictureBox.Image;
            Graphics graphics = Graphics.FromImage(image);
            graphics.Clear(Color.White);
            int numberOfRectangles = random.Next(20, 51);//количество треугольников в сгенерированном прямоугольнике
            int squareSide = random.Next(15, 20);//количество сторон квадратов
            int deltaLength = 17;

            int angle = random.Next(1, 90);//угол поворота прямоугольников относительно центра


            //отсечение вложенных прямоугольников
            for (int i = 0; i < numberOfRectangles; i++) {
                Point A = new Point(center.x - (squareSide + deltaLength * i) / 2, center.y - (squareSide + deltaLength * i) / 2);
                Point B = new Point(center.x + (squareSide + deltaLength * i) / 2, center.y - (squareSide + deltaLength * i) / 2);
                Point C = new Point(center.x + (squareSide + deltaLength * i) / 2, center.y + (squareSide + deltaLength * i) / 2);
                Point D = new Point(center.x - (squareSide + deltaLength * i) / 2, center.y + (squareSide + deltaLength * i) / 2);

                A = new Point(
                    (float)((A.x - center.x) * Math.Cos(ToRadians(-angle * i)) + (A.y - center.y) * Math.Sin(ToRadians(-angle * i)) + center.x),
                    (float)(-(A.x - center.x) * Math.Sin(ToRadians(-angle * i)) + (A.y - center.y) * Math.Cos(ToRadians(-angle * i)) + center.y));
                B = new Point(
                    (float)((B.x - center.x) * Math.Cos(ToRadians(-angle * i)) + (B.y - center.y) * Math.Sin(ToRadians(-angle * i)) + center.x),
                    (float)(-(B.x - center.x) * Math.Sin(ToRadians(-angle * i)) + (B.y - center.y) * Math.Cos(ToRadians(-angle * i)) + center.y));
                C = new Point(
                    (float)((C.x - center.x) * Math.Cos(ToRadians(-angle * i)) + (C.y - center.y) * Math.Sin(ToRadians(-angle * i)) + center.x),
                    (float)(-(C.x - center.x) * Math.Sin(ToRadians(-angle * i)) + (C.y - center.y) * Math.Cos(ToRadians(-angle * i)) + center.y));
                D = new Point(
                    (float)((D.x - center.x) * Math.Cos(ToRadians(-angle * i)) + (D.y - center.y) * Math.Sin(ToRadians(-angle * i)) + center.x),
                    (float)(-(D.x - center.x) * Math.Sin(ToRadians(-angle * i)) + (D.y - center.y) * Math.Cos(ToRadians(-angle * i)) + center.y));

                Square square = new Square(A, B, C, D, center);
                squareList.Add(square);
                DrawSquare(graphics, square);
            }


            pictureBox.Image = image;//обновить изображение после отсечения
            infoLabel.Text = "Количество прямоугольников: " + numberOfRectangles;//вывести количество прямоугольников
        }


        //функция перевода угла в радианы
        private double ToRadians(int angleInDegrees) {
            return angleInDegrees * Math.PI / 180;
        }


        //функция рисования квадрата
        private void DrawSquare(Graphics graphics, Square square) {
            graphics.DrawLine(pen, square.a.A.x, square.a.A.y, square.a.B.x, square.a.B.y);//нарисовать точку a
            graphics.DrawLine(pen, square.b.A.x, square.b.A.y, square.b.B.x, square.b.B.y);//нарисовать точку b
            graphics.DrawLine(pen, square.c.A.x, square.c.A.y, square.c.B.x, square.c.B.y);//нарисовать точку c
            graphics.DrawLine(pen, square.d.A.x, square.d.A.y, square.d.B.x, square.d.B.y);//нарисовать точку d
        }


        //кнопка отсечения вложенных прямоугольников
        private void ClipButtonClick(object sender, EventArgs e) {
            Image image = pictureBox.Image;
            Graphics graphics = Graphics.FromImage(image);
            if (squareList.Count > 0) {
                for (int i = 0; i < squareList.Count; i++) {
                    foreach (Segment segment in squareList[i].Segments()) {
                        for (int j = i; j < squareList.Count; j++) {
                            if (!squareList[i].Equals(squareList[j])) {
                                Clip(segment, squareList[j], graphics);//вызов функции отсечения
                            }
                        }
                    }
                }
            } else {
                infoLabel.Text = "Сначала необходимо сгенерировать фигуры";
            }
            pictureBox.Image = image;
        }


        //функция отсечения
        private void Clip(Segment segment, Square square, Graphics graphics) {
            float x, y;
            Vector D = segment.B - segment.A;
            if (D.x != 0 && D.y != 0) {
                tLowerList.Clear();
                tUpperList.Clear();
                foreach (Segment rib in square.Segments()) {
                    Vector w = segment.A - rib.A;
                    Vector n = square.ribNormalDictionary[rib];
                    float D_n = D * n;
                    float w_n = w * n;
                    float t = -w_n / D_n;
                    if (t >= 0 && t <= 1) {
                        x = segment.A.x + (segment.B.x - segment.A.x) * t;
                        y = segment.A.y + (segment.B.y - segment.A.y) * t;
                        if (rib.HasPoint(x, y)) {
                            if (D_n > 0) {
                                tLowerList.Add(t);
                            } else if (D_n < 0) {
                                tUpperList.Add(t);
                            }
                        }
                    }
                }
                if (tUpperList.Count != 0 && tLowerList.Count != 0) {
                    tUpperList.Sort();
                    tLowerList.Sort();
                    DrawClippedSegment(segment, square, graphics);//вызов функции рисования отсеченного сегмента
                }
            }
        }


        //функция рисования отсеченного сегмента
        private void DrawClippedSegment(Segment segment, Square square, Graphics graphics) {
            float x1, y1, x2, y2;


            //координаты отсеченного сегмента
            x1 = segment.A.x + (segment.B.x - segment.A.x) * tUpperList[0];
            y1 = segment.A.y + (segment.B.y - segment.A.y) * tUpperList[0];
            x2 = segment.A.x + (segment.B.x - segment.A.x) * tLowerList[^1];
            y2 = segment.A.y + (segment.B.y - segment.A.y) * tLowerList[^1];
            graphics.DrawLine(clipPen, x1, y1, x2, y2);//нарисовать линию
        }
    }
}
