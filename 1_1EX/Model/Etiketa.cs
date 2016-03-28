using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows;

namespace _1_1EX.Model
{
    
    public class Etiketa
    {

        string id;
        Color boja;
        string opis;

        public Etiketa(string Id, Color Boja, string Opis) {
            id = Id;
            boja = Boja;
            opis = Opis;
        }

        public Etiketa() 
        {
            Id = "";
            boja = Color.FromRgb(255, 255, 255);
            Opis = "opisEtikete";
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

        public override string ToString()
        {
            return String.Format("Id: {0}, Boja: {1}, Opis: {2}", Id, Boja.ToString(), Opis);
        }
    }
}
