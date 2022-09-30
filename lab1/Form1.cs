using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;



namespace lab1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        int[,] LastName = {
            { 10, 10, 50, 10, 10, 10, 10, 100, 10, 100, 50, 100,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1}, //С
            { 100, 60, 100, 100, 80, 60, 120, 60, -1,-1,-1,-1,-1,-1,-1,-1-1,-1,-1,-1, -1,-1,-1,-1,-1}, //т
            { 150, 60, 180, 60, 150, 75, 180, 75, 150, 60, 150, 100, 150, 100, 180, 100, 180, 60, 180, 75,-1,-1,-1,-1}, //е
            { 210, 60, 210, 95, 235, 60, 235, 95, 210, 95, 240, 95,240, 95, 240, 100,-1,-1,-1,-1,-1,-1,-1,-1}, //ц
            { 270, 60, 270, 100, 270, 80, 280, 80, 290, 60, 280, 80, 290, 60, 300, 80, 300, 80,290, 100, 280, 80,290, 100},//ю
            { 330, 60, 350, 60,330, 60, 330, 100,330, 75, 350, 75,350, 60, 350, 75,-1,-1,-1,-1,-1,-1,-1,-1}, //р
            { 380, 60, 410, 60, 380, 75, 410, 75,380, 60, 380, 100,380, 100, 410, 100,410, 60, 410, 75,-1,-1,-1,-1},//е
            { 440, 60, 440, 100,470, 60, 470, 100,440, 80, 470, 80,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},//н
            { 500, 60, 500, 100, 500, 80, 530, 60, 500, 80, 530, 100, -1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},//к
            { 575, 60, 560, 80, 575, 60, 590, 80, 590,80,575, 100, 560, 80, 575, 100, -1,-1,-1,-1,-1,-1,-1,-1}//о
        };


        private static void PutPixel(Graphics g, Color col, float x, float y, int alpha)//Метод, що встановлює пікселі на формі із заданим кольором і прозорістю
        {
            g.FillRectangle(new SolidBrush(Color.FromArgb(alpha, col)), x, y, 1, 1);
        }

        public static void DDAline(Graphics g, Color clr, float x0, float y0, float x1, float y1)
        {
            float step, x,y;
            float dx = (x1 - x0);
            float dy = (y1 - y0);
            if (Math.Abs(dx) >= Math.Abs(dy))
              step = (Math.Abs(dx)); 
            else
              step = (Math.Abs(dy)); 
            dx = dx / step;
            dy = dy / step;
            x = x0;
            y = y0;
            for(int i=1; i <=step; i++)
            {
                PutPixel(g, clr, x, y, 255);
                x = x + dx;
                y = y + dy;
            }
        }
    
        private void button1_Click(object sender, EventArgs e) //DDA
        {
            Stopwatch sLine = new Stopwatch();
            Stopwatch sLastName = new Stopwatch();
            Stopwatch sWatch = new Stopwatch();

            sLine.Start();
            sWatch.Start(); 
            
            Graphics g = pictureBox1.CreateGraphics();
            DDAline(g, Color.Black, 100, 250, pictureBox1.Width - 50, pictureBox1.Height - 50);
            
            sLine.Stop();
            
            sLastName.Start();

            int rows = LastName.GetUpperBound(0) + 1;    // кількість рядків
            int columns = LastName.Length / rows;        // кількість рядків

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j = j + 4)
                {
                    if (LastName[i, j] > 0)
                        DDAline(g, Color.Black, LastName[i, j], LastName[i, j+1], LastName[i, j+2], LastName[i, j+3]);
                }
            }
            sLastName.Stop();
            sWatch.Stop();

            Console.WriteLine("\n\nDDA");
            Console.Write("-Time required to draw a line:");
            Console.Write(sLine.ElapsedMilliseconds.ToString());
            Console.Write("\n-The time required to draw a surname:");
            Console.Write(sLastName.ElapsedMilliseconds.ToString());
            Console.Write("\n-Total time:");
            Console.Write(sWatch.ElapsedMilliseconds.ToString());
        }

        public static void BresenhamLine(Graphics g, Color clr, float x0, float y0, float x1, float y1)
        {
            //Зміни координат
            float dx = (x1 > x0) ? (x1 - x0) : (x0 - x1);
            float dy = (y1 > y0) ? (y1 - y0) : (y0 - y1);
            //Напрямок збільшення
            float sx = (x1 >= x0) ? (1) : (-1);
            float sy = (y1 >= y0) ? (1) : (-1);

            if (dy < dx)
            {
                float d = (dy * 2) - dx;//(dy << 1) - dx;
                float d1 = dy * 2;//dy << 1;
                float d2 = (dy - dx) * 2;//(dy - dx) << 1;
                PutPixel(g, clr, x0, y0, 255);
                float x = x0 + sx;
                float y = y0;
                for (int i = 1; i <= dx; i++)
                {
                    if (d > 0)
                    {
                        d += d2;
                        y += sy;
                    }
                    else
                        d += d1;
                    PutPixel(g, clr, x, y, 255);
                    x++;
                }
            }
            else
            {
                float d = (dx * 2) - dy;//(dx << 1) - dy;
                float d1 = dx * 2;// dx << 1;
                float d2 = (dx - dy) * 2;//(dx - dy) << 1;
                PutPixel(g, clr, x0, y0, 255);
                float x = x0;
                float y = y0 + sy;
                for (int i = 1; i <= dy; i++)
                {
                    if (d > 0)
                    {
                        d += d2;
                        x += sx;
                    }
                    else
                        d += d1;
                    PutPixel(g, clr, x, y, 255);
                    y++;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e) //Bresenham's line
        {
            Stopwatch sLine = new Stopwatch();
            Stopwatch sLastName = new Stopwatch();
            Stopwatch sWatch = new Stopwatch();

            sLine.Start();
            sWatch.Start();

            Graphics g = pictureBox1.CreateGraphics();
            BresenhamLine(g, Color.Black, 100, 250, pictureBox1.Width - 50, pictureBox1.Height - 50);

            sLine.Stop();

            sLastName.Start();

            int rows = LastName.GetUpperBound(0) + 1;    // кількість рядків
            int columns = LastName.Length / rows;        // кількість рядків

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j = j + 4)
                {
                    if (LastName[i, j] > 0)
                        BresenhamLine(g, Color.Black, LastName[i, j], LastName[i, j + 1], LastName[i, j + 2], LastName[i, j + 3]);
                }
            }
            sLastName.Stop();
            sWatch.Stop();

            Console.WriteLine("\n\nBresenham's line");
            Console.Write("-Time required to draw a line:");
            Console.Write(sLine.ElapsedMilliseconds.ToString());
            Console.Write("\n-The time required to draw a surname:");
            Console.Write(sLastName.ElapsedMilliseconds.ToString());
            Console.Write("\n-Total time:");
            Console.Write(sWatch.ElapsedMilliseconds.ToString());
        }

        public static void BresenhamCircle(Graphics g, Color clr, float rx, float ry, float radius)
        {
            float x = 0, y = radius, gap = 0, delta = (2 - 2 * radius);
            while (y >= 0)
            {
                PutPixel(g, clr, rx + x, ry + y, 255);
                PutPixel(g, clr, rx + x, ry - y, 255);
                PutPixel(g, clr, rx - x, ry - y, 255);
                PutPixel(g, clr, rx - x, ry + y, 255);
                gap = 2 * (delta + y) - 1;
                if (delta < 0 && gap <= 0)
                {
                    x++;
                    delta += 2 * x + 1;
                    continue;
                }
                if (delta > 0 && gap > 0)
                {
                    y--;
                    delta -= 2 * y + 1;
                    continue;
                }
                x++;
                delta += 2 * (x - y);
                y--;
            }
        }

        private void button5_Click(object sender, EventArgs e) //Bresenham's circle
        {
            Stopwatch sWatch = new Stopwatch();
            
            sWatch.Start();

            Graphics g = pictureBox1.CreateGraphics();
            BresenhamCircle(g, Color.Black, 350, 165, 150);

            sWatch.Stop();

            Console.WriteLine("\n\nBresenham's circle:");
            Console.Write("\n-Time:");
            Console.Write(sWatch.ElapsedMilliseconds.ToString());
        }

        //Целая часть числа
        private static int IPart(float x)
        {
            return (int)x;
        }
        //дробная часть числа
        private static float FPart(float x)
        {
            while (x >= 0)
                x--;
            x++;
            return x;
        }

        public static void DrawWuLine(Graphics g, Color clr, int x0, int y0, int x1, int y1)
        {
            //Вычисление изменения координат
            int dx = (x1 > x0) ? (x1 - x0) : (x0 - x1);
            int dy = (y1 > y0) ? (y1 - y0) : (y0 - y1);
            //Если линия параллельна одной из осей, рисуем обычную линию - заполняем все пикселы в ряд
            if (dx == 0 || dy == 0)
            {
                g.DrawLine(new Pen(clr), x0, y0, x1, y1);
                return;
            }

            //Для Х-линии (коэффициент наклона < 1)
            if (dy < dx)
            {
                //Первая точка должна иметь меньшую координату Х
                if (x1 < x0)
                {
                    x1 += x0; x0 = x1 - x0; x1 -= x0;
                    y1 += y0; y0 = y1 - y0; y1 -= y0;
                }
                //Относительное изменение координаты Y
                float grad = (float)dy / dx;
                //Промежуточная переменная для Y
                float intery = y0 + grad;
                //Первая точка
                PutPixel(g, clr, x0, y0, 255);

                for (int x = x0 + 1; x < x1; x++)
                {
                    //Верхняя точка
                    PutPixel(g, clr, x, IPart(intery), (int)(255 - FPart(intery) * 255));
                    //Нижняя точка
                    PutPixel(g, clr, x, IPart(intery) + 1, (int)(FPart(intery) * 255));
                    //Изменение координаты Y
                    intery += grad;
                }
                //Последняя точка
                PutPixel(g, clr, x1, y1, 255);
            }
            //Для Y-линии (коэффициент наклона > 1)
            else
            {
                //Первая точка должна иметь меньшую координату Y
                if (y1 < y0)
                {
                    x1 += x0; x0 = x1 - x0; x1 -= x0;
                    y1 += y0; y0 = y1 - y0; y1 -= y0;
                }
                //Относительное изменение координаты X
                float grad = (float)dx / dy;
                //Промежуточная переменная для X
                float interx = x0 + grad;
                //Первая точка
                PutPixel(g, clr, x0, y0, 255);

                for (int y = y0 + 1; y < y1; y++)
                {
                    //Верхняя точка
                    PutPixel(g, clr, IPart(interx), y, 255 - (int)(FPart(interx) * 255));
                    //Нижняя точка
                    PutPixel(g, clr, IPart(interx) + 1, y, (int)(FPart(interx) * 255));
                    //Изменение координаты X
                    interx += grad;
                }
                //Последняя точка
                PutPixel(g, clr, x1, y1, 255);
            }
        }

        private void button3_Click(object sender, EventArgs e) //Wu's line
        {
            Stopwatch sLine = new Stopwatch();
            Stopwatch sLastName = new Stopwatch();
            Stopwatch sWatch = new Stopwatch();

            sLine.Start();
            sWatch.Start();

            Graphics g = pictureBox1.CreateGraphics();
            DrawWuLine(g, Color.Black, 100, 250, pictureBox1.Width - 150, pictureBox1.Height - 100);

            sLine.Stop();

            sLastName.Start();

            int rows = LastName.GetUpperBound(0) + 1;    // кількість рядків
            int columns = LastName.Length / rows;        // кількість рядків

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j = j + 4)
                {
                    if (LastName[i, j] > 0)
                        BresenhamLine(g, Color.Black, LastName[i, j], LastName[i, j + 1], LastName[i, j + 2], LastName[i, j + 3]);
                }
            }
            sLastName.Stop();
            sWatch.Stop();

            Console.WriteLine("\n\nWu's line");
            Console.Write("-Time required to draw a line:");
            Console.Write(sLine.ElapsedMilliseconds.ToString());
            Console.Write("\n-The time required to draw a surname:");
            Console.Write(sLastName.ElapsedMilliseconds.ToString());
            Console.Write("\n-Total time:");
            Console.Write(sWatch.ElapsedMilliseconds.ToString());
        }

        private void button4_Click(object sender, EventArgs e) // clean
        {
            pictureBox1.Image = null;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
    }
}
