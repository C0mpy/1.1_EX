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
using _1_1EX.WinTip;

namespace _1_1EX.WinTag
{
    /// <summary>
    /// Interaction logic for TagManagement.xaml
    /// </summary>
    /// 


    //ODRADITI PRIKAZ BOJE U TABELI
    public partial class TagManagement : Window
    {
        public ObservableCollection<Etiketa> tags
        {
            get;
            set;
        }

        public Resurs resource;

        public TagManagement(ObservableCollection<Etiketa> t, Resurs r)
        {
            InitializeComponent();
            this.DataContext = this;
            tags = t;
            resource = r;
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;

        }

        private void TagCreateClick(object sender, RoutedEventArgs e)
        {
            var ew = new _1_1EX.WinEtiketa.EtiketaWin();
            ew.Show();
        }

        private void TagDeleteClick(object sender, RoutedEventArgs e)
        {
            tags.Remove((Etiketa)dgTags.SelectedItem);
        }

        private void SaveClick(object sender, RoutedEventArgs e)
        {
            for (int j = 0; j < dgTags.Items.Count; j++)
            {
                CheckBox cb = TypeManagement.FindVisualChildren<CheckBox>(dgTags).FirstOrDefault();
                if (cb.IsChecked == true)
                {
                    resource.Etikete1.Add(tags.ElementAt(j));
                }
            }


            this.Close();
        }
    }
}
