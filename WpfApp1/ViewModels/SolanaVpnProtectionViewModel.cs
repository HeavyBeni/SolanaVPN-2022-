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
            for (int i = 0; i < 1; i++)
            {
                Servers.Add(new ServerModel
                {
                    ID = 1,
                    
                    Country = "USA 01",

                    Server = "us1.vpnbook.com",

                    Username= "vpnbook",

                    Password = "twrszht",

                    Image = "https://imgur.com/tX2FzGr.png"
                }); ;

                Servers.Add(new ServerModel
                {
                    ID = 2,

                    Country = "USA 02",
                    
                    Server = "us2.vpnbook.com",

                    Username = "vpnbook",

                    Password = "twrszht",

                    Image = "https://imgur.com/tX2FzGr.png"
                });

                Servers.Add(new ServerModel
                {   
                    ID= 3,

                    Country = "CANADA 01",

                    Server = "ca222.vpnbook.com",

                    Username = "vpnbook",

                    Password = "twrszht",

                    Image = "https://i.imgur.com/irQNDth.png"
                });

                Servers.Add(new ServerModel
                {
                    ID = 4,

                    Country = "CANADA 02",

                    Server = "ca198.vpnbook.com",

                    Username = "vpnbook",

                    Password = "twrszht",

                    Image = "https://i.imgur.com/irQNDth.png"
                });

                Servers.Add(new ServerModel
                {
                    ID = 5,

                    Country = "FRANCE 01",

                    Server = "fr1.vpnbook.com",

                    Username = "vpnbook",

                    Password = "twrszht",

                    Image = "https://i.imgur.com/QqRSTNg.png"
                });

                Servers.Add(new ServerModel
                {
                    ID = 6,

                    Country = "FRANCE 02",

                    Server = "fr8.vpnbook.com",

                    Username = "vpnbook",

                    Password = "twrszht",

                    Image = "https://i.imgur.com/QqRSTNg.png"
                });

                Servers.Add(new ServerModel
                {
                    ID = 7,

                    Country = "GERMANY 01",

                    Server = "de4.vpnbook.com",

                    Username = "vpnbook",

                    Password = "twrszht",

                    Image = "https://i.imgur.com/PenzKfd.png"
                });

                Servers.Add(new ServerModel
                {
                    ID = 8,

                    Country = "POLAND 01",

                    Server = "PL226.vpnbook.com",

                    Username = "vpnbook",

                    Password = "twrszht",

                    Image = "https://i.imgur.com/lfolGcA.png"
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
            Task.Run(() =>
            {
                Status = "Connecting...";
                var process = new Process();
                process.StartInfo.FileName = "cmd.exe";
                process.StartInfo.WorkingDirectory = Environment.CurrentDirectory;
                process.StartInfo.ArgumentList.Add(@"/c rasdial MyServer vpnbook twrszht /phonebook:./VPN/VPN.pbk");
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;

                process.Start();
                process.WaitForExit();

                switch (process.ExitCode)
                {
                    case 0:
                        Debug.WriteLine("Success!");
                        Status = "Success";
                        break;
                    case 691:
                        Debug.WriteLine("Wrong Credentials!");
                        Status = "Wrong credentials!";
                        break;
                    default:
                        Debug.WriteLine($"Error: {process.ExitCode}");
                        Status = $"Error: {process.ExitCode}";
                        break;
                }
            });
        }
    }
}
