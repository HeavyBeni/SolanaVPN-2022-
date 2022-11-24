using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using WpfApp1.ViewModel;
using WpfApp1.CustomControls;
using WpfApp1.Repositories;
using System.Data.SqlClient;
using System.Data;
using System.Net;
using WpfApp1.Views;
using System.Security.Principal;
using System.Threading;

namespace WpfApp1.ViewModels
{
    internal class RegisterViewModel : ViewModelBase
    {

        private string _username;
        private string _password;
        private string _conPassword;
        private string _errorMessage;

        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public string ConPassword
        {
            get
            {
                return _conPassword;
            }
            set
            {
                _conPassword = value;
                OnPropertyChanged(nameof(ConPassword));
            }
        }

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }


        public ICommand RegisterCommand { get; }

        private readonly string _connectionString;

        // Constructor
        public RegisterViewModel()
        {
            RegisterCommand = new ViewModelCommand(ExecuteRegisterCommand, CanExecuteRegisterCommand);
            _connectionString = "Data Source=DESKTOP-H3UDNVA;Initial Catalog=LaptopDb;Integrated Security=True";
        }

        private bool CanExecuteRegisterCommand(object obj)
        {
            //ErrorMessage = "* CanExecute";
            //bool validData;
            //if (string.IsNullOrWhiteSpace(Username) || Password == null || ConPassword == null || Password != ConPassword ||
            //    Username.Length < 3 || Password.Length < 3 || ConPassword.Length < 3)
            //{
            //    validData = false;
            //    ErrorMessage = "* validData = false;";
            //}
            //else
            //{
            //    validData = true;
            //    ErrorMessage = "* validData = true;";
            //}
            //return validData;

            bool CanExecute;
            if (Username == null || Password == null)
            {
                CanExecute = false;
            }
            else
            {
                CanExecute = true;
            }
            return CanExecute;


        }

        protected SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }

        private void ExecuteRegisterCommand(object obj)
        {
            using SqlConnection connection = GetConnection();
            using SqlCommand command = new();



            connection.Open();
            SqlCommand check_User_Name = new SqlCommand("SELECT COUNT(*) FROM [User1] WHERE ([Username] = @username)", connection);
            check_User_Name.Parameters.AddWithValue("@username", Username);
            int UserExist = (int)check_User_Name.ExecuteScalar();
            connection.Close();



            if (UserExist > 0)
            {
                ErrorMessage = "* Username already taken!";
            }
            else
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "INSERT INTO User1(username, password) VALUES(@username, @password)";
                command.Parameters.AddWithValue("@username", Username);
                command.Parameters.AddWithValue("@password", Password);
                command.ExecuteNonQuery();
                connection.Close();

                LoginView login = new LoginView();
                RegisterView register = new RegisterView();
                register.Close();
                login.Show();
            }

        }
    }
}
