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
using System.Collections.ObjectModel;
using _1_1EX.Model;
using Microsoft.Win32;

namespace _1_1EX
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class WinResurs : Window, INotifyPropertyChanged
    {

        public MainWindow mw;
        public Resurs backup;

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

        public WinResurs(MainWindow mwin, Resurs r)
        {
            InitializeComponent();
            this.DataContext = this;
            resurs = r;
            tags = MainWindow.tags;
            types = MainWindow.types;
            mw = mwin;
            id.Text = r.Id;
            ime.Text = r.Ime;
            opis.Text = r.Opis;
            //setovat frekvenciju u combobox
            if (r.Frekvencija1.ToString() == "Rare")
            {
                frekvencija.SelectedIndex = 0;
            }
            else if (r.Frekvencija1.ToString() == "Common")
            {
                frekvencija.SelectedIndex = 1;
            }
            else if (r.Frekvencija1.ToString() == "Universal")
            {
                frekvencija.SelectedIndex = 2;
            }
            obnovljiv.IsChecked = r.Obnovljiv;
            vaznost.IsChecked = r.Vaznost;
            eksploatacija.IsChecked = r.Eksploatacija;
            if (r.Mera1.ToString() == "Scoop")
            {
                mera.SelectedIndex = 0;
            }
            else if (r.Mera1.ToString() == "Barrel")
            {
                mera.SelectedIndex = 1;
            }
            else if (r.Mera1.ToString() == "Ton")
            {
                mera.SelectedIndex = 2;
            }
            else if (r.Mera1.ToString() == "Kilogram")
            {
                mera.SelectedIndex = 3;
            }
            cena.Text = (r.Cena).ToString();

            backup = new Resurs(r.Id, r.Ime, r.Opis, r.Tip, r.Frekvencija1, r.Ikonica, r.Obnovljiv, r.Vaznost,
                r.Eksploatacija, r.Mera1, r.Cena, r.Datum, r.Etikete1);
            MessageBox.Show(resurs.ToString());
            removeResourceFromList();
            picker.SelectedDate = r.Datum;
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
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
            this.Title = "Modify Resource";

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

        private void odaberiIkonicu(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "PNG Files (*.png)|*.png|JPEG Files (*.jpeg)|*.jpeg|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";

            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = openFileDialog.ShowDialog();


            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                string filename = openFileDialog.FileName;
                string startupPath = Environment.CurrentDirectory;
                System.IO.File.Copy(filename, startupPath.Substring(0, startupPath.Length - 9) + "SlikeResursi\\" + System.IO.Path.GetFileName(filename));
                resurs.Ikonica = filename;
            }
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

        public void removeResourceFromList()
        {
            for (int i = 0; i < MainWindow.resursi.Count; i++)
            {
                if (backup.Id == MainWindow.resursi[i].Id)
                {
                    MainWindow.resursi.RemoveAt(i);
                    break;
                }
            }
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.resursi.Add(backup);
            this.Close();
        }

        void modify_Click(object sender, RoutedEventArgs e)
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
            resurs.Obnovljiv = (bool)obnovljiv.IsChecked;
            resurs.Vaznost = (bool)vaznost.IsChecked;
            resurs.Eksploatacija = (bool)eksploatacija.IsChecked;
            resurs.Mera1 = (Mera)Enum.Parse(typeof(Mera), mera.Text);
            resurs.Datum = (DateTime)picker.SelectedDate;

            MainWindow.resursi.Add(resurs);
            MessageBox.Show("Resource has been modifyed!");
            mw.ucitajResurse();
            Serializer.WriteResources();
            this.Close();
        }

    }
}
