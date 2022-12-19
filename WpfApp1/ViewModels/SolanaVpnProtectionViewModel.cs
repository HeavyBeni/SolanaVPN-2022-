using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Security;
using System.Security.Cryptography;
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
        // ServerModel
        public ObservableCollection<ServerModel> Servers { get; set; }
        
        // Properties
        private string _status;
        public string Status
        {
            get { return _status; }
            set 
            { 
                _status = value;
                OnPropertyChanged(nameof(Status));
            }
        }


        private bool _connected;
        public bool Connected
        {
            get { return _connected; }
            set
            {
                _connected = value;
                OnPropertyChanged(nameof(_connected));
            }
        }


        private string _conDisconContent;
        public string ConDisConContent
        {
            get { return _conDisconContent; }
            set
            {
                _conDisconContent = value;
                OnPropertyChanged(nameof(ConDisConContent));
            }
        }


        private ServerModel _selectedServerModel;
        public ServerModel SelectedServerModel
        {
            get { return _selectedServerModel; }
            set
            {
                if (_selectedServerModel != value)
                {
                    _selectedServerModel = value;
                    OnPropertyChanged(nameof(SelectedServerModel));
                }
            }
        }


        // Commands
        public ICommand ConnectDisconnectCommand { get; set; }

        // Constructors
        public SolanaVpnProtectionViewModel()
        {
            // Adding the servers to servermodel
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

                Servers.Add(new ServerModel
                {
                    ID = 9,
                    Country = "Coming Soon"
                });

                Servers.Add(new ServerModel
                {
                    ID = 10,
                    Country = "Coming Soon"
                });



            }

            // Command
            
            ConnectDisconnectCommand = new ViewModelCommand(ExecuteConnectDisconnectCommand);
            
            // Setting Connectbutton Content
            ConDisConContent = "Connect";
            
            // Satus
            Status = "Standby";
        }

        private void SelectedServer(object sender, EventArgs e)
        {
            if (SelectedServer != null)
            {
                Status = "ok";
            }
        }
        
        //Building the server that the user can connect to
        private void ServerBuilder()
        {
            var address = SelectedServerModel.Server;
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

        // Executing connection or disconnection
        private void ExecuteConnectDisconnectCommand(object obj)
        {
            if (ConDisConContent == "Connect")
            {
                Task.Run(() =>
                {
                    // Starting Connection
                    Task.Run(ServerBuilder);
                    Status = "Connecting...";

                    // Connection with cmd to the VPN Adress
                    var process = new Process();
                    process.StartInfo.FileName = "cmd.exe";
                    process.StartInfo.WorkingDirectory = Environment.CurrentDirectory;
                    process.StartInfo.ArgumentList.Add($@"/c rasdial MyServer {SelectedServerModel.Username} {SelectedServerModel.Password} /phonebook:./VPN/{SelectedServerModel.Server}.pbk");
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.CreateNoWindow = true;

                    process.Start();
                    process.WaitForExit();

                    // Preparing for Errors
                    switch (process.ExitCode)
                    {
                        case 0:
                            Debug.WriteLine("Success!");
                            ConDisConContent = "Disconnect";
                            Status = "Connected";
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
            
            // If user is connected than disconnect
            else
            {
                Status = "Disconnectiong...";
                
                // Starting the process for disconnecting via CMD
                var process = new Process();
                process.StartInfo.FileName = "cmd.exe";
                process.StartInfo.WorkingDirectory = Environment.CurrentDirectory;
                process.StartInfo.ArgumentList.Add(@"/c rasdial /d");

                process.Start();
                process.WaitForExit();

                Status = "Standby";
                ConDisConContent = "Connect"; 
            }  
        }
    }
}
