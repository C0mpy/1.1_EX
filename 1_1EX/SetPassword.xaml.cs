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
using System.Text.RegularExpressions;

namespace _1_1EX
{
    /// <summary>
    /// Interaction logic for SetPassword.xaml
    /// </summary>
    public partial class SetPassword : Window
    {
        public SetPassword()
        {
            InitializeComponent();
        }

        private void setPass(object sender, RoutedEventArgs e)
        {
            if (!isValidEmail(email_tb.Text)) {
                MessageBox.Show("Email is not valid!");
                return;
            }

            if (pass_tb.Password != confirm_tb.Password) {
                MessageBox.Show("Passwords are different!");
                return;
            }

            if (pass_tb.Password == "") {
                MessageBox.Show("You must enter password!");
                return;
            }

            string[] lines = { email_tb.Text, pass_tb.Password};
            string path = Environment.CurrentDirectory + "\\pass.txt";
            System.IO.File.WriteAllLines(path, lines);
            
            MessageBox.Show("Password is succesfully set.");
            MainWindow.sp.Close();

        }

        private bool isValidEmail(string email)
        {
            string pattern = @"^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,}$";
            Regex rgx = new Regex(pattern, RegexOptions.IgnoreCase);
            if (rgx.IsMatch(email))
                return true;
            else
                return false;
        }

        private void cancelPass(object sender, RoutedEventArgs e)
        {
            MainWindow.sp.Close();

        }
    }
}
