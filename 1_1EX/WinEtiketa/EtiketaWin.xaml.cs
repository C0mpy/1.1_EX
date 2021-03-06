﻿using System;
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
    /// Interaction logic for EtiketaWin.xaml
    /// </summary>
    public partial class EtiketaWin : Window, INotifyPropertyChanged
    {
        public EtiketaWin()
        {
            InitializeComponent();
            tag = new Etiketa();
            this.DataContext = this;
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }

        #region NotifyProperties
        public static Etiketa tag;
        public string tagId
        {
            get
            {
                return tag.Id;
            }
            set
            {
                if (value != tag.Id)
                {
                    tag.Id = value;
                    OnPropertyChanged("tagId");
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
            tag.Id = tagid.Text;
            tag.Boja = (Color)colorPicker.SelectedColor;
            tag.Opis = tagopis.Text;
            TagManagement.tags.Add(tag);
            TagManagement.displayTable.Add(tag);
            this.Close();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
