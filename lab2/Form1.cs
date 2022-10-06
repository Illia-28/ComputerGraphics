using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //крива Безьє за методом "Де Кастельжо"
        Graphics G; // Об'єкт графіки
        PointF[] Arr1 = new PointF[] // Початковий масив точок
        {
            new PointF(50,400),
            new PointF(600,350),
            new PointF(100,200),
            new PointF(580,200),

        };
        int Fuctorial(int n) // Функція обчислення факторіалу
        {
            int res = 1;
            for (int i = 1; i <= n; i++)
                res *= i;
            return res;
        }
        float polinom(int i, int n, float t)// Функція обчислення полінома Бернштейна
        {
            return (Fuctorial(n) / (Fuctorial(i) * Fuctorial(n - i))) * (float)Math.Pow(t, i) * (float)Math.Pow(1 - t, n - i);
        }
        void Draw(PointF[] Arr)// Функція малювання кривої
        {
            int j = 0;
            float step = 0.01f;// Візьмемо крок 0.01 для більшої точності

            PointF[] result = new PointF[101];//Кінцевий масив точок кривої
            for (float t = 0; t < 1; t += step)
            {
                float ytmp = 0;
                float xtmp = 0;
                for (int i = 0; i < Arr.Length; i++)
                { // проходимо по кожній точці
                    float b = polinom(i, Arr.Length - 1, t); // обчислюємо наш поліном Бернштейна
                    xtmp += Arr[i].X * b; // записуємо та додаємо результат
                    ytmp += Arr[i].Y * b;
                }
                result[j] = new PointF(xtmp, ytmp);
                j++;

            }
            G.DrawLines(new Pen(Color.Red), result);// Малюємо отриману криву Безьє
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        
        }

        private void button1_Click(object sender, EventArgs e)
        {
            G = Graphics.FromHwnd(pictureBox1.Handle);
            G.Clear(Color.White);
            G.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            Draw(Arr1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PointF[] Arr_rotated = new PointF[Arr1.Length];
            for(int i = 0; i < Arr1.Length; i++)
            {
                double x_pivot = Arr1[0].X;
                double y_pivot = Arr1[0].Y;
                double x_shifted = Arr1[i].X - x_pivot;
                double y_shifted = Arr1[i].Y - y_pivot;
                float x = (float)(x_pivot + (x_shifted * Math.Cos(Math.PI / 4) - y_shifted * Math.Sin(Math.PI / 4)));
                float y = (float)(y_pivot + (x_shifted * Math.Sin(Math.PI / 4) + y_shifted * Math.Cos(Math.PI / 4)));
                Arr_rotated[i] = new PointF(x, y);
                //Console.WriteLine(x);
                //Console.WriteLine(y);
            }

            G = Graphics.FromHwnd(pictureBox1.Handle);
            G.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            Draw(Arr_rotated);
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            PointF[] Arr_scale = new PointF[Arr1.Length];
            float scale = 50; //%
            scale /= 100;
            for (int i = 0; i < Arr1.Length; i++)
            {
                Arr_scale[i] = new PointF(Arr1[i].X * scale, Arr1[i].Y * scale);
            }
            //G.Clear(Color.White);
            G = Graphics.FromHwnd(pictureBox1.Handle);
            G.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            Draw(Arr_scale);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            int value = rnd.Next(0, 10);
            if( value % 2 == 0) button6_Click(sender, e);
            else button7_Click(sender, e);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            PointF[] Arr_shift = new PointF[Arr1.Length];
            for (int i = 0; i < Arr1.Length; i++)
            {
                Arr_shift[i] = new PointF(Arr1[i].X + 43, Arr1[i].Y + 19);
            }
            // G.Clear(Color.White);
            G = Graphics.FromHwnd(pictureBox1.Handle);
            G.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            Draw(Arr_shift);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            double x_max = Arr1[0].X;
            for (int i = 0; i < Arr1.Length; i++)
            {
                if (Arr1[i].X > x_max) x_max = Arr1[i].X;
            }
           // Console.WriteLine(x_max);
            PointF[] Arr_mirror = new PointF[Arr1.Length];
            float x;
            for (int i = 0; i < Arr1.Length; i++)
            {
                x = (float)(x_max + (x_max - Arr1[i].X));
                Arr_mirror[i] = new PointF(x, Arr1[i].Y);
            }
            G = Graphics.FromHwnd(pictureBox1.Handle);
            G.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            Draw(Arr_mirror);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            double y_max = Arr1[0].Y;
            for (int i = 0; i < Arr1.Length; i++)
            {
                if (Arr1[i].X > y_max) y_max = Arr1[i].Y;
            }
           // Console.WriteLine(y_max);
            PointF[] Arr_mirror = new PointF[Arr1.Length];
            float y;
            for (int i = 0; i < Arr1.Length; i++)
            {
                y = (float)(y_max + (y_max - Arr1[i].Y));
                Arr_mirror[i] = new PointF(Arr1[i].X, y);
            }
            G = Graphics.FromHwnd(pictureBox1.Handle);
            G.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            Draw(Arr_mirror);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
        }

        
    }
}
