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


namespace lab3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void FloodFillzatravka(Bitmap bmp, Point pt, Color replacementColor)
        {
            Stack<Point> pixels = new Stack<Point>();
            Color targetColor = bmp.GetPixel(pt.X, pt.Y);
            pixels.Push(pt);
            while (pixels.Count > 0)
            {
                Point a = pixels.Pop();
                if (a.X < bmp.Width && a.X > 0 &&
                        a.Y < bmp.Height && a.Y > 0)
                {
                    if (bmp.GetPixel(a.X, a.Y) == targetColor)
                    {
                        bmp.SetPixel(a.X, a.Y, replacementColor);
                        pixels.Push(new Point(a.X - 1, a.Y));
                        pixels.Push(new Point(a.X + 1, a.Y));
                        pixels.Push(new Point(a.X, a.Y - 1));
                        pixels.Push(new Point(a.X, a.Y + 1));
                    }
                }
            }
            return;
        }

        private void FloodFillSL(Bitmap bmp, Point pt, Color replacementColor)
        {
            Color targetColor = bmp.GetPixel(pt.X, pt.Y);
            if (targetColor.ToArgb().Equals(replacementColor.ToArgb()))
            {
                return;
            }
            Stack<Point> pixels = new Stack<Point>();
            pixels.Push(pt);
            while (pixels.Count != 0)
            {
                Point temp = pixels.Pop();
                int y1 = temp.Y;
                while (y1 >= 0 && bmp.GetPixel(temp.X, y1) == targetColor)
                {
                    y1--;
                }
                y1++;
                bool spanLeft = false;
                bool spanRight = false;
                while (y1 < bmp.Height && bmp.GetPixel(temp.X, y1) == targetColor)
                {
                    bmp.SetPixel(temp.X, y1, replacementColor);

                    if (!spanLeft && temp.X > 0 && bmp.GetPixel(temp.X - 1, y1) == targetColor)
                    {
                        pixels.Push(new Point(temp.X - 1, y1));
                        spanLeft = true;
                    }
                    else if (spanLeft && temp.X - 1 == 0 && bmp.GetPixel(temp.X - 1, y1) != targetColor)
                    {
                        spanLeft = false;
                    }
                    if (!spanRight && temp.X < bmp.Width - 1 && bmp.GetPixel(temp.X + 1, y1) == targetColor)
                    {
                        pixels.Push(new Point(temp.X + 1, y1));
                        spanRight = true;
                    }
                    else if (spanRight && temp.X < bmp.Width - 1 && bmp.GetPixel(temp.X + 1, y1) != targetColor)
                    {
                        spanRight = false;
                    }
                    y1++;
                }
            }
        }

        private void FloodFill4xStack(Bitmap bmp, Point pt, Color replacementColor)
        {
            Color targetColor = bmp.GetPixel(pt.X, pt.Y);
            Stack<Point> pixels = new Stack<Point>();
            pixels.Push(pt);
            if (targetColor.ToArgb().Equals(replacementColor.ToArgb()))
            {
                return;
            }
            int[] dx = new int[] { 0, 1, 0, -1 };
            int[] dy = new int[] { -1, 0, 1, 0 };
            while (pixels.Count != 0)
            {
                Point temp = pixels.Pop();
                bmp.SetPixel(temp.X, temp.Y, replacementColor);
                for (int i = 0; i < 4; i++)
                {
                    int nx = temp.X + dx[i];
                    int ny = temp.Y + dy[i];
                    if (nx >= 0 && nx < bmp.Width && ny >= 0 && ny < bmp.Height && bmp.GetPixel(nx, ny) == targetColor)
                    {
                        pt.X = nx;
                        pt.Y = ny;
                        pixels.Push(pt);
                    }
                }
            }
        }

        private void FloodFill8xStack(Bitmap bmp, Point pt, Color replacementColor)
        {
            Color targetColor = bmp.GetPixel(pt.X, pt.Y);
            Stack<Point> pixels = new Stack<Point>();
            pixels.Push(pt);
            if (targetColor.ToArgb().Equals(replacementColor.ToArgb()))
            {
                return;
            }
            int[] dx = new int[] { 0, 1, 1, 1, 0, -1, -1, -1 };
            int[] dy = new int[] { -1, -1, 0, 1, 1, 1, 0, -1 };
            while (pixels.Count != 0)
            {
                Point temp = pixels.Pop();
                bmp.SetPixel(temp.X, temp.Y, replacementColor);
                for (int i = 0; i < 8; i++)
                {
                    int nx = temp.X + dx[i];
                    int ny = temp.Y + dy[i];
                    if (nx >= 0 && nx < bmp.Width && ny >= 0 && ny < bmp.Height && bmp.GetPixel(nx, ny) == targetColor)
                    {
                        pt.X = nx;
                        pt.Y = ny;
                        pixels.Push(pt);
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Stopwatch stopWatch = new Stopwatch();
            Bitmap bmp1 = new Bitmap(@"D:\KPI\3_course\grafic\lab3\triangl.png");
            Bitmap bmp2 = new Bitmap(@"D:\KPI\3_course\grafic\lab3\contour.png");
            pictureBox1.Image = new Bitmap(@"D:\KPI\3_course\grafic\lab3\triangl.png");
            pictureBox3.Image = new Bitmap(@"D:\KPI\3_course\grafic\lab3\contour.png");
            stopWatch.Start();
            FloodFillzatravka(bmp1, new Point(128, 128), Color.Yellow);
            stopWatch.Stop();
            TimeSpan ts1 = stopWatch.Elapsed;
            stopWatch.Start();
            FloodFillzatravka(bmp2, new Point(128, 128), Color.Yellow);
            stopWatch.Stop();
            TimeSpan ts2 = stopWatch.Elapsed;
            label4.Text = ts1.TotalMilliseconds + " ms";
            label5.Text = ts2.TotalMilliseconds + " ms";
            label4.Visible = true;
            label5.Visible = true;
            bmp1.Save(@"D:\KPI\3_course\grafic\lab3\New.png");
            bmp2.Save(@"D:\KPI\3_course\grafic\lab3\NewSecond.png");
            pictureBox2.Image = bmp1;
            pictureBox4.Image = bmp2;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Stopwatch stopWatch = new Stopwatch();
            Bitmap bmp1 = new Bitmap(@"D:\KPI\3_course\grafic\lab3\square.png");
            Bitmap bmp2 = new Bitmap(@"D:\KPI\3_course\grafic\lab3\contour.png");
            pictureBox1.Image = new Bitmap(@"D:\KPI\3_course\grafic\lab3\square.png");
            pictureBox3.Image = new Bitmap(@"D:\KPI\3_course\grafic\lab3\contour.png");
            stopWatch.Start();
            FloodFillSL(bmp1, new Point(128, 128), Color.Red);
            stopWatch.Stop();
            TimeSpan ts1 = stopWatch.Elapsed;
            stopWatch.Start();
            FloodFillSL(bmp2, new Point(128, 128), Color.Red);
            stopWatch.Stop();
            TimeSpan ts2 = stopWatch.Elapsed;
            label4.Text = ts1.TotalMilliseconds + " ms";
            label5.Text = ts2.TotalMilliseconds + " ms";
            label4.Visible = true;
            label5.Visible = true;
            bmp1.Save(@"D:\FormsKG2\MyNew.png");
            bmp2.Save(@"D:\FormsKG2\MyNewSecond.png");
            pictureBox2.Image = bmp1;
            pictureBox4.Image = bmp2;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Stopwatch stopWatch = new Stopwatch();
            Bitmap bmp1 = new Bitmap(@"D:\KPI\3_course\grafic\lab3\pentagon.png");
            Bitmap bmp2 = new Bitmap(@"D:\KPI\3_course\grafic\lab3\contour.png");
            pictureBox1.Image = new Bitmap(@"D:\KPI\3_course\grafic\lab3\pentagon.png");
            pictureBox3.Image = new Bitmap(@"D:\KPI\3_course\grafic\lab3\contour.png");
            stopWatch.Start();
            FloodFill4xStack(bmp1, new Point(128, 128), Color.Blue);
            stopWatch.Stop();
            TimeSpan ts1 = stopWatch.Elapsed;
            stopWatch.Start();
            FloodFill4xStack(bmp2, new Point(128, 128), Color.Blue);
            stopWatch.Stop();
            TimeSpan ts2 = stopWatch.Elapsed;
            label4.Text = ts1.TotalMilliseconds + " ms";
            label5.Text = ts2.TotalMilliseconds + " ms";
            label4.Visible = true;
            label5.Visible = true;
            bmp1.Save(@"D:\FormsKG2\MyNew.png");
            bmp2.Save(@"D:\FormsKG2\MyNewSecond.png");
            pictureBox2.Image = bmp1;
            pictureBox4.Image = bmp2;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Stopwatch stopWatch = new Stopwatch();
            Bitmap bmp1 = new Bitmap(@"D:\KPI\3_course\grafic\lab3\hexagon.png");
            Bitmap bmp2 = new Bitmap(@"D:\KPI\3_course\grafic\lab3\contour.png");
            pictureBox1.Image = new Bitmap(@"D:\KPI\3_course\grafic\lab3\hexagon.png");
            pictureBox3.Image = new Bitmap(@"D:\KPI\3_course\grafic\lab3\contour.png");
            stopWatch.Start();
            FloodFill8xStack(bmp1, new Point(128, 128), Color.DarkViolet);
            stopWatch.Stop();
            TimeSpan ts1 = stopWatch.Elapsed;
            stopWatch.Start();
            FloodFill8xStack(bmp2, new Point(128, 128), Color.DarkViolet);
            stopWatch.Stop();
            TimeSpan ts2 = stopWatch.Elapsed;
            label4.Text = ts1.TotalMilliseconds + " ms";
            label5.Text = ts2.TotalMilliseconds + " ms";
            label4.Visible = true;
            label5.Visible = true;
            bmp1.Save(@"D:\FormsKG2\MyNew.png");
            bmp2.Save(@"D:\FormsKG2\MyNewSecond.png");
            pictureBox2.Image = bmp1;
            pictureBox4.Image = bmp2;
        }
    

    private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(31, 71);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(103, 43);
            this.button1.TabIndex = 0;
            this.button1.Text = "Zatravka ";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(31, 140);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(103, 50);
            this.button2.TabIndex = 1;
            this.button2.Text = "SL";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(31, 216);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(103, 46);
            this.button3.TabIndex = 2;
            this.button3.Text = "Stack4";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(31, 293);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(103, 47);
            this.button4.TabIndex = 3;
            this.button4.Text = "Stack8";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(205, 82);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(282, 244);
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(593, 82);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(282, 244);
            this.pictureBox2.TabIndex = 5;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Location = new System.Drawing.Point(205, 367);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(282, 261);
            this.pictureBox3.TabIndex = 6;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox4
            // 
            this.pictureBox4.Location = new System.Drawing.Point(593, 367);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(282, 261);
            this.pictureBox4.TabIndex = 7;
            this.pictureBox4.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(319, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 20);
            this.label1.TabIndex = 8;
            this.label1.Text = "Before";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(709, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 20);
            this.label2.TabIndex = 9;
            this.label2.Text = "After";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(950, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 20);
            this.label3.TabIndex = 10;
            this.label3.Text = "Time";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(950, 140);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 20);
            this.label4.TabIndex = 11;
            this.label4.Text = "Time";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(950, 458);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(42, 20);
            this.label5.TabIndex = 12;
            this.label5.Text = "Time";
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(1073, 698);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox4);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Lab3";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

    }
}
