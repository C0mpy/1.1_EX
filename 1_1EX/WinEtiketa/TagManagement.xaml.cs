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
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using _1_1EX.Model;

namespace _1_1EX.WinEtiketa
{

    public partial class TagManagement : Window
    {
        public static ObservableCollection<Etiketa> tags
        {
            get;
            set;
        }
        public static ObservableCollection<Etiketa> displayTable
        {
            get;
            set;
        }
        public List<Etiketa> backup = new List<Etiketa>();
        public bool closeFlag = false;

        public TagManagement(ObservableCollection<Etiketa> e)
        {
            InitializeComponent();
            this.DataContext = this;
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            tags = new ObservableCollection<Etiketa>();
            displayTable = new ObservableCollection<Etiketa>();
            foreach (Etiketa et in e)
            {
                tags.Add(new Etiketa(et.Id, et.Boja, et.Opis));
                backup.Add(new Etiketa(et.Id, et.Boja, et.Opis));
                displayTable.Add(new Etiketa(et.Id, et.Boja, et.Opis));
            }

            if (e.Count != 0)
            {
                dgTags.SelectedIndex = 0;
            }

        }

        private void TagCreateClick(object sender, RoutedEventArgs e)
        {
            var darkwindow = new Window()
            {
                Background = Brushes.Black,
                Opacity = 0.4,
                AllowsTransparency = true,
                WindowStyle = WindowStyle.None,
                WindowState = WindowState.Maximized,
            };
            darkwindow.Show();
            var ew = new _1_1EX.WinEtiketa.EtiketaWin();
            ew.ShowDialog();
            darkwindow.Close();
        }

        private void TagDeleteClick(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < tags.Count; i++)
            {
                if (tags[i].Id == ((Etiketa)dgTags.SelectedItem).Id)
                {
                    tags.RemoveAt(i);
                    break;
                }
            }
            displayTable.Remove((Etiketa)dgTags.SelectedItem);
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            closeFlag = true;
            MainWindow.tags = tags;
            Serializer.SaveEtiketa();
            this.Close();
        }

        private void filterTable(object sender, RoutedEventArgs e)
        {
            displayTable.Clear();
            foreach (Etiketa tr in tags)
            {
                if (tr.Id.ToLower().Contains(searchTextBox.Text.ToLower()))
                {
                    displayTable.Add(tr);
                }
            }
        }

        private void tagEditClick(object sender, RoutedEventArgs e)
        {
            Etiketa tr = dgTags.SelectedItem as Etiketa;
            var darkwindow = new Window()
            {
                Background = Brushes.Black,
                Opacity = 0.4,
                AllowsTransparency = true,
                WindowStyle = WindowStyle.None,
                WindowState = WindowState.Maximized,
            };
            darkwindow.Show();
            var ew = new _1_1EX.WinEtiketa.TagEdit(tr);
            ew.ShowDialog();
            darkwindow.Close();
            if (tags.Count == 1)
            {
                dgTags.SelectedIndex = 0;
            }
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            if (!closeFlag)
            {
                MainWindow.tags.Clear();
                foreach (Etiketa tr in backup)
                {
                    MainWindow.tags.Add(tr);
                }
            }
        }


    }
}
