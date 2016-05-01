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

        //ako cemo imati vise mapa da ovde stoji ime aktivne mape?
        public static string active_map;
        
        //i mozda da cuvamo resurse za svaku mapu u hashmap gde je kljuc ime mape?
        public static List<Resurs> resursi = new List<Resurs>();

        //ovo je isto za sve mape jel?
        public static List<Etiketa> etikete = new List<Etiketa>();
        public static List<TipResursa> tipovi = new List<TipResursa>();

        public MainWindow()
        {
            InitializeComponent();
            resurs = new Resurs();
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
            resurs.Frekvencija1 = (Frekvencija)Enum.Parse(typeof(Frekvencija), frekvencija.Text);
            //TODO Implementirati dodavanje ikonice
            resurs.Ikonica = "ikonica";
            resurs.Obnovljiv = (bool)obnovljiv.IsChecked;
            resurs.Vaznost = (bool)vaznost.IsChecked;
            resurs.Eksploatacija = (bool)eksploatacija.IsChecked;
            resurs.Mera1 = (Mera)Enum.Parse(typeof(Mera), mera.Text);
            resurs.Datum = (DateTime)picker.SelectedDate;
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var wt = new _1_1EX.WinTip.WinTip();
            wt.Show();
        }

    }
}
