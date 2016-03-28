using System;
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
using _1_1EX.Model;

namespace _1_1EX
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {

        public static List<Resurs> resursi;

        public MainWindow()
        {
            InitializeComponent();
            resurs = new Resurs();
            resursi = new List<Resurs>();

            this.DataContext = this;

            iscrtajSliku();

            picker.SelectedDate = DateTime.Today;
        }

        public void iscrtajSliku() {
            /*
            Uri myUri = new Uri("map.jpg", UriKind.RelativeOrAbsolute);
            var bitmap = new BitmapImage(myUri);
            myImage.Source = bitmap;*/
            
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
        private Resurs resurs;
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

        public double Cena
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

        void dodajResursFormReset()
        {
            id.Text = "";
            ime.Text = "";
            opis.Text = "";
            frekvencija.SelectedIndex = 0;
            obnovljiv.IsChecked = false;
            vaznost.IsChecked = false;
            eksploatacija.IsChecked = false;
            mera.SelectedIndex = 0;
            cena.Text = "0";
            picker.SelectedDate = DateTime.Today;


        }

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
            //TODO Implementirati dodavanje tipa resursa
            resurs.Tip = new TipResursa();
            resurs.Frekvencija1 = (Frekvencija)Enum.Parse(typeof(Frekvencija), frekvencija.Text);
            //TODO Implementirati dodavanje ikonice
            resurs.Ikonica = "ikonica";
            resurs.Obnovljiv = (bool)obnovljiv.IsChecked;
            resurs.Vaznost = (bool)vaznost.IsChecked;
            resurs.Eksploatacija = (bool)eksploatacija.IsChecked;
            resurs.Mera1 = (Mera)Enum.Parse(typeof(Mera), mera.Text);
            resurs.Datum = (DateTime)picker.SelectedDate;
            //TODO Implementirati dodavanje i odabir etikete
            resurs.dodajEtiketu(new Etiketa());
            resursi.Add(resurs);
            for (int i = 0; i < resursi.Count; i++)
            {
                MessageBox.Show(resursi[i].ToString());
            }
            dodajResursFormReset();
            resurs = new Resurs();
        }

        private void EtiketaClick(object sender, RoutedEventArgs e)
        {
            var ew = new _1_1EX.WinEtiketa.EtiketaWin();
            ew.Show();
        }

    }
}
