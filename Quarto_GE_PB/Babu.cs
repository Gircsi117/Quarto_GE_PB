using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quarto_GE_PB
{
    class Babu
    {
        private string ertekek;

        public string Ertekek { get => ertekek; set => ertekek = value; }

        public Babu(string ertekek)
        {
            this.ertekek = ertekek;
        }
    }
}
