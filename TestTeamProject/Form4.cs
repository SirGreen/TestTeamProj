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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ///restart

            Form2 form2 = new Form2();
            form2.isRestart = true;

            this.Close();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ///new game
            Form2 form2 = new Form2();
            form2.isNewGm = true;
           
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.isMenu = true;

            this.Close();
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }
    }
}
