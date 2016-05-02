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

namespace _1_1EX.WinTip
{
    /// <summary>
    /// Interaction logic for WinTip.xaml
    /// </summary>
    public partial class WinTip : Window
    {
        public WinTip()
        {
            InitializeComponent();
            tip = new TipResursa();
            this.DataContext = this;
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
            tip.Ikonica = "icon";
            tip.Opis = tipopis.Text;
            MainWindow.resurs.Tip = tip;
            MainWindow.types.Add(tip);
            //Serializer.SaveTip(); -- ne radi mi ni ovde mladjane :( a di je Serializer ?
            //sacuvaj promene u fajl
            tip = new TipResursa();
            this.Close();

        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
