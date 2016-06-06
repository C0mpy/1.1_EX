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
using System.Diagnostics;
using System.Windows.Navigation;

namespace _1_1EX
{
    /// <summary>
    /// Interaction logic for SignInWindow.xaml
    /// </summary>
    public partial class SignInWindow : Window
    {
        private string email;
        private string pass;

        public SignInWindow(string Email, string Pass)
        {
            email = Email;
            pass = Pass;
            InitializeComponent();
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            
            String uri = e.Uri.AbsoluteUri+"?email="+email+"="+pass;
            Process.Start(new ProcessStartInfo(uri));
            e.Handled = true;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (textBox1.Password == pass)
            {
                MainWindow mw = new MainWindow();
                App.siw.Close();
                mw.Show();
            }
            else {
                MessageBox.Show("Wrong password!");
            }
        }
    }
}
