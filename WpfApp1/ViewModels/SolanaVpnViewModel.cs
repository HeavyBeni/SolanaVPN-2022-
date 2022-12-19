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
        //Commands, Defines what is showed, ProtectionView(VPN) or Settings
        public ViewModelCommand ShowProtectionView { get; set; }
        public ViewModelCommand ShowSettingsView { get; set; }
        public SolanaVpnProtectionViewModel SolanaVpnProtectionVM { get; set; }
        public SolanaVpnSettingsViewModel SolanaVpnSettingsVM { get; set; }

        //Properties

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

        // Constructors
        public SolanaVpnViewModel()
        {

            SolanaVpnProtectionVM = new SolanaVpnProtectionViewModel();
            SolanaVpnSettingsVM = new SolanaVpnSettingsViewModel();
            CurrentView = SolanaVpnProtectionVM;


            ShowProtectionView = new ViewModelCommand(o => { CurrentView = SolanaVpnProtectionVM; });
            ShowSettingsView = new ViewModelCommand(o => { CurrentView = SolanaVpnSettingsVM; });

        }
    }
}
