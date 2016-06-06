using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows;
using System.ComponentModel;

namespace _1_1EX.Model
{

    public class Etiketa : INotifyPropertyChanged
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
        public Color Boja
        {
            get
            {
                return boja;
            }
            set
            {
                if (value != boja)
                {
                    boja = value;
                    OnPropertyChanged("Boja");
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
            return String.Format("Id: {0}, Boja: {1}, Opis: {2}", Id, Boja.ToString(), Opis);
        }
    }
}
