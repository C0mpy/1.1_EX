using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

namespace _1_1EX
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static SignInWindow siw;
        protected override void OnStartup(StartupEventArgs e)
        {
            string startupPath = Environment.CurrentDirectory;
            
            System.IO.StreamReader file = new System.IO.StreamReader(startupPath+"\\pass.txt");
            string email = file.ReadLine();
            string pass = file.ReadLine();
            file.Close();

            if (email == null)
            {
                MainWindow mw = new MainWindow();
                mw.Show();
            }
            else {
                siw = new SignInWindow(email,pass);
                siw.Show();
            }
                
        }
    }
}
