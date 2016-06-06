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
using System.Data;
using _1_1EX.Model;

namespace _1_1EX.WinTip
{

    public partial class TypeManagement : Window
    {
        public static ObservableCollection<TipResursa> types
        {
            get;
            set;
        }
        public static ObservableCollection<TipResursa> displayTable
        {
            get;
            set;
        }
        public List<TipResursa> backup = new List<TipResursa>();
        public bool closeFlag = false;

        public TypeManagement(ObservableCollection<TipResursa> t)
        {
            InitializeComponent();
            this.DataContext = this;
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            types = new ObservableCollection<TipResursa>();
            displayTable = new ObservableCollection<TipResursa>();
            foreach (TipResursa tr in t)
            {
                types.Add(new TipResursa(tr.Id, tr.Ime, tr.Ikonica, tr.Opis));
                backup.Add(new TipResursa(tr.Id, tr.Ime, tr.Ikonica, tr.Opis));
                displayTable.Add(new TipResursa(tr.Id, tr.Ime, tr.Ikonica, tr.Opis));
            }

            if (t.Count != 0)
            {
                dgTypes.SelectedIndex = 0;
            }

        }

        private void TypeCreateClick(object sender, RoutedEventArgs e)
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
            var ew = new _1_1EX.WinTip.WinTip();
            ew.ShowDialog();
            darkwindow.Close();
        }

        private void TypeDeleteClick(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < types.Count; i++)
            {
                if (types[i].Id == ((TipResursa)dgTypes.SelectedItem).Id)
                {
                    types.RemoveAt(i);
                    break;
                }
            }
            displayTable.Remove((TipResursa)dgTypes.SelectedItem);
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            closeFlag = true;
            MainWindow.types = types;
            Serializer.SaveTip();
            this.Close();
        }

        private void filterTable(object sender, RoutedEventArgs e)
        {
            displayTable.Clear();
            foreach (TipResursa tr in types)
            {
                if (tr.Id.ToLower().Contains(searchTextBox.Text.ToLower()))
                {
                    displayTable.Add(tr);
                }
            }
        }

        private void typeEdit_Click(object sender, RoutedEventArgs e)
        {
            TipResursa tr = dgTypes.SelectedItem as TipResursa;
            var darkwindow = new Window()
            {
                Background = Brushes.Black,
                Opacity = 0.4,
                AllowsTransparency = true,
                WindowStyle = WindowStyle.None,
                WindowState = WindowState.Maximized,
            };
            darkwindow.Show();
            var ew = new _1_1EX.WinTip.TypeEdit(tr);
            ew.ShowDialog();
            darkwindow.Close();
            if (types.Count == 1)
            {
                dgTypes.SelectedIndex = 0;
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
                MainWindow.types.Clear();
                foreach (TipResursa tr in backup)
                {
                    MainWindow.types.Add(tr);
                }
            }
        }
    }
}

