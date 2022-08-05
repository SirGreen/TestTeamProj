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
            Form1 form1 = new Form1();
            form1.creatBtn(this);
        }

    }
}
