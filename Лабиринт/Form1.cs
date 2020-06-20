using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Лабиринт
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }

        public int _X_ = 0;
        public int _Y_ = 0;

        PictureBox[,] Pole;
        Stackk stacks = new Stackk();
        List<Point> Sosedi = new List<Point>();
        Random random;
        Point start;
        Point SledHod;

        //генерация поля
        public void Generic()
        {
            int Size = 0;
            _X_ = Convert.ToInt32(textBox1.Text);
            _Y_ = Convert.ToInt32(textBox2.Text);

            if ((_X_ <= 13) && (_Y_ <= 13))
                Size = 15;
            else
                Size = 5;

            Pole = new PictureBox[(_Y_ * 2) + 1, (_X_ * 2) + 1];
            int X = 0;
            int Y = 0;
            for (int i = 0; i < (_Y_ * 2) + 1; i++)
            {
                X = 0;
                if (i == 0)
                {
                    for (int j = 0; j < (_X_ * 2) + 1; j++)
                    {
                        PictureBox pic = new PictureBox()
                        {
                            Location = new Point(X, Y),
                            Size = new Size(Size, Size),
                            BackColor = Color.FromArgb(47, 83, 52),
                            //Image = global::Лабиринт.Properties.Resources.wall,
                            //SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
                        };

                        panel1.Controls.Add(pic);
                        Pole[i, j] = pic;
                        X += Size;
                    }

                }
                else if ((i > 0) && (i < _Y_ * 2))
                {
                    for (int j = 0; j < (_X_ * 2) + 1; j++)
                    {
                        if ((i % 2 != 0) && (j % 2 != 0))
                        {
                            PictureBox pic = new PictureBox()
                            {
                                Location = new Point(X, Y),
                                Size = new Size(Size, Size),
                                BackColor = Color.White,
                            };
                            panel1.Controls.Add(pic);
                            Pole[i, j] = pic;


                        }
                        else
                        {
                            PictureBox pic = new PictureBox()
                            {
                                Location = new Point(X, Y),
                                Size = new Size(Size, Size),
                                BackColor = Color.FromArgb(47, 83, 52),
                                //Image = global::Лабиринт.Properties.Resources.wall,
                                //SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage,
                            };
                            panel1.Controls.Add(pic);
                            Pole[i, j] = pic;
                        }
                        X += Size;
                    }
                }
                else
                {
                    for (int j = 0; j < (_X_ * 2) + 1; j++)
                    {
                        PictureBox pic = new PictureBox()
                        {
                            Location = new Point(X, Y),
                            Size = new Size(Size, Size),
                            BackColor = Color.FromArgb(47, 83, 52),
                            //Image = global::Лабиринт.Properties.Resources.wall,
                            //SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage,
                        };
                        panel1.Controls.Add(pic);
                        Pole[i, j] = pic;
                        X += Size;
                    }
                }
                Y += Size;

            }


        }

        //пока есть белые клетки на поле
        public bool ProverkaPokaEstSosedi()
        {
            _X_ = Convert.ToInt32(textBox1.Text);
            _Y_ = Convert.ToInt32(textBox2.Text);
            bool rezult = false;
            for (int i = 1; i <= (_Y_ * 2) - 1; i += 2)
            {
                for (int j = 1; j <= (_X_ * 2) - 1; j += 2)
                {

                    if (Pole[i, j].BackColor == Color.White)
                    {
                        rezult = true;
                        break;
                    }
                    else
                        rezult = false;
                }
                if (rezult == true)
                    break;
            }
            return rezult;
        }


        //колличество соседей

        public bool CountSosed(Point p)
        {
            _X_ = Convert.ToInt32(textBox1.Text);
            _Y_ = Convert.ToInt32(textBox2.Text);
            bool rezult = false;
            Point sosed = p; ;
            if ((p.Y + 2 <= (_Y_ * 2) - 1) && (Pole[p.Y + 2, p.X].BackColor == Color.White))
            {
                sosed.Y = p.Y + 2;
                sosed.X = p.X;
                Sosedi.Add(sosed);
                rezult = true;
            }
            if ((p.X + 2 <= (_X_ * 2) - 1) && (Pole[p.Y, p.X + 2].BackColor == Color.White))
            {
                sosed.X = p.X + 2;
                sosed.Y = p.Y;
                Sosedi.Add(sosed);
                rezult = true;
            }
            if ((p.Y - 2 >= 1) && (Pole[p.Y - 2, p.X].BackColor == Color.White))
            {
                sosed.Y = p.Y - 2;
                sosed.X = p.X;
                Sosedi.Add(sosed);
                rezult = true;
            }
            if ((p.X - 2 >= 1) && (Pole[p.Y, p.X - 2].BackColor == Color.White))
            {
                sosed.X = p.X - 2;
                sosed.Y = p.Y;
                Sosedi.Add(sosed);
                rezult = true;
            }
            return rezult;
        }

        //соединение старта и следующего хода
        public void Average()
        {
            if ((SledHod.Y - 2 == start.Y) && (SledHod.X == start.X))
            {
                Pole[SledHod.Y - 1, start.X].BackColor = Color.Black;
            }
            if ((SledHod.X - 2 == start.X) && (SledHod.Y == start.Y))
            {
                Pole[start.Y, SledHod.X - 1].BackColor = Color.Black;
            }
            if ((SledHod.Y + 2 == start.Y) && (SledHod.X == start.X))
            {
                Pole[SledHod.Y + 1, start.X].BackColor = Color.Black;
            }
            if ((SledHod.X + 2 == start.X) && (SledHod.Y == start.Y))
            {
                Pole[start.Y, SledHod.X + 1].BackColor = Color.Black;
            }

        }


        private void BunifuFlatButton1_Click(object sender, EventArgs e)
        {
            Generic();
            //Thread thread = new Thread(Generic);
            //thread.Start();

        }
        int shagI = 0;
        int shagJ = 0;

        async private void BunifuFlatButton2_Click(object sender, EventArgs e)
        {
            random = new Random();
            shagI = 1;
            shagJ = 1;

            while (ProverkaPokaEstSosedi())
            {
                start.X = shagJ;
                start.Y = shagI;
                stacks.Push(start);
                if (CountSosed(start) == true)
                {

                    Pole[start.Y, start.X].BackColor = Color.FromArgb(0, 255, 0);
                    SledHod = Sosedi[random.Next(0, Sosedi.Count)];
                    Pole[start.Y, start.X].BackColor = Color.Black;
                    Average();
                    Pole[SledHod.Y, SledHod.X].BackColor = Color.FromArgb(0, 255, 0);
                    await Task.Delay(1);
                    shagI = SledHod.Y;
                    shagJ = SledHod.X;
                    Sosedi.Clear();
                }
                else
                {

                    while (CountSosed(start) == false)
                    {
                        Pole[start.Y, start.X].BackColor = Color.Black;
                        stacks.Pop();
                        start = stacks.Peek();
                        shagJ = start.X;
                        shagI = start.Y;

                    }

                }

            }
            if (ProverkaPokaEstSosedi() == false)
            {
                start.X = shagJ;
                start.Y = shagI;
                Pole[start.Y, start.X].BackColor = Color.Black;
                Pole[0, 1].BackColor = Color.Black;
                Pole[_Y_ * 2, (_X_ * 2) - 1].BackColor = Color.Black;

            }
        }
        Point moveStart;
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                moveStart = new Point(e.X, e.Y);
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) != 0)
            {
                Point deltaPos = new Point(e.X - moveStart.X, e.Y - moveStart.Y);
                this.Location = new Point(this.Location.X + deltaPos.X, this.Location.Y + deltaPos.Y);

            }
        }

        private void BunifuFlatButton4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        Point Exit;
        async private void BunifuFlatButton3_Click(object sender, EventArgs e)
        {
            stacks.ClearStackk();
            Sosedi.Clear();
            random = new Random();
            shagI = 0;
            shagJ = 1;
            Exit.X = (_X_ * 2) - 1;
            Exit.Y = (_Y_ * 2) - 1;
            List<Point> points;
            while (SledHod != Exit)
            {
                start.X = shagJ;
                start.Y = shagI;
                stacks.Push(start);
                if (SosediStackkPoiscPuti(start) == true)
                {
                    Pole[start.Y, start.X].BackColor = Color.Red;
                    SledHod = Sosedi[random.Next(0, Sosedi.Count)];
                    shagI = SledHod.Y;
                    shagJ = SledHod.X;
                    await Task.Delay(1);
                    Sosedi.Clear();

                }

                else
                {

                    //MessageBox.Show("стоп");
                    while (SosediStackkPoiscPuti(start) == false)
                    {

                        Pole[start.Y, start.X].BackColor = Color.Blue;
                        stacks.Pop();
                        start = stacks.Peek();
                        shagJ = start.X;
                        shagI = start.Y;
                        if (start == Exit)
                        {
                            break;
                        }

                    }
                    if (start == Exit)
                    {
                        break;

                    }

                    //points = stacks.Perebor();
                    //for (int i = 0; i < points.Count; i++)
                    //{
                    //    textBox3.Text += Convert.ToString(points[i].Y);
                    //    textBox3.Text += ",";
                    //    textBox3.Text += Convert.ToString(points[i].X);
                    //    textBox3.Text += Environment.NewLine;
                    //}
                    //textBox4.Text += Convert.ToString(start.Y);
                    //textBox4.Text += ",";
                    //textBox4.Text += Convert.ToString(start.X);
                }

            }

            Pole[_Y_ * 2, (_X_ * 2) - 1].BackColor = Color.Red;
            Pole[SledHod.Y, SledHod.X].BackColor =   Color.Red;
            points = stacks.Perebor();
            for (int i = 0; i < points.Count; i++)
            {
                textBox3.Text += Convert.ToString(points[i].Y);
                textBox3.Text += ",";
                textBox3.Text += Convert.ToString(points[i].X);
                textBox3.Text += Environment.NewLine;
            }
            textBox4.Text += Convert.ToString(start.Y);
            textBox4.Text += ",";
            textBox4.Text += Convert.ToString(start.X);

        }

        //Проверка соседей
        public bool SosediStackkPoiscPuti(Point p)
        {
            bool rezult = false;
            //нижний сосед
            Point sosed = p; ;
            if ((p.Y + 1 <= (_Y_ * 2) - 1) && (Pole[p.Y + 1, p.X].BackColor == Color.Black))
            {
                sosed.Y = p.Y + 1;
                sosed.X = p.X;
                Sosedi.Add(sosed);
                rezult = true;
            }
            if ((p.X + 1 <= (_X_ * 2) - 1) && (Pole[p.Y, p.X + 1].BackColor == Color.Black))
            {
                sosed.X = p.X + 1;
                sosed.Y = p.Y;
                Sosedi.Add(sosed);
                rezult = true;
            }
            if ((p.Y - 1 >= 1) && (Pole[p.Y - 1, p.X].BackColor == Color.Black))
            {
                sosed.Y = p.Y - 1;
                sosed.X = p.X;
                Sosedi.Add(sosed);
                rezult = true;
            }
            if ((p.X - 1 >= 1) && (Pole[p.Y, p.X - 1].BackColor == Color.Black))
            {
                sosed.X = p.X - 1;
                sosed.Y = p.Y;
                Sosedi.Add(sosed);
                rezult = true;
            }

            return rezult;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
