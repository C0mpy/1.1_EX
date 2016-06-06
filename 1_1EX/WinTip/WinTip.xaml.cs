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


    public partial class WinTip : Window
    {
        public WinTip()
        {
            InitializeComponent();
            tip = new TipResursa();
            this.DataContext = this;
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }

        #region NotifyProperties
        public static TipResursa tip;
        public string tipId
        {
            get
            {
                return tip.Id;
            }
            set
            {
                if (value != tip.Id)
                {
                    tip.Id = value;
                    OnPropertyChanged("tipId");
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

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            tip.Id = tipid.Text;
            tip.Ime = tipime.Text;
            tip.Opis = tipopis.Text;
            if (tip.Ikonica == "")
            {
                string startupPath = Environment.CurrentDirectory;
                tip.Ikonica = startupPath.Substring(0, startupPath.Length - 9) + "/type.png";
            }

            TypeManagement.types.Add(tip);
            TypeManagement.displayTable.Add(tip);
            this.Close();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, RoutedEventArgs e)
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
                tip.Ikonica = filename;
                iconDisplay.Source = new BitmapImage(new Uri(filename));
                removeButton.Visibility = Visibility.Visible;
            }

        }

        private void removePath(object sender, RoutedEventArgs e)
        {
            tip.Ikonica = "";
            iconDisplay.Source = null;
            removeButton.Visibility = Visibility.Collapsed;
        }

    }
}
