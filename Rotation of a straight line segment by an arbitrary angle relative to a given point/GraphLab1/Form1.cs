using System;
using System.Drawing;
using System.Windows.Forms;

namespace GraphLab1 {
    struct Segment {
        public float x1;
        public float y1;
        public float x2;
        public float y2;

        public Segment(float x1, float y1, float x2, float y2) {


            //инициализация начальных координат по осям ox и oy
            this.x1 = x1;
            this.y1 = y1;
            this.x2 = x2;
            this.y2 = y2;
        }
    }

    public partial class Form1 : Form {
        private readonly Pen segmentPen = new Pen(Color.Red, 3);
        private readonly Segment segment;
        private readonly Font font = new Font("Segoe UI", 15);
        private readonly Brush textBrush = Brushes.Black;
        private readonly Brush pointBrush = Brushes.Blue;

        private readonly float POINT_WIDTH = 7;
        private readonly float POINT_HEIGHT = 7;

        private decimal angle;//угол поворота

        private Point point;

        public Form1() {
            InitializeComponent();
            segment = new Segment(pictureBox.Width / 2, pictureBox.Height / 2 - pictureBox.Height / 6, pictureBox.Width / 2, pictureBox.Height / 2 + pictureBox.Height / 6);//прямая
            point = new Point(pictureBox.Width / 2 - 40, pictureBox.Height / 2 + 30);//точка
            DrawPointAndSegment();//вызов функции Нарисовать точку и прямую
        }


        //окно для рисунка
        private void PictureBoxMouseClick(object sender, MouseEventArgs e) {
            InitializePoint(e.X, e.Y);//вызов функции инициализации точки с координатами x и y
            DrawPointAndSegment();//вызов функции Нарисовать точку и прямую
        }


        //функции инициализации точки с координатами x и y
        private void InitializePoint(int x, int y) {
            point = new Point(x, y);//создание объекта Точка
        }


        //функция Нарисовать точку и прямую
        private void DrawPointAndSegment() {
            DrawInitialSegment();//вызов функции Нарисовать исходную прямую
            Image image = pictureBox.Image;//создание объекта, отображаемое элементом pictureBox
            Graphics graphics = Graphics.FromImage(image);
            graphics.FillEllipse(pointBrush, point.X - POINT_WIDTH / 2, point.Y - POINT_HEIGHT / 2, POINT_WIDTH, POINT_HEIGHT);
            graphics.DrawString("O", font, textBrush, point.X + 5, point.Y - 20);
            pictureBox.Image = image;
        }


        //Функция рисующая исходный сегмент
        private void DrawInitialSegment() {
            Image image = new Bitmap(pictureBox.Width, pictureBox.Height);
            Graphics graphics = Graphics.FromImage(image);//Создает новый объект Graphics из указанного объекта Image
            graphics.DrawLine(segmentPen, segment.x1, segment.y1, segment.x2, segment.y2);
            graphics.FillEllipse(pointBrush, segment.x1 - POINT_WIDTH / 2, segment.y1 - POINT_HEIGHT / 2, POINT_WIDTH, POINT_HEIGHT);
            graphics.FillEllipse(pointBrush, segment.x2 - POINT_WIDTH / 2, segment.y2 - POINT_HEIGHT / 2, POINT_WIDTH, POINT_HEIGHT);
            graphics.DrawString("A", font, textBrush, segment.x1 + 5, segment.y1 - 20);//подписать один конец прямой буквой А
            graphics.DrawString("B", font, textBrush, segment.x2 + 5, segment.y2 - 20);//подписать другой конец прямой буквой В
            pictureBox.Image = image;
        }


        //кнопка поворачивающая линию на заданный угол
        private void RotateButtonClick(object sender, EventArgs e) {
            DrawPointAndSegment();//нарисовать точку и прямую
            angle = angleNumericUpDown.Value;//получение заданного угла


            //получение новых координат после изменения угла
            float x1_new = (float)((segment.x1 - point.X) * Math.Cos(ToRadians(-angle)) + (segment.y1 - point.Y) * Math.Sin(ToRadians(-angle)) + point.X);
            float y1_new = (float)(-(segment.x1 - point.X) * Math.Sin(ToRadians(-angle)) + (segment.y1 - point.Y) * Math.Cos(ToRadians(-angle)) + point.Y);
            float x2_new = (float)((segment.x2 - point.X) * Math.Cos(ToRadians(-angle)) + (segment.y2 - point.Y) * Math.Sin(ToRadians(-angle)) + point.X);
            float y2_new = (float)(-(segment.x2 - point.X) * Math.Sin(ToRadians(-angle)) + (segment.y2 - point.Y) * Math.Cos(ToRadians(-angle)) + point.Y);
            
            
            DrawRotatedSegment(x1_new, y1_new, x2_new, y2_new);//нарисовать перевернутую прямую
        }


        //перевод угла в радианы
        private double ToRadians(decimal angleInDegrees) {
            return (double)angleInDegrees * Math.PI / 180;
        }


        //повернуть перевернутую линию
        private void DrawRotatedSegment(float x1, float y1, float x2, float y2) {
            Image image = pictureBox.Image;//создание объекта, отображаемое элементом pictureBox
            Graphics graphics = Graphics.FromImage(image);//Создает новый объект Graphics из указанного объекта Image
            graphics.DrawLine(segmentPen, x1, y1, x2, y2);
            graphics.FillEllipse(pointBrush, x1 - POINT_WIDTH / 2, y1 - POINT_HEIGHT / 2, POINT_WIDTH, POINT_HEIGHT);
            graphics.FillEllipse(pointBrush, x2 - POINT_WIDTH / 2, y2 - POINT_HEIGHT / 2, POINT_WIDTH, POINT_HEIGHT);
            graphics.DrawString("A'", font, textBrush, x1 + 5, y1 - 20);//подписать один конец прямой буквой А
            graphics.DrawString("B'", font, textBrush, x2 + 5, y2 - 20);//подписать другой конец прямой буквой В
            pictureBox.Image = image;//замена старого изображения новым
        }
    }
}
