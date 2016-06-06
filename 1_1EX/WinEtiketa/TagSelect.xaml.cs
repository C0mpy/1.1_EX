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
using System.Windows.Threading;
using System.Collections.ObjectModel;
using System.Threading;
using System.Collections;

namespace _1_1EX.WinEtiketa
{

    public partial class TagSelect : Window
    {
        public ObservableCollection<Etiketa> tags
        {
            get;
            set;
        }
        public List<Etiketa> backup;
        public List<Etiketa> selectedTags;
        public bool closeFlag;
        public bool checkFlag;
        public Resurs resurs;
        public TagSelect(ObservableCollection<Etiketa> t, Resurs r)
        {
            InitializeComponent();
            this.DataContext = this;
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            resurs = r;
            tags = new ObservableCollection<Etiketa>();
            backup = new List<Etiketa>();
            selectedTags = new List<Etiketa>();
            closeFlag = false;
            checkFlag = false;
            foreach (Etiketa tr in t)
            {
                tags.Add(new Etiketa(tr.Id, tr.Boja, tr.Opis));
            }
            if (r.Etikete1.Count != 0)
            {
                foreach (Etiketa et in r.Etikete1)
                {
                    selectedTags.Add(new Etiketa(et.Id, et.Boja, et.Opis));
                    backup.Add(new Etiketa(et.Id, et.Boja, et.Opis));
                }
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

            resurs.Etikete1.Clear();
            foreach (Etiketa et in selectedTags)
            {
                resurs.Etikete1.Add(et);
            }
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
                resurs.Etikete1.Clear();
                foreach (Etiketa et in backup)
                {
                    resurs.Etikete1.Add(et);
                }
            }
            MessageBox.Show(resurs.ToString());
        }

        public void selectResource()
        {
            if (selectedTags.Count != 0)
            {
                for (int i = 0; i < tags.Count; i++)
                {
                    for (int j = 0; j < selectedTags.Count; j++)
                    {
                        if (selectedTags.ElementAt(j).Id == tags.ElementAt(i).Id)
                        {
                            CheckBox cb = FindVisualChildren<CheckBox>(dgTags).ElementAt(i);
                            checkFlag = true;
                            cb.IsChecked = true;
                            break;
                        }
                    }
                }
            }
            searchTextBox.IsReadOnly = false;
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
            tags.Clear();
            searchTextBox.IsReadOnly = true;
            foreach (Etiketa et in MainWindow.tags)
            {
                if (et.Id.ToLower().Contains(searchTextBox.Text.ToLower()))
                {
                    tags.Add(et);
                }
            }
            Dispatcher.Invoke(new Action(() => { selectResource(); }), DispatcherPriority.ContextIdle, null);
        }

        private void Checkbox_Checked(object sender, RoutedEventArgs e)
        {
            if (!checkFlag)
            {
                selectedTags.Add((Etiketa)dgTags.SelectedItem);
            }
            checkFlag = false;
        }

        private void Checkbox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (!checkFlag)
            {
                for (int i = 0; i < selectedTags.Count; i++)
                {
                    if (selectedTags.ElementAt(i).Id == ((Etiketa)dgTags.SelectedItem).Id)
                    {
                        selectedTags.RemoveAt(i);
                    }
                }
            }
            checkFlag = false;
        }
    }
}
