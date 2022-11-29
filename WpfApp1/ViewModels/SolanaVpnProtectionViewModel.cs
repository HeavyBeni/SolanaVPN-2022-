using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfApp1.Models;
using WpfApp1.ViewModel;

namespace WpfApp1.ViewModels
{
    internal class SolanaVpnProtectionViewModel : ViewModelBase
    {
        public ObservableCollection<ServerModel> Servers { get; set; }
        private string _Status;

        public string Status
        {
            get { return _Status; }
            set { _Status = value; }
        }

        public ICommand ConnectCommand { get; set; }
        public SolanaVpnProtectionViewModel()
        {
            Servers = new ObservableCollection<ServerModel>();
            for (int i = 0; i < 10; i++)
            {
                Servers.Add(new ServerModel
                {
                    Country = "NORWAY"
                });

                ConnectCommand = new ViewModelCommand(ExecuteConnectCommand);
            }
        }

        private void ExecuteConnectCommand(object obj)
        {
            Status = "Connecting...";
        }
    }
}
