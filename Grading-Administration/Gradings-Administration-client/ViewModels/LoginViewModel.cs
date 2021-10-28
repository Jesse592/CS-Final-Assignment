using Grading_Administration_Server.EntityFramework.models;
using Grading_Administraton_Shared.Entities;
using GradingAdmin_client.Handlers;
using Gradings_Administration_client;
using Gradings_Administration_client.Commands;
using Gradings_Administration_client.ViewModels;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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
            this.LoginHandler = new LoginHandlerVM(this);

            Application.Current.MainWindow.Height = 250;
            Application.Current.MainWindow.Width = 350;
        }

        public BaseViewModel SelectedViewModel
        {
            get { return _selectedViewModel; }
            set
            {
                _selectedViewModel = value;
                OnPropertyChanged("SelectedViewModel");
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

        public void ShowError()
        {
            MessageBox.Show("Verkeerde inloggegevens");
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

        private bool _SavePassword;
        public bool SavePassword
        {
            get { return _SavePassword; }
            set
            {
                _SavePassword = value;
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

        internal void UpdateViewModel(User u)
        {
            Application.Current.Dispatcher?.Invoke(() =>
            {
                var win = new Window();
                win.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                win.WindowState = WindowState.Maximized;

                switch (Enum.GetName(typeof(UserType), int.Parse(u.UserType)))
                {
                    case "STUDENT":
                        win.Content = new StudentViewModel(u);
                        break;
                    case "TEACHER":
                        win.Content = new TeacherViewModel(u);
                        break;
                    case "ADMIN":
                        win.Content = new AdminViewModel();
                        break;
                    default:
                        SendError("Onjuiste gebruiker");
                        break;
                }

                win.Show();

                Application.Current.MainWindow.Close();
            });

            
        }

        public void Login(string UserName, string Password)
        {
            //string message = "User " + UserName + " Pass " + Password;
            //MessageBox.Show(message);
            LoginHandler.Login(UserName, Password);
        }
        
        public void SendError(string message)
        {
            MessageBox.Show(message);
        }
    }
}
