using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab4
{
    public partial class Form1 : Form
    {
        static Pen pen1;
        static Graphics g;
        static Pen pen2;


        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) // Сніжинка Коха
        {
            pictureBox1.Visible = false;

            //Вибираємо кольори замальовки
            pen1 = new Pen(Color.Green, 1);
            pen2 = new Pen(Color.Blue, 1);
            //Визначаємо об'єкт "g" класу Graphics
            g = CreateGraphics();
            g.Clear(Color.White);

            //Визначимо координати вихідного трикутника
            var point1 = new PointF(300, 200);
            var point2 = new PointF(1000, 200);
            var point3 = new PointF(650, 700);

            //Замалюємо трикутник
            g.DrawLine(pen1, point1, point2);
            g.DrawLine(pen1, point2, point3);
            g.DrawLine(pen1, point3, point1);

            //Викликаємо функцію Fractal для того, щоб намалювати три кривих Коха на сторонах трикутника
            Fractal(point1, point2, point3, 5);
            Fractal(point2, point3, point1, 5);
            Fractal(point3, point1, point2, 5);
        }

        //Рекурсивна функція малювання кривої Коха
        static int Fractal(PointF p1, PointF p2, PointF p3, int iter)
        {
        
            if (iter > 0) 
            {
                //середня третина відрізка
                var p4 = new PointF((p2.X + 2 * p1.X) / 3, (p2.Y + 2 * p1.Y) / 3);
                var p5 = new PointF((2 * p2.X + p1.X) / 3, (p1.Y + 2 * p2.Y) / 3);
                //координати вершини кута
                var ps = new PointF((p2.X + p1.X) / 2, (p2.Y + p1.Y) / 2);
                var pn = new PointF((4 * ps.X - p3.X) / 3, (4 * ps.Y - p3.Y) / 3);
                //малюємо його
                g.DrawLine(pen1, p4, pn);
                g.DrawLine(pen1, p5, pn);
                g.DrawLine(pen2, p4, p5);


                //Рекурсивно викликаємо функцію необхідне число разів
                Fractal(p4, pn, p5, iter - 1);
                Fractal(pn, p5, p4, iter - 1);
                Fractal(p1, p4, new PointF((2 * p1.X + p3.X) / 3, (2 * p1.Y + p3.Y) / 3), iter - 1);
                Fractal(p5, p2, new PointF((2 * p2.X + p3.X) / 3, (2 * p2.Y + p3.Y) / 3), iter - 1);

            }
            return iter;
        }

        public class Complex
        {
            public double a;
            public double b;

            public Complex(double a, double b)
            {
                this.a = a;
                this.b = b;
            }

            public void Square() // корінь числа
            {
                double temp = (a * a) - (b * b);
                b = 2.0 * a * b;
                a = temp;
            }

            public double Magnitude() // обчислення 
            {
                return Math.Sqrt((a * a) + (b * b));
            }

            public void Add(Complex c) // додавання
            {
                a += c.a;
                b += c.b;
            }
        }

        private void button2_Click(object sender, EventArgs e) // множина Мандельброта
        {

            g = CreateGraphics();
            g.Clear(Color.White);
            pictureBox1.Visible = true;
            
            Bitmap bm = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            for (int x = 0; x < pictureBox1.Width; x++)
            {
                for (int y = 0; y < pictureBox1.Height; y++)
                {
                    double a = (double)(x - (pictureBox1.Width / 2)) / (double)(pictureBox1.Width / 4);
                    double b = (double)(y - (pictureBox1.Height / 2)) / (double)(pictureBox1.Height / 4);
                    Complex c = new Complex(a, b); // вказівник координат
                    Complex z = new Complex(0, 0); // відображеня
                    int it = 0;
                    do
                    {
                        it++;
                        z.Square();
                        z.Add(c);
                        if (z.Magnitude() > 2.0) break;
                    }
                    while (it < 100);
                    bm.SetPixel(x, y, it < 100 ? Color.White : Color.Purple);
                }
            }
            pictureBox1.Image = bm;
        }

        private void button3_Click(object sender, EventArgs e)// гілка папороті
        {
            g = CreateGraphics();
            g.Clear(Color.White);
            pictureBox1.Visible = true;
            var bm  = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            var r = new Random();
            double x = 0;
            double y = 0;
            for (int count = 0; count < 200000; count++) // від кількості повторень залежить, наскільки детальною буде папороть
            {
                bm.SetPixel((int)(550 + 85 * x), (int)(50 + 85 * y), Color.Green); // задаємо маштаб та зміщення відносно перетину Ox і Oy
                int roll = r.Next(100); // випадкове число до 100
                double xp = x;
                if (roll < 1)
                {
                    x = 0;
                    y = 0.16 * y;
                }
                else if (roll < 86)
                {
                    x = 0.85 * x + 0.04 * y;
                    y = -0.04 * xp + 0.85 * y + 1.6;
                }
                else if (roll < 93)
                {
                    x = 0.2 * x - 0.26 * y;
                    y = 0.23 * xp + 0.22 * y + 1.6;
                }
                else
                {
                    x = -0.15 * x + 0.28 * y;
                    y = 0.26 * xp + 0.24 * y + 0.44;
                }
            }
            pictureBox1.Image = bm;
        }
    }
}
