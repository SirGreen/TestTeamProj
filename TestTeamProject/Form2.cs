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
        public Form2()
        {
            InitializeComponent();
        }

        static int h = 8, v = 8, bomb = 10;
        int[,] game = new int[h, v];

        private void Form2_Load(object sender, EventArgs e)
        {
            ///create Bomb location

            Random rand = new Random();
            for (int i = 0; i < bomb; i++)
            {
                int x, y;
                x = rand.Next(0, 8);
                y = rand.Next(0, 8);

                game[x, y] = -1;
            }

            ///create Grid button
            for (int i = 0; i < h; i++)
            {
                for (int j = 0; j < v; j++)
                {
                    Button btn = new Button();

                    btn.Location = new Point(i * 50, j * 50);
                    btn.Height = 50;
                    btn.Width = 50;

                    if (game[i, j] == -1) btn.Text = "B";

                    this.Controls.Add(btn);

                    btn.Click += Btn_Click;
                }
            }
        }

        private void Btn_Click(object? sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (game[btn.Location.X, btn.Location.Y] == -1)
            {
                MessageBox.Show("Game Over");
            }
        }
    }
}