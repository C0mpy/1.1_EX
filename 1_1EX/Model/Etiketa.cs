using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace _1_1EX.Model
{
    class Etiketa
    {

        string id;
        Color boja;
        string opis;

        public Etiketa(string Id, Color Boja, string Opis) {
            id = Id;
            boja = Boja;
            opis = Opis;
        }

        public Color Boja
        {
            get { return boja; }
            set { boja = value; }
        }

        public string Opis
        {
            get { return opis; }
            set { opis = value; }
        }

        public string Id
        {
            get { return id; }
            set { id = value; }
        }
    }
}
