﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestTeamProject
{

    public partial class Form2 : Form
    {
        ///khai bao bien
        #region Bien
        static int maxh = 31, maxv = 31;
        public static int h, v, bomb;

        static int fx, fy;
        static bool firstclick = false;

        static int[,] game = new int[maxh, maxv];

        static bool[,] isVisit = new bool[maxh, maxv];

        Button[,] b = new Button[maxh, maxv];

        Random rand = new Random();

        int[] dx = new int[8] { -1, -1, -1, 0, 1, 1, 1, 0 };
        int[] dy = new int[8] { -1, 0, 1, 1, 1, 0, -1, -1 };
        bool[,] rightChk = new bool[maxh, maxv];
        #endregion

        ///Truyen du lieu
        #region Truyen du lieu
        public int value1
        {
            get { return h; }
            set { h = value; }
        }

        public int value2
        {
            get { return v; }
            set { v = value; }
        }

        public int value3
        {
            get { return bomb; }
            set { bomb = value; }
        }
        #endregion

        public Form2()
        {
            InitializeComponent();
            Initgame();
        }

        private void ColorOfNum(Button btn, int cl)
        {
            switch (cl)
            {
                case 1:
                    btn.ForeColor = Color.Blue;
                    break;
                case 2:
                    btn.ForeColor = Color.Green;
                    break;
                case 3:
                    btn.ForeColor = Color.Red;
                    break;
                case 4:
                    btn.ForeColor = Color.BlueViolet;
                    break;
                case 5:
                    btn.ForeColor = Color.Brown;
                    break;
                case 6:
                    btn.ForeColor = Color.DarkOliveGreen;
                    break;
                case 7:
                    btn.ForeColor = Color.Crimson;
                    break;
                default:
                    btn.ForeColor = Color.CadetBlue;
                    break;
            }
        }

        private bool isValid(int x, int y, int i)
        {
            return (x + dx[i] >= 0 && x + dx[i] < h && y + dy[i] >= 0 && y + dy[i] < v);
        }

        private void BombCount(int x, int y)
        {
            for (int i = 0; i < 8; i++)
            {
                if (isValid(x, y, i) && game[x + dx[i], y + dy[i]] != -1)
                    game[x + dx[i], y + dy[i]]++;
            }
        }

        private void Initgame()
        {
            ///Init game
            firstclick = true;
            for (int i = 0; i < h; i++)
            {
                for (int j = 0; j < v; j++)
                {
                    game[i, j] = 0;
                    isVisit[i, j] = false;
                }
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            ///timer Location

            label1.Location = new Point(h * 50,0);
            label2.Location = new Point(h * 50, 50);

            ///create Grid button

            for (int i = 0; i < h; i++)
            {
                for (int j = 0; j < v; j++)
                {
                    b[i, j] = new Button();

                    b[i, j].Name = i.ToString() + "_" + j.ToString();
                    b[i, j].Location = new Point(i * 50, j * 50);
                    b[i, j].Height = 50;
                    b[i, j].Width = 50;
                    b[i, j].Font = new Font(b[i, j].Font.Name, 10);
                    b[i, j].BackColor = Color.White;

                    this.Controls.Add(b[i, j]);

                    b[i, j].Click += Btn_Click;
                    b[i, j].MouseDown += Btn_RightClick;

                }
            }

        }

        private string Time(int x)
        {
            string s = "";
            if (x > 9) s = x.ToString();
            else s = "0" + x.ToString();

            return s;
        }

        int sec = 0, min = 0, hour = 0;

        private void timer1_Tick(object sender, EventArgs e)
        {
            sec++;
            if(sec==60)
            {
                min++;
                sec = 0;
                if (min == 60)
                {
                    hour++;
                    min = 0;
                }
            }

            if (hour > 0) label2.Text = Time(hour) + ":" + Time(min) + ":" + Time(sec);
            else label2.Text = Time(min) + ":" + Time(sec);

        }

        ///Show the number of bombs in each cell
        private void Loan(int x, int y)
        {
            if (isVisit[x, y]) return;

            isVisit[x, y] = true;

            b[x, y].BackColor = Color.Silver;

            if (game[x, y] != 0)
            {
                ColorOfNum(b[x, y], game[x, y]);
                b[x, y].Text = game[x, y].ToString();
            }

            for (int i = 0; i < 8; i++)
            {
                if (isValid(x, y, i) && !rightChk[x + dx[i], y + dy[i]])
                {
                    if (game[x, y] == 0)
                    {
                        if (game[x + dx[i], y + dy[i]] != -1)
                        {
                            b[x + dx[i], y + dy[i]].BackColor = Color.Silver;

                            if (game[x + dx[i], y + dy[i]] != 0)
                            {
                                isVisit[x + dx[i], y + dy[i]] = true;
                                ColorOfNum(b[x + dx[i], y + dy[i]], game[x + dx[i], y + dy[i]]);
                                b[x + dx[i], y + dy[i]].Text = game[x + dx[i], y + dy[i]].ToString();
                            }
                        }

                    }

                    if (game[x + dx[i], y + dy[i]] == 0 && !isVisit[x + dx[i], y + dy[i]])
                        Loan(x + dx[i], y + dy[i]);

                }
            }
        }

        private void Depict()
        {
            for (int i = 0; i < h; i++)
            {
                for (int j = 0; j < v; j++)
                {
                    if (game[i, j] == -1) b[i, j].Text = "B";
                    else
                    {
                        if (game[i, j] != 0)
                        {
                            ColorOfNum(b[i, j], game[i, j]);
                            b[i, j].Text = game[i, j].ToString();
                        }
                        b[i, j].BackColor = Color.Silver;
                    }

                }
            }
        }

        private void CreateBombLocation()
        {
            int x, y;
            x = rand.Next(0, h);
            y = rand.Next(0, v);

            while (game[x, y] == -1 || (x == fx && y == fy))
            {
                x = rand.Next(0, h);
                y = rand.Next(0, v);
            }

            BombCount(x, y);
            game[x, y] = -1;

        }

        private void GameOperation(int x, int y)
        {
            if (game[x, y] == -1) ///Lose Game
            {
                b[x, y].Text = "B";
                timer1.Enabled = false;
                MessageBox.Show("Game Over");
                Depict();
            }
            else Loan(x, y);
        }

        private void Btn_Click(object? sender, EventArgs e)
        {
            Button btn = sender as Button;

            int x = 0, y = 0;

            ///Xu li xau

            string s = btn.Name, s1 = "";

            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == '_')
                {
                    x = Convert.ToInt32(s1);
                    s1 = "";
                }
                else s1 += s[i];
            }

            y = Convert.ToInt32(s1);

            ///firstclick
            if (firstclick)
            {
                firstclick = false;

                ///start timer
                timer1.Enabled = true;

                fx = x;
                fy = y;

                for (int i = 0; i < bomb; i++)
                {
                    CreateBombLocation();
                }
            }

            GameOperation(x, y);
        }

        private void Btn_RightClick(object? sender, MouseEventArgs e)
        {
            Button btn = sender as Button;

            if (e.Button != MouseButtons.Right) return;

            int x = 0, y = 0;

            ///Xu li xau

            string s = btn.Name, s1 = "";

            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == '_')
                {
                    x = Convert.ToInt32(s1);
                    s1 = "";
                }
                else s1 += s[i];
            }

            y = Convert.ToInt32(s1);

            // Right Click To Open Flag 
            if (btn.BackColor != Color.Silver)
            {
                if (rightChk[x,y] == false)
                {
                    btn.BackColor = Color.OrangeRed;
                    rightChk[x, y] = true;
                    btn.Click -= Btn_Click;
                }
                else
                {
                    btn.BackColor = Color.White;
                    rightChk[x, y] = false;
                    btn.Click += Btn_Click;
                }
      
            }
            else /// Right Click To Open Remaining Cells
            {
                ///Check for enough flags

                int flag = 0;
                for (int i = 0; i < 8; i++)
                {
                    if (isValid(x, y, i) && rightChk[x + dx[i], y + dy[i]]) flag++;
                }

                ///Open cells
                
                if(flag == game[x,y])
                {
                    for (int i = 0; i < 8; i++) 
                    {
                        if (isValid(x, y, i) && !isVisit[x + dx[i], y + dy[i]] 
                            && !rightChk[x + dx[i],y+dy[i]]) 
                            GameOperation(x + dx[i], y + dy[i]);
                    }    
                }
            }
        }
    }
}