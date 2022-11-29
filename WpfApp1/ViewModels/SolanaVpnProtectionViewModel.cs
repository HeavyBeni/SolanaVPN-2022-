using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
            set 
            { 
                _Status = value;
                OnPropertyChanged(nameof(Status));
            }
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

        private void ServerBuilder()
        {
            var address = "us1.vpnbook.com";
            var FolderPath = $"{Directory.GetCurrentDirectory()}/VPN";
            var PbkPath = $"{FolderPath}/{address}.pbk";

            if(!Directory.Exists(FolderPath))
                Directory.CreateDirectory(FolderPath);

            if (File.Exists(PbkPath))
            {
                MessageBox.Show("Connection Laready exists!");
                return;
            }

            var sb = new StringBuilder();
            sb.AppendLine("[MyServer");
            sb.AppendLine("MEDIA=rastapi");
            sb.AppendLine("Port=VPN2-0");
            sb.AppendLine("Device=WAN Miniport (IKEv2");
            sb.AppendLine("DEVICE=vpn");
            sb.AppendLine($"PhoneNumber={address}");
            File.WriteAllText(PbkPath, sb.ToString());
            
        }

        private void ExecuteConnectCommand(object obj)
        {
            Status = "Connecting...";
            var process = new Process();
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.WorkingDirectory = Environment.CurrentDirectory;
            process.StartInfo.ArgumentList.Add(@"/c rasdial MyServer vpnbook twrszht /phonebook:./VPN/VPN.pbk");

            process.Start();
            MessageBox.Show("faul");
            process.WaitForExit();
            
            switch (process.ExitCode)
            {
                case 0:
                    Console.WriteLine("SUccess!");
                    break;
                case 100:
                    Console.WriteLine("Wrong Credentials!");
                    break;
                default:
                    Console.WriteLine($"Error: {process.ExitCode}");
                    break;
            }
        }
    }
}
