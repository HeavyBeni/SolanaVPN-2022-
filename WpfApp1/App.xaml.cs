using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WpfApp1;
using WpfApp1.Views;

namespace WpfApp1
{
    // Interaction logic for App.xaml
    public partial class App : Application
    {
        
        // Starting Application
        protected void ApplicationStart(object sender, StartupEventArgs e)
        {
            var loginView = new LoginView();
            loginView.Show();
        }
    }
}