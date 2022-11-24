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
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected void ApplicationStart(object sender, StartupEventArgs e)
        {
            var loginView = new LoginView();
            //var registerView = new RegisterView();
            //var vpnView = new SolanaVpnView();
            loginView.Show();

            

            //loginView.IsVisibleChanged += (s, ev) =>
            //{
            //    if (loginView.IsVisible == false && loginView.IsLoaded)
            //    {
            //        vpnView.Show();
            //        loginView.Close();
            //    }
            //};

            //registerView.IsVisibleChanged += (s, ev) =>
            //{
            //    if (registerView.IsVisible == false && registerView.IsLoaded)
            //    {
            //        loginView.Show();
            //        registerView.Close();
            //    }
            //};

            //if (IsLogin)

        }
    }
}