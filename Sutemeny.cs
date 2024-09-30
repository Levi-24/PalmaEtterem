using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PalmaEtterem
{
    class Sutemeny
    {
        public string Nev { get; set; }
        public string Tipus { get; set; }
        public bool Dijazott { get; set; }
        public int Ar { get; set; }
        public string Egyseg { get; set; }

        public Sutemeny(string s)
        {
            var darabok = s.Split(';');
            Nev = darabok[0];
            Tipus = darabok[1];
            Dijazott = bool.Parse(darabok[2]);
            Ar = int.Parse(darabok[3]);
            Egyseg = darabok[4];
        }
    }
}
