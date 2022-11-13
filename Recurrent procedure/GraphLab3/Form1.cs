using System;
using System.Drawing;
using System.Windows.Forms;


//структура Точка
struct Point {
    public float x;
    public float y;

    public Point(float x, float y) {
        this.x = x;
        this.y = y;
    }
}


//структура Сегмент
struct Segment {
    public Point firstPoint;
    public Point secondPoint;

    public Segment(Point a, Point b) {
        firstPoint = a;
        secondPoint = b;
    }

    public Segment(float x1, float y1, float x2, float y2) {
        firstPoint = new Point(x1, y1);
        secondPoint = new Point(x2, y2);
    }

    public float Length() {
        return (float)Math.Sqrt(Math.Pow(secondPoint.x - firstPoint.x, 2) + Math.Pow(secondPoint.y - firstPoint.y, 2));
    }
}


//структура Треугольник
struct RightTriangle {
    public Segment CD;
    public Segment DE;
    public Segment EC;

    public RightTriangle(Segment CD, float angle) {
        this.CD = CD;
        float DE_Length = (float)(CD.Length() * Math.Sin(ToRadians(angle)));
        float EC_Length = (float)(CD.Length() * Math.Cos(ToRadians(angle)));
        float q = -EC_Length / DE_Length;
        float E_x = (CD.firstPoint.x + CD.secondPoint.x * q * q + (CD.firstPoint.y - CD.secondPoint.y) * q) / (1 + q * q);
        float E_y = (CD.firstPoint.y + CD.secondPoint.y * q * q + (CD.secondPoint.x - CD.firstPoint.x) * q) / (1 + q * q);
        Point E = new Point(E_x, E_y);
        DE = new Segment(CD.secondPoint, E);
        EC = new Segment(E, CD.firstPoint);
    }

    private static float ToRadians(float angleInDegrees) {
        return (float)(angleInDegrees * Math.PI / 180);
    }
}


//структура Квадрат
struct Square {
    public Segment AB;
    public Segment BC;
    public Segment CD;
    public Segment DA;

    public Square(Segment AB) {
        this.AB = AB;
        Point C = new Point(this.AB.secondPoint.x + this.AB.firstPoint.y - this.AB.secondPoint.y, this.AB.secondPoint.y + this.AB.secondPoint.x - this.AB.firstPoint.x);
        Point D = new Point(this.AB.firstPoint.x + this.AB.firstPoint.y - this.AB.secondPoint.y, this.AB.firstPoint.y + this.AB.secondPoint.x - this.AB.firstPoint.x);
        BC = new Segment(AB.secondPoint, C);
        CD = new Segment(C, D);
        DA = new Segment(D, AB.firstPoint);
    }
}

namespace GraphLab3 {
    public partial class Form1 : Form {
        private readonly Point center;
        private readonly Pen pen = new Pen(Color.DarkRed, 1.5f);
        private Graphics graphics;
        private float angle;//внутренний угол прямоугольных треугольников
        private int depth;//глубина рекурсии
        private Image image;


        //инициализация формы
        public Form1() {
            InitializeComponent();
            center = new Point(pictureBox.Width / 2, pictureBox.Height / 2);//центр изображения
            image = new Bitmap(pictureBox.Width, pictureBox.Height);
            graphics = Graphics.FromImage(image);
            graphics.Clear(Color.White);
            pictureBox.Image = image;
        }


        //кнопка генерации домика
        private void GenerateButtonClick(object sender, EventArgs e) {
            image = new Bitmap(pictureBox.Width, pictureBox.Height);
            graphics = Graphics.FromImage(image);
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            angle = (float)angleNumericUpDown.Value;
            depth = (int)recursionDepthNumericUpDown.Value;
            graphics.Clear(Color.White);
            Segment baseSegment = new Segment(center.x - 25, 40, center.x + 25, 40);
            DrawTreePart(baseSegment, angle, depth);//нарисовать домик
            pictureBox.Image = image;
        }


        //функция рисования домика
        private void DrawTreePart(Segment baseSegment, float angle, int depth) {
            if (depth > 0) {
                Square square = new Square(baseSegment);
                DrawSquare(square, graphics);//нарисовать квадрат
                RightTriangle triangle = new RightTriangle(square.CD, angle);
                DrawTriangle(triangle, graphics);//нарисовать треугольник
                DrawTreePart(triangle.EC, angle, depth - 1);//нарисовать домик
                DrawTreePart(triangle.DE, angle, depth - 1);//нарисовать домик
            }
        }


        //функция рисования квадрата
        private void DrawSquare(Square square, Graphics graphics) {
            graphics.DrawLine(pen, square.AB.firstPoint.x, square.AB.firstPoint.y, square.AB.secondPoint.x, square.AB.secondPoint.y);//нарисовать сторону AB квадрата
            graphics.DrawLine(pen, square.BC.firstPoint.x, square.BC.firstPoint.y, square.BC.secondPoint.x, square.BC.secondPoint.y);//нарисовать сторону BC квадрата
            graphics.DrawLine(pen, square.CD.firstPoint.x, square.CD.firstPoint.y, square.CD.secondPoint.x, square.CD.secondPoint.y);//нарисовать сторону CD квадрата
            graphics.DrawLine(pen, square.DA.firstPoint.x, square.DA.firstPoint.y, square.DA.secondPoint.x, square.DA.secondPoint.y);//нарисовать сторону DA квадрата

        }


        //функция рисования треугольника
        private void DrawTriangle(RightTriangle triangle, Graphics graphics) {
            graphics.DrawLine(pen, triangle.CD.firstPoint.x, triangle.CD.firstPoint.y, triangle.CD.secondPoint.x, triangle.CD.secondPoint.y);//нарисовать сторону CD треугольника
            graphics.DrawLine(pen, triangle.DE.firstPoint.x, triangle.DE.firstPoint.y, triangle.DE.secondPoint.x, triangle.DE.secondPoint.y);//нарисовать сторону DE треугольника
            graphics.DrawLine(pen, triangle.EC.firstPoint.x, triangle.EC.firstPoint.y, triangle.EC.secondPoint.x, triangle.EC.secondPoint.y);//нарисовать сторону EC треугольника
        }
    }
}
