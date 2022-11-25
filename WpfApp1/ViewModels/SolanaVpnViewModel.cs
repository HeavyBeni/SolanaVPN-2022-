using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfApp1.ViewModel;


namespace WpfApp1.ViewModels
{
    internal class SolanaVpnViewModel : ViewModelBase
    {
        //Commands
        public ViewModelCommand ShutdownWindowCommand{ get; set; }
        public ViewModelCommand MaximizeWindowCommand { get; set; }
        public ViewModelCommand MinimizeWindowCommand { get; set; }
        public ViewModelCommand ShowProtectionView { get; set; }
        public ViewModelCommand ShowSettingsView { get; set; }



        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged(nameof(CurrentView));
            }
        }

        public SolanaVpnProtectionViewModel SolanaVpnProtectionVM { get; set; }

        public SolanaVpnSettingsViewModel SolanaVpnSettingsVM { get; set; }

        public SolanaVpnViewModel()
        {

            SolanaVpnProtectionVM = new SolanaVpnProtectionViewModel();
            SolanaVpnSettingsVM = new SolanaVpnSettingsViewModel();
            CurrentView = SolanaVpnProtectionVM;

            //Application.Current.MainWindow.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;

            //MoveWindowCommand = new ViewModelCommand(o => { Application.Current.MainWindow.DragMove(); });
            //ShutdownWindowCommand = new ViewModelCommand(o => { Application.Current.Shutdown(); });
            //MaximizeWindowCommand = new ViewModelCommand(o =>
            //{
            //    if (Application.Current.MainWindow.WindowState == WindowState.Maximized)
            //    {
            //        Application.Current.MainWindow.WindowState = WindowState.Normal;
            //    }
            //    else
            //    {
            //        Application.Current.MainWindow.WindowState = WindowState.Maximized;
            //    }
            //});
            //MinimizeWindowCommand = new ViewModelCommand(o => { Application.Current.MainWindow.WindowState = WindowState.Minimized; });

            ShowProtectionView = new ViewModelCommand(o => { CurrentView = SolanaVpnProtectionVM; });
            ShowSettingsView = new ViewModelCommand(o => { CurrentView = SolanaVpnSettingsVM; });

        }
    }
}
