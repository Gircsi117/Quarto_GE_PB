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
                    pan.Tag = $"{i}{j}";

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
                    pb.BorderStyle = BorderStyle.FixedSingle;
                    pb.Name = $"{ertekad(sorszam)}";
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
                int rowPos = Convert.ToInt32(pan.Tag.ToString().Substring(0, 1));
                int colPos = Convert.ToInt32(pan.Tag.ToString().Substring(1, 1));
                scan(rowPos, colPos);
            }
        }

        private void startBTN_Click(object sender, EventArgs e)
        {
            general();
            babu_general();
            (sender as Button).Enabled = false;
        }

        private void konv_sor(List<string> list)
        {
            string[,] tulajdonsagok = new string[4, 4];
            
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    tulajdonsagok[i, j] = list[i].Substring(j, 1);
                }
            }

            vizsgal(tulajdonsagok);
        }

        private void vizsgal(string[,] Array)
        {
            bool nyertel = false;

            for (int col = 0; col < 4; col++)
            {
                int db = 0;

                if (db % 4 == 0 && !nyertel)
                {
                    nyertel = true;
                    MessageBox.Show("Nyertél");

                    label1.Text = "Bezárhatod a játékot!";

                    for (int i = 0; i < 4; i++)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            mezo[i, j].Enabled = false;
                        }
                    }
                }
            }
        }

        private void scan(int rowPos, int colPos)
        {
            List<string> list = new List<string>();

            //sor
            for (int col = 0; col < 4; col++)
            {
                if (mezo[rowPos, col].Controls.Count != 0)
                {
                    list.Add(mezo[rowPos, col].Controls[0].Tag.ToString());
                    if (list.Count == 4)
                    {
                        konv_sor(list);
                    }
                }
            }

            //oszlop
            list = new List<string>();
            
            for (int row = 0; row < 4; row++)
            {
                if (mezo[row, colPos].Controls.Count != 0)
                {
                    list.Add(mezo[row, colPos].Controls[0].Tag.ToString());
                    if (list.Count == 4)
                    {
                        konv_sor(list);
                    }
                }
            }

            //átló
            list = new List<string>();
            
            if (colPos == rowPos || colPos == 3 - rowPos)
            {
                for (int i = 0; i < 4; i++)
                {
                    int offset = 3;
                    if(colPos == rowPos)
                    {
                        offset = 0;
                    }

                    if (mezo[i, Math.Abs(offset - i)].Controls.Count != 0)
                    {
                        list.Add(mezo[i, Math.Abs(offset - i)].Controls[0].Tag.ToString());
                        if (list.Count == 4)
                        {
                            konv_sor(list);
                        }
                    }
                }
            }
        }
    }
}
