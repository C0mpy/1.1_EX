using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _1_1EX.Model
{
    public class TipResursa
    {

        string id;
        string ime;
        string ikonica;
        string opis;

        public TipResursa(string Id, string Ime, string Ikonica, string Opis) {
            id = Id;
            ime = Ime;
            ikonica = Ikonica;
            opis = Opis;
        }

        public TipResursa()
        {
            Id = "";
            Ime = "";
            Ikonica = "";
            Opis = "";
        }

        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Ime
        {
            get { return ime; }
            set { ime = value; }
        }

        public string Ikonica
        {
            get { return ikonica; }
            set { ikonica = value; }
        }

        public string Opis
        {
            get { return opis; }
            set { opis = value; }
        }

        public override string ToString()
        {
            return String.Format("[Id: {0}, Ime: {1}, Ikonica: {2}, Opis:{3}]", Id, Ime, Ikonica, Opis);
        }
    }
}
