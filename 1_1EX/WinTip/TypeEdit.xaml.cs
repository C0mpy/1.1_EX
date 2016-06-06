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
using _1_1EX.Model;
using System.ComponentModel;
using Microsoft.Win32;

namespace _1_1EX.WinTip
{
    /// <summary>
    /// Interaction logic for TypeEdit.xaml
    /// </summary>
    public partial class TypeEdit : Window
    {
        TipResursa backup;
        String icon;
        bool closeFlag = false;

        public TypeEdit(TipResursa r)
        {
            InitializeComponent();
            this.DataContext = this;
            backup = new TipResursa(r.Id, r.Ime, r.Ikonica, r.Opis);
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            removeTypeFromList();
            Id = r.Id;
            tipime.Text = backup.Ime;
            tipopis.Text = backup.Opis;
            iconDisplay.Source = new BitmapImage(new Uri(backup.Ikonica));
            icon = r.Ikonica;
        }


        public void removeTypeFromList()
        {
            for (int i = 0; i < TypeManagement.types.Count; i++)
            {
                if (backup.Id == TypeManagement.types[i].Id)
                {
                    TypeManagement.types.RemoveAt(i);
                    TypeManagement.displayTable.RemoveAt(i);
                    break;
                }
            }
        }

        private void iconSelect(object sender, RoutedEventArgs e)
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
                this.icon = filename;
                iconDisplay.Source = new BitmapImage(new Uri(filename));
                removeButton.Visibility = Visibility.Visible;
            }

        }

        private void removePath(object sender, RoutedEventArgs e)
        {
            this.icon = "";
            iconDisplay.Source = null;
            removeButton.Visibility = Visibility.Collapsed;
        }

        private void saveType(object sender, RoutedEventArgs e)
        {
            TypeManagement.types.Add(new TipResursa(Id, tipime.Text, this.icon, tipopis.Text));
            TypeManagement.displayTable.Add(new TipResursa(Id, tipime.Text, this.icon, tipopis.Text));
            this.closeFlag = true;
            this.Close();
        }

        private void cancelEdit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            if (!closeFlag)
            {
                TypeManagement.types.Add(backup);
                TypeManagement.displayTable.Add(backup);
            }
        }

        #region NotifyProperties
        public static string id;
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
    }
}
