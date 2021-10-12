using Grading_Administraton_Shared.Entities;
using GradingAdmin_client.Handlers;
using Gradings_Administration_client;
using Gradings_Administration_client.Commands;
using Gradings_Administration_client.ViewModels;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace GradingAdmin_client.ViewModels
{
    class LoginViewModel : BaseViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private LoginHandlerVM LoginHandler;
        private BaseViewModel _selectedViewModel;

        public LoginViewModel()
        {
            this.LoginHandler = new LoginHandlerVM(this, User);
        }

        public BaseViewModel SelectedViewModel
        {
            get { return _selectedViewModel; }
            set
            {
                _selectedViewModel = value;
                OnPropertyChanged(nameof(SelectedViewModel));
            }
        }

        private string _UserName;
        public string UserName
        {
            get { return _UserName; }
            set
            {
                _UserName = value;
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("UserName"));
            }
        }

        private string _Password;
        public string Password
        {
            get { return _Password; }
            set
            {
                _Password = value;
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Password"));
            }
        }

        private ICommand _SendLoginCommand;
        public ICommand SendLoginCommand
        {
            get
            {
                if (_SendLoginCommand == null)
                {
                    _SendLoginCommand = new GeneralCommand(
                        param => Login(UserName, Password)
                        );
                }
                return _SendLoginCommand;
            }

        }

        public void Login(string UserName, string Password)
        {
            //string message = "User " + UserName + " Pass " + Password;
            //MessageBox.Show(message);
            LoginHandler.Login(UserName, Password);
        }

        private User _User;
        public User User
        {
            get { return _User; }
            set
            {
                _User = value;
            }
        }
        
        public void SendError(string message)
        {
            MessageBox.Show(message);
        }
    }
}
