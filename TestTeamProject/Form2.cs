using System;
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
        public static int h, v, bomb, winChk;

        static int fx, fy;
        int sec = 0, min = 0, hour = 0, flags = 0;

        static bool firstclick = false, res = false, menu = false, isLose = false;

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

        public bool isRestart
        {
            get { return res; }
            set { res = value; }
        }

        public bool isMenu
        {
            get { return menu; }
            set { menu = value; }
        }

        public bool isNewGm
        {
            get { return isNewGm; }
            set { firstclick = value; }
        }

        #endregion


        public Form2()
        {
            InitializeComponent();
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

            isLose = false;

            sec = 0; min = 0; hour = 0;

            for (int i = 0; i < h; i++)
            {
                for (int j = 0; j < v; j++)
                {
                    game[i, j] = 0;
                    isVisit[i, j] = false;
                    rightChk[i, j] = false;
                }
            }
        }

        private void Create()
        {
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
                    b[i, j].Font = new Font(b[i, j].Font.Name, 10, FontStyle.Bold);
                    b[i, j].BackColor = Color.White;

                    panel1.Controls.Add(b[i, j]);

                    b[i, j].MouseDown += Btn_Click;
                    b[i, j].MouseDown += Btn_RightClick;

                }
            }
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            Initgame();

            ///timer Location

            label1.Location = new Point(h*50+25+42, 0);
            label2.Location = new Point(h*50+25+41, 50);

            // Bomb counter Location
            label3.Location = new Point(h*50+25, 100);
            label4.Location = new Point(h*50+25+60, 150);

            ///Pause button Location

            button1.Location = new Point(h*50+25, 200);

            ///Menu button Location

            button2.Location = new Point(h*50+25, 250);

            ///Restart button Location

            button3.Location = new Point(h*50+25, 350);

            ///New Game button Location
            button4.Location = new Point(h*50+25, 300);

            // Set Value
            flags = bomb;
            label4.Text = flags.ToString();
            winChk = h * v - bomb;

            Create();
        }

        private string Time(int x)
        {
            string s = "";
            if (x > 9) s = x.ToString();
            else s = "0" + x.ToString();

            return s;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            sec++;
            if (sec == 60)
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

        /// Pause/ Resume
        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            this.Hide();

            Form3 form3 = new Form3();
            form3.ShowDialog();

            this.Show();
            timer1.Start();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        DateTime timespam;

        void Restart()
        {
            if ((DateTime.Now - timespam).Ticks < 5000000) return;
            timespam=DateTime.Now;
            res = true;
            isLose = false;

            //reset timer
            timer1.Stop();
            sec = 0; min = 0; hour = 0;
            label2.Text = "00:00";
            
            //init value

            flags = bomb;
            label4.Text = flags.ToString();
            winChk = h * v - bomb;

            ///Init Restart

            for (int i = 0; i < h; i++)
            {
                for (int j = 0; j < v; j++)
                {
                    //init button
                    /*b[i, j].BackColor = Color.White;
                    b[i, j].Text = "";
                    b[i, j].ForeColor = Color.Black;*/

                    panel1.Controls.Remove(b[i, j]);
                    
                    //init array
                    isVisit[i, j] = false;
                    rightChk[i, j] = false;
                }
            }
            Create();
            this.Refresh();
        }

        void NewGame()
        {
            if ((DateTime.Now - timespam).Ticks < 5000000) return;
            timespam = DateTime.Now;
            firstclick = true;
            isLose = false;

            //reset timer
            timer1.Stop();
            sec = 0; min = 0; hour = 0;
            label2.Text = "00:00";

            //init value
            flags = bomb;
            label4.Text = flags.ToString();
            winChk = h * v - bomb;

            //init New Game
            for (int i = 0; i < h; i++)
            {
                for (int j = 0; j < v; j++)
                {
                    game[i, j] = 0;
                    isVisit[i, j] = false;
                    rightChk[i, j] = false;
                    panel1.Controls.Remove(b[i, j]);
                }
            }
            Create();
            this.Refresh();
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            button3.Enabled = false;
            Restart();
            await Task.Delay(444);
            button3.Enabled = true;
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            button4.Enabled = false;
            NewGame();
            await Task.Delay(444);
            button4.Enabled = true;
        }

        ///Show the number of bombs in each cell
        private void Loan(int x, int y)
        {
            if (isVisit[x, y]) return;

            isVisit[x, y] = true;

            winChk--;

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
                                if (!isVisit[x + dx[i], y + dy[i]]) winChk--;
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

        private void GameOption(bool WoL)
        {

            Form4 form4 = new Form4();

            form4.label1.Text = WoL ? "You Win" : "You Lose";

            form4.ShowDialog();

            if(res)
            {
                Restart();
            }

            if (firstclick)
            {
                NewGame();
            }

            if (menu)
            {
                menu = false;
                this.Close();
            }

        }

        private void Lose()
        {
            timer1.Enabled = false;

            Depict();

            GameOption(false);
        }

        private void Win()
        {
            timer1.Enabled = false;

            flags = 0;
            label4.Text = flags.ToString();

            Depict();

            GameOption(true);
            
        }

        private void GameOperation(int x, int y)
        {
            if (game[x, y] == -1) ///Lose Game
            {
                isLose = true;
            }
            else
            {
                Loan(x, y);
            }
        }

        private void Btn_Click(object? sender, MouseEventArgs e)
        {
            Button btn = sender as Button;

            if (e.Button != MouseButtons.Left) return;

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
            else if (res)
            {
                timer1.Start(); //timer start after restart
                res = false;
            }
            GameOperation(x, y);

            if (winChk == 0) Win();
            else if (isLose) Lose();
        }

        private void Btn_RightClick(object? sender, MouseEventArgs e)
        {
            Button btn = sender as Button;

            if (e.Button != MouseButtons.Right) return;
            if (firstclick)
            {
                timer1.Start();
            }
            if (res)
            {
                timer1.Start();
                res = false;
            }

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
                if (rightChk[x, y] == false)
                {
                    btn.BackColor = Color.OrangeRed;
                    rightChk[x, y] = true;
                    btn.MouseDown -= Btn_Click;

                    flags--;
                    label4.Text = flags.ToString();
                }
                else
                {
                    btn.BackColor = Color.White;
                    rightChk[x, y] = false;
                    btn.MouseDown += Btn_Click;

                    flags++;
                    label4.Text = flags.ToString();
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

                if (flag >= game[x, y])
                {
                    for (int i = 0; i < 8; i++)
                    {
                        if (isValid(x, y, i) && !isVisit[x + dx[i], y + dy[i]]
                            && !rightChk[x + dx[i], y + dy[i]])
                        {
                            GameOperation(x + dx[i], y + dy[i]);
                            
                            if (winChk == 0)    // Win Game
                            {
                                Win();
                                break;
                            }
                            else if(isLose)
                            {
                                Lose();
                                break;
                            }
                        }
                    }
                }
            }
        }
    }
}