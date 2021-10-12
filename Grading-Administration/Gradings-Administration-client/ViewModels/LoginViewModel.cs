using Gradings_Administration_client;
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
    class LoginViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public LoginViewModel()
        {
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

        private void Login(string UserName, string Password)
        {
            //string message = "User " + UserName + " Pass " + Password;
            //MessageBox.Show(message);

        }
    }
}
