namespace TestTeamProject
{
    public partial class Form1 : Form
    {
       public Form1()
       {
            InitializeComponent();
       }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        // Khoi tao ban dau
        public int h = 8, v = 8, bomb = 10;

        // Beginner Mode
        private void button1_Click(object sender, EventArgs e)
        {
            h = 8; v = 8; bomb = 10;
            ChngValCus(h, v, bomb);
            NotEnbl();
            ClChng();
            button1.BackColor = Color.PaleGreen;
        }

        // Intermediate Mode
        private void button2_Click(object sender, EventArgs e)
        {
            h = 16; v = 16; bomb = 40;
            ChngValCus(h, v, bomb);
            NotEnbl();
            ClChng();
            button2.BackColor = Color.LightSkyBlue;
        }
        
        // Expert Mode
        private void button3_Click(object sender, EventArgs e)
        {
            h = 30; v = 16; bomb = 99;
            ChngValCus(h, v, bomb);
            NotEnbl();
            ClChng();
            button3.BackColor = Color.Orange;
        }

        // Custom Mode
        private void button4_Click(object sender, EventArgs e)
        {
            numericUpDown1.Enabled = true;
            numericUpDown2.Enabled = true;
            numericUpDown3.Enabled = true;
            ClChng();
            button4.BackColor = Color.Pink;
        }

        // Start Game
        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 form2 = new Form2();

            form2.value1 = h;
            form2.value2 = v;
            form2.value3 = bomb;

            form2.ShowDialog();
            this.Show();
        }

        // Change Width --> New Bomb.maximum
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            h = ((int)numericUpDown1.Value);
            numericUpDown3.Maximum = h * v - 1;
        }        

        // Change Height --> New Bomb.maximum
        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            v = ((int)numericUpDown2.Value);
            numericUpDown3.Maximum = h * v - 1;
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            bomb = ((int)numericUpDown3.Value);
        }

        // Huy Enable Custom h,v,bomb 
        private void NotEnbl()
        {
            numericUpDown1.Enabled = false;
            numericUpDown2.Enabled = false;
            numericUpDown3.Enabled = false;
        }

        // Doi hien thi so cua Custom khi ko chon Custom Mode
        private void ChngValCus(int a, int b, int c)
        {
            numericUpDown1.Value = a;
            numericUpDown2.Value = b;   
            numericUpDown3.Value = c;
        }

        private void ClChng()
        {
            button1.BackColor = SystemColors.InactiveCaption;
            button2.BackColor = SystemColors.InactiveCaption;
            button3.BackColor = SystemColors.InactiveCaption; 
            button4.BackColor = SystemColors.InactiveCaption;
        }
    }
}