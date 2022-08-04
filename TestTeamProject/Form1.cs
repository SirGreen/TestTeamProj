namespace TestTeamProject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //creatBtn(this);
            List<int> un = new List<int>();
            int h = 8, v = 8;
            un.Add(h);
            MessageBox.Show(un[0].ToString());
            for (int i = 0; i < h; i++)
                for (int j = 0; j < v; j++)
                {
                    Button btn = new Button()
                    {
                        Location = new Point(i * 20, j * 20),
                        Height = 20,
                        Width = 20,
                    };
                    this.Controls.Add(btn);
            }
            
        }

        int i = 0;
        public void creatBtn(Form f)
        {
            Button button = new Button();
            f.Controls.Add(button);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();            
            form2.ShowDialog();
        }
        //static int[,] o = new int[12,2];
        
        int[,] p = new int[2, 2];
        private void Form1_DoubleClick(object sender, EventArgs e)
        {
            btn2.Text = "btn2";
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            MessageBox.Show(rnd.Next(0, 100).ToString());
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}