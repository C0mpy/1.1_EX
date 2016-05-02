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
using System.Collections.ObjectModel;
using System.Data;
using _1_1EX.Model;

namespace _1_1EX.WinTip
{
    /// <summary>
    /// Interaction logic for TypeManagement.xaml
    /// </summary>
    /// 

    //ODRADITI PRIKAZ IKONICE U TABELI
    public partial class TypeManagement : Window
    {
        public ObservableCollection<TipResursa> types
        {
            get;
            set;
        }
        public Resurs resource;
        public TypeManagement(ObservableCollection<TipResursa> t, Resurs r)
        {
            InitializeComponent();
            this.DataContext = this;
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            types = t;
            resource = r;

            for (int i = 0; i < dgTypes.Columns.Count; i++)
            {
                if (r.Tip != null)
                {
                    if (r.Tip.Id == dgTypes.Columns[1].GetCellContent(i).ToString())
                    {
                        (dgTypes.Columns[0].GetCellContent(i) as RadioButton).IsChecked = true;
                        break;
                    }
                }
            }

            dgTypes.ItemsSource = types;
        }

        private void TypeCreateClick(object sender, RoutedEventArgs e)
        {
            var ew = new _1_1EX.WinTip.WinTip();
            ew.Show();
        }

        private void TypeDeleteClick(object sender, RoutedEventArgs e)
        {
            types.Remove((TipResursa)dgTypes.SelectedItem);
        }

        private void SaveClick(object sender, RoutedEventArgs e)
        {


            for (int j = 0; j < dgTypes.Items.Count; j++)
            {
                RadioButton rb = FindVisualChildren<RadioButton>(dgTypes).FirstOrDefault();
                if (rb.IsChecked == true)
                {
                    resource.Tip = types.ElementAt(j);
                    break;
                }
            }


            this.Close();
        }

        //pa zar mora ovo da se koristi da bih dobio element iz DataGridTemplateColumn u WPF?!
        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }
    }
}
