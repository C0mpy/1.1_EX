﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Collections.ObjectModel;
using _1_1EX.Model;

namespace _1_1EX
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class WinResurs : Window, INotifyPropertyChanged
    {

        //ako cemo imati vise mapa da ovde stoji ime aktivne mape?
        public static string active_map;

        //i mozda da cuvamo resurse za svaku mapu u hashmap gde je kljuc ime mape?
        public int index;
        public MainWindow mw;

        public static ObservableCollection<TipResursa> types
        {
            get;
            set;
        }

        public static ObservableCollection<Etiketa> tags
        {
            get;
            set;
        }

        public WinResurs()
        {
            InitializeComponent();
            this.DataContext = this;
            resurs = new Resurs();
            types = Serializer.LoadTip();
            tags = Serializer.LoadEtiketa();
            picker.SelectedDate = DateTime.Today;
        }

       

        private void DatePicker_SelectedDateChanged(object sender,
        SelectionChangedEventArgs e)
        {
            // ... Get DatePicker reference.
            var picker = sender as DatePicker;

            // ... Get nullable DateTime from SelectedDate.
            DateTime? date = picker.SelectedDate;



            if (date == null)
            {
                // ... A null object.
                this.Title = "No date";
            }
            else
            {
                // ... No need to display the time.
                this.Title = date.Value.ToShortDateString();
            }


        }

        private void title(object sender,
        ContextMenuEventArgs e)
        {
            this.Title = "das";

        }

        #region NotifyProperties
        public static Resurs resurs;
        public string Id
        {
            get
            {
                return resurs.Id;
            }
            set
            {
                if (value != resurs.Id)
                {
                    resurs.Id = value;
                    OnPropertyChanged("Id");
                }
            }
        }

        public String Cena
        {
            get
            {
                return resurs.Cena;
            }
            set
            {
                if (value != resurs.Cena)
                {
                    resurs.Cena = value;
                    OnPropertyChanged("Cena");
                }
            }
        }

        public string Ime
        {
            get
            {
                return resurs.Ime;
            }
            set
            {
                if (value != resurs.Ime)
                {
                    resurs.Ime = value;
                    OnPropertyChanged("Ime");
                }
            }
        }

        #endregion

        #region PropertyChangedNotifier
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        void dodaj_Click(object sender, RoutedEventArgs e)
        {
            if (resurs.Id == "" || resurs.Ime == "")
            {
                id.Text = "1";
                id.Text = "";
                ime.Text = "1";
                ime.Text = "";
                return;
            }

            resurs.Ime = ime.Text;
            resurs.Opis = opis.Text;
            resurs.Frekvencija1 = (Frekvencija)Enum.Parse(typeof(Frekvencija), frekvencija.Text);
            resurs.Ikonica = "ikonica";
            resurs.Obnovljiv = (bool)obnovljiv.IsChecked;
            resurs.Vaznost = (bool)vaznost.IsChecked;
            resurs.Eksploatacija = (bool)eksploatacija.IsChecked;
            resurs.Mera1 = (Mera)Enum.Parse(typeof(Mera), mera.Text);
            resurs.Datum = (DateTime)picker.SelectedDate;
            
            MessageBox.Show("Resource with id " + index + " has been modifyed!");
            MainWindow.resursi[index] = resurs;
            mw.ucitajResurse();
            Serializer.WriteResources();
        }

        private void EtiketaClick(object sender, RoutedEventArgs e)
        {
            var ew = new _1_1EX.WinTag.TagManagement(tags, resurs);
            ew.Show();
        }

        private void TypeClick(object sender, RoutedEventArgs e)
        {
            var ew = new _1_1EX.WinTip.TypeManagement(types, resurs);
            ew.Show();
        }

        public void setData(MainWindow mwin, Resurs r, int i) {

            mw = mwin;
            index = i;
            
            id.Text = r.Id;
            ime.Text = r.Ime;
            opis.Text = r.Opis;
            MessageBox.Show(r.Cena.ToString());
            resurs.Ikonica = r.Ikonica;
            
            //setovat frekvenciju u combo box

            obnovljiv.IsChecked = r.Obnovljiv;
            vaznost.IsChecked = r.Vaznost;
            eksploatacija.IsChecked = r.Eksploatacija;
            cena.Text = (r.Cena).ToString();

        }

    }
}
