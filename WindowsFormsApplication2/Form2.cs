using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Program.N =  int.Parse(textBox1.Text);
            Form1 f = new Form1();
            this.Hide();  //ascundem formularul
            f.ShowDialog();
            this.Show();  //afisam formularul
        }
    }
}
