using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace _1_1EX.Model
{
    public class TipResursa : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

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
            get
            {
                return id;
            }
            set
            {
                if (value != id)
                {
                    id = value;
                    OnPropertyChanged("Id");
                }
            }
        }
        public string Ime
        {
            get
            {
                return ime;
            }
            set
            {
                if (value != ime)
                {
                    ime = value;
                    OnPropertyChanged("Ime");
                }
            }
        }
        public string Ikonica
        {
            get
            {
                return ikonica;
            }
            set
            {
                if (value != ikonica)
                {
                    ikonica = value;
                    OnPropertyChanged("Ikonica");
                }
            }
        }
        public string Opis
        {
            get
            {
                return opis;
            }
            set
            {
                if (value != opis)
                {
                    opis = value;
                    OnPropertyChanged("Opis");
                }
            }
        }

        public override string ToString()
        {
            return String.Format("[Id: {0}, Ime: {1}, Ikonica: {2}, Opis:{3}]", Id, Ime, Ikonica, Opis);
        }
    }
}
