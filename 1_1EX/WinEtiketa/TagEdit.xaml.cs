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

namespace _1_1EX.WinEtiketa
{
    /// <summary>
    /// Interaction logic for TagEdit.xaml
    /// </summary>
    public partial class TagEdit : Window
    {
        Etiketa backup;
        bool closeFlag = false;

        public TagEdit(Etiketa r)
        {
            InitializeComponent();
            this.DataContext = this;
            backup = new Etiketa(r.Id, r.Boja, r.Opis);
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            removeTagFromList();
            Id = r.Id;
            colorPicker.SelectedColor = r.Boja;
            tagopis.Text = backup.Opis;
        }

        public void removeTagFromList()
        {
            for (int i = 0; i < TagManagement.tags.Count; i++)
            {
                if (backup.Id == TagManagement.tags[i].Id)
                {
                    TagManagement.tags.RemoveAt(i);
                    TagManagement.displayTable.RemoveAt(i);
                    break;
                }
            }
        }

        private void saveTag(object sender, RoutedEventArgs e)
        {
            TagManagement.tags.Add(new Etiketa(Id, (Color)colorPicker.SelectedColor, tagopis.Text));
            TagManagement.displayTable.Add(new Etiketa(Id, (Color)colorPicker.SelectedColor, tagopis.Text));
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
                TagManagement.tags.Add(backup);
                TagManagement.displayTable.Add(backup);
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
