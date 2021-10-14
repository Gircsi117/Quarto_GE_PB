using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quarto_GE_PB
{
    public partial class Form1 : Form
    {
        public static int n = 4;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            general();
        }

        private void general()
        {
            int szeles = 400 / n;
            int x = 12;
            int y = 12;

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Panel pan = new Panel();
                    alapPANEL.Controls.Add(pan);
                    pan.Location = new Point(x, y);
                    pan.Size = new Size(szeles, szeles);
                    pan.BorderStyle = BorderStyle.FixedSingle;
                    pan.BackColor = Color.Gray;

                    x += szeles;
                }
                y += szeles;
                x = 12;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Form2 fr = new Form2();
            fr.Show();
        }
    }
}
