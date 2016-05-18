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
    public partial class MainWindow : Window, INotifyPropertyChanged
    {

        //ako cemo imati vise mapa da ovde stoji ime aktivne mape?
        public static string active_map;
        
        //i mozda da cuvamo resurse za svaku mapu u hashmap gde je kljuc ime mape?
        public static List<Resurs> resursi = new List<Resurs>();

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

        public MainWindow()
        {

            //WinResurs wr = new WinResurs();
            //wr.Show();

            InitializeComponent();
            this.DataContext = this;
            resurs = new Resurs();
            active_map = "mapa1resursi";
            types = Serializer.LoadTip();
            tags = Serializer.LoadEtiketa();
            resursi = Serializer.ReadResources();
            ucitajResurse();
            picker.SelectedDate = DateTime.Today;

             
        }


        public void ucitajResurse()
        {

            gr1.Children.Clear();
            gr1.Height = 136;
            int br = resursi.Count + 1;
            Image[] carImg = new Image[br];

            MenuItem[] mi = new MenuItem[br];
            Border[] bor = new Border[br];


            ColumnDefinition[] gridCol1 = new ColumnDefinition[br / 5 + 5];
            RowDefinition[] gridRow1 = new RowDefinition[br];

            for (int j = 0; j < 5; j++)
            {
                gridCol1[j] = new ColumnDefinition();
                gridCol1[j].Width = new GridLength(40);


                gr1.ColumnDefinitions.Add(gridCol1[j]);
            }

            int col = -1;
            for (int i = 0; i < br; i++)
            {

                if ((i % 5) == 0)
                {
                    col++;
                    gridRow1[i / 5] = new RowDefinition();
                    //gridRow1[i].Height = new GridLength(40);

                    gridRow1[i / 5].Height = new GridLength(0.5, GridUnitType.Star);
                    gridRow1[i / 5].Height = GridLength.Auto;
                    gr1.RowDefinitions.Add(gridRow1[i / 5]);


                }

                Console.WriteLine(i % 5);
                if (col > 2 & ((i % 5) == 0))
                {
                    gr1.Height += 40;
                }

                carImg[i] = new Image();
                if (i == br - 1)
                {
                    carImg[i].Source = new BitmapImage(new Uri("add2.png", UriKind.RelativeOrAbsolute));
                }
                else
                {
                    
                    carImg[i].Source = new BitmapImage(new Uri(resursi[i].Ikonica, UriKind.RelativeOrAbsolute)); ;

                    int index = i;
                    MenuItem modMenuItem = new MenuItem();
                    modMenuItem.Header = "Modify";
                    modMenuItem.Click += (sender, e) => modifyResourceAction(index);
                    MenuItem delMenuItem = new MenuItem();
                    delMenuItem.Header = "Delete";
                    delMenuItem.Click += (sender, e) => deleteResourceAction(index);
                    ContextMenu cm = new ContextMenu();
                    cm.Items.Add(modMenuItem);
                    cm.Items.Add(delMenuItem);
                    carImg[i].ContextMenu = cm;
                }

                carImg[i].Width = 40;
                carImg[i].Height = 40;

                RenderOptions.SetBitmapScalingMode(carImg[i], BitmapScalingMode.Fant);
                bor[i] = new Border();
                bor[i].Child = carImg[i];
                bor[i].Visibility = System.Windows.Visibility.Visible;
                bor[i].BorderBrush = Brushes.Silver;
                bor[i].BorderThickness = new Thickness(1);
                bor[i].SetValue(Grid.ColumnProperty, i % 5);
                bor[i].SetValue(Grid.RowProperty, col);

                gr1.Children.Add(bor[i]);



            }

        }

        private void modifyResourceAction(int i)
        {
            WinResurs wr = new WinResurs();
            wr.setData(this, resursi[i], i);
            wr.Show();
        }

        private void deleteResourceAction(int i)
        {
            MessageBox.Show("Resource with id " + resursi[i].Id + " has been deleted!");
            resursi.RemoveAt(i);
            ucitajResurse();
            Serializer.WriteResources();
        }

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
                //string startupPath = Environment.CurrentDirectory;
                //System.IO.File.Copy(filename, startupPath.Substring(0, startupPath.Length - 9) + "SlikeResursi\\" + System.IO.Path.GetFileName(filename));
                resurs.Ikonica = filename;
            }
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

        public string Cena
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
            resurs.Obnovljiv = (bool)obnovljiv.IsChecked;
            resurs.Vaznost = (bool)vaznost.IsChecked;
            resurs.Eksploatacija = (bool)eksploatacija.IsChecked;
            resurs.Mera1 = (Mera)Enum.Parse(typeof(Mera), mera.Text);
            resurs.Datum = (DateTime)picker.SelectedDate;
            resursi.Add(resurs);

            MessageBox.Show(resursi.Count.ToString());
            for (int i = 0; i < resursi.Count; i++)
            {
                MessageBox.Show(resursi.ElementAt(i).ToString());
            }

            ucitajResurse();
            Serializer.WriteResources();
            resurs = new Resurs();
            dodajResursFormReset();

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

    }
}
