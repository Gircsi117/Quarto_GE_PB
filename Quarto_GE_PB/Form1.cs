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
        public static Panel[,] mezo;
        public static List<PictureBox> babuk;
        public static PictureBox kivalasztott;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
        }

        private void general()
        {
            mezo = new Panel[n, n];

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

                    mezo[i, j] = pan;

                    pan.Click += elhelyez;

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

        private void babu_general()
        {
            babuk = new List<PictureBox>() { };

            int meret = babukPANEL.Width / 4;
            int x = 0;
            int y = 0;

            int sorszam = 0;

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    PictureBox pb = new PictureBox();
                    pb.Size = new Size(meret, meret);
                    pb.BackgroundImage = kepek.Images[sorszam];
                    pb.BackgroundImageLayout = ImageLayout.Zoom;
                    pb.Name = $"{ertekad(sorszam)}PBOX";
                    pb.Tag = ertekad(sorszam);
                    babukPANEL.Controls.Add(pb);
                    babuk.Add(pb);
                    pb.Click += kiemel;
                    pb.Location = new Point(x, y);

                    sorszam++;

                    x += meret;
                }
                x = 0;
                y += meret;
            }
        }

        //4 hosszú bináris kód generátor
        private string ertekad(int szam)
        {
            return $"{szam / 8 % 2}{szam / 4 % 2}{szam / 2 % 2}{szam % 2}";
        }

        //Bábú kiválasztása
        private void kiemel(object sender, EventArgs e)
        {
            kivalasztott = sender as PictureBox;
            mutatPBOX.BackgroundImage = kivalasztott.BackgroundImage;
            //babukPANEL.Enabled = false;
        }

        //A kiválasztott bábú elhelyezése
        private void elhelyez(object sender, EventArgs e)
        {
            Panel pan = sender as Panel;

            if (kivalasztott != null && pan.Controls.Count == 0)
            {
                pan.Controls.Add(kivalasztott);
                kivalasztott.Location = new Point(0, 0);
                kivalasztott.Size = new Size(pan.Width, pan.Height);
                kivalasztott.Enabled = false;
                kivalasztott = null;
                mutatPBOX.BackgroundImage = null;
                //babukPANEL.Enabled = true;
            }
        }

        private void startBTN_Click(object sender, EventArgs e)
        {
            general();
            babu_general();
            (sender as Button).Enabled = false;
        }

        private static bool gyozelem_feltetel()
        {
            if (db == 4)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static int[] scan(Panel[,] tabla)
        {
            int db = 0;

            for (int row = 0; row < 4; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    if(mezo[col, row].Controls.Count == 0) { continue; }
                    else if(mezo[col, row].Controls.Count != 0)
                    {
                        db++;

                        if(db == 4)
                        {
                            return new int[2] { col, row };
                        }
                    }                    
                }
            }
            return new int[2] { -1, -1 };
        }
    }
}
