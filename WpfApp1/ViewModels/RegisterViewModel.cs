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
using WpfApp1.Models;

namespace WpfApp1.ViewModels
{
    internal class RegisterViewModel : ViewModelBase
    {
        // Fields
        private string _username;
        private string _password;
        private string _conPassword;
        private string _errorMessage;
        private bool _isRegisterViewVisible = true;

        private IUserRepository userRepository;

        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        // Properties
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

        public bool IsRegisterViewVisible
        {
            get
            {
                return _isRegisterViewVisible;
            }
            set
            {
                _isRegisterViewVisible = value;
                OnPropertyChanged(nameof(IsRegisterViewVisible));
            }
        }

        // Commands
        public ICommand RegisterCommand { get; }


        // Constructors
        public RegisterViewModel()
        {
            // Making a link to the UserRepository class
            userRepository = new UserRepository();

            // Getting the "Can-" and "Execute" Commands for Logging in
            RegisterCommand = new ViewModelCommand(ExecuteRegisterCommand, CanExecuteRegisterCommand);
        }

        private bool CanExecuteRegisterCommand(object obj)
        {
            bool validData;
            // The login button wont be interactive until the requirements are met
            if ( string.IsNullOrWhiteSpace(Username) || Password == null || ConPassword == null || Password != ConPassword ||
                Username.Length < 3 || Password.Length < 3 || ConPassword.Length < 3 )
            {
                validData = false;
            }
            else
            {
                validData = true;
            }

            // It needs to be returned with TRUE value before entering ExecuteRegisterCommand
            return validData;
        }

        private void ExecuteRegisterCommand(object obj)
        {
            // Checking for taken username, linked to "Repositories/UserRepository.cs"
            bool usernameok = userRepository.IfTakenUsername(new string(Username));
            if (usernameok)
            {
                ErrorMessage = "* Username already taken!";
            }
            else
            {
                // Inserting Username and Password into Database, also linked to "Repositories/UserRepository.cs"
                var isUserValid = userRepository.Add(new System.Net.NetworkCredential(Username, Password));
                if (isUserValid)
                {
                    ErrorMessage = "* Something went wrong.";
                }
                else
                {

                    ErrorMessage = "+ User succesfully created!";
                    IsRegisterViewVisible = false;
                }
            }
        }
    }
}
