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
using System.Collections.ObjectModel;
using System.Windows.Threading;
using System.Threading;

namespace _1_1EX.WinTip
{
 
    public partial class TypeSelect : Window
    {
        public ObservableCollection<TipResursa> types
        {
            get;
            set;
        }
        public TipResursa backup = null;
        public TipResursa selectedType = null;
        public bool closeFlag = false;
        public bool checkFlag = false;
        public Resurs resurs;

        public TypeSelect(ObservableCollection<TipResursa> t, Resurs r)
        {
            InitializeComponent();
            this.DataContext = this;
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            resurs = r;
            types = new ObservableCollection<TipResursa>();
            foreach (TipResursa tr in t)
            {
                types.Add(new TipResursa(tr.Id, tr.Ime, tr.Ikonica, tr.Opis));
            }
            if (r.Tip != null)
            {
                selectedType = new TipResursa(r.Tip.Id, r.Tip.Ime, r.Tip.Ikonica, r.Tip.Opis);
                backup = new TipResursa(r.Tip.Id, r.Tip.Ime, r.Tip.Ikonica, r.Tip.Opis);
            }
        }

        bool _shown;
        protected override void OnContentRendered(EventArgs e)
        {
            base.OnContentRendered(e);
            if (_shown)
                return;

            _shown = true;

            selectResource();
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            resurs.Tip = selectedType;
            closeFlag = true;
            this.Close();
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            if (!closeFlag)
            {
                resurs.Tip = backup;
            }
        }

        public void selectResource()
        {
            if (selectedType != null)
            {
                for (int i = 0; i < dgTypes.Items.Count; i++)
                {
                    if (selectedType.Id == types.ElementAt(i).Id)
                    {
                        RadioButton rb = FindVisualChildren<RadioButton>(dgTypes).ElementAt(i);
                        checkFlag = true;
                        rb.IsChecked = true;
                        break;
                    }
                }
            }
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

        private void filterTable(object sender, RoutedEventArgs e)
        {
            types.Clear();
            foreach (TipResursa tr in MainWindow.types)
            {
                if (tr.Id.ToLower().Contains(searchTextBox.Text.ToLower()))
                {
                    types.Add(tr);
                }
            }
            selectResource();
            
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (!checkFlag)
            {
                selectedType = (TipResursa)dgTypes.SelectedItem;
            }
            checkFlag = false;
        }
    }

}
