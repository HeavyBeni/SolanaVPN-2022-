using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security;
using System.Windows.Input;
using WpfApp1.ViewModel;
using WpfApp1.Models;
using WpfApp1.Repositories;
using System.Threading;
using System.Security.Principal;
using WpfApp1.Views;
using Microsoft.VisualBasic;
using System.ComponentModel;

namespace WpfApp1.ViewModels
{
    
    public class LoginViewModel : ViewModelBase
    {
        // Fields
        private string _username;
        private string _password;
        private string _errorMessage;
        private bool _isLoginViewVisible = true;

        private IUserRepository userRepository;

        public string Username
        {
            get
            {
                return _username;
            }
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
        public string ErrorMessage
        {
            get
            {
                return _errorMessage;
            }
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }
        public bool IsLoginViewVisible
        {
            get
            {
                return _isLoginViewVisible;
            }
            set
            {
                _isLoginViewVisible = value;
                OnPropertyChanged(nameof(IsLoginViewVisible));
            }
        }


        // Commands
        public ICommand LoginCommand { get; }


        // Constructors
        public LoginViewModel()
        {
            // 
            userRepository = new UserRepository();

            // Creating LoginCommand from ViewModelCommand structure
            LoginCommand = new ViewModelCommand(ExecuteLoginCommand, CanExecuteLoginCommand);
        }
        

        private bool CanExecuteLoginCommand(object obj)
        {
            //
            bool validData;
            if (string.IsNullOrWhiteSpace(Username) || Username.Length < 3 ||
                Password == null || Password.Length < 3)
                validData = false;
            else
                validData = true;
            return validData;
        }

        private void ExecuteLoginCommand(object obj)
        {
            var isUserValid = userRepository.AuthenticateUser(new System.Net.NetworkCredential(Username, Password));
            if (isUserValid)
            {
                // The Display should be changed by now if the Authentication went trough,
                // if LoginView is still visable, something went wrong.
                ErrorMessage = "* Access gained, something went wrong...";
            }
            else
            {
                // AuthenicateUser went wrong, probably Invalid credentials
                ErrorMessage = "* Invalid username or password";
            }
        }
    }
}