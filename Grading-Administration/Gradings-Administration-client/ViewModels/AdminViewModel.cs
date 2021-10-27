

using Grading_Administration_Server.EntityFramework.models;
using Grading_Administraton_Shared.Entities;
using GradingAdmin_client.Handlers;
using Gradings_Administration_client.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;

namespace GradingAdmin_client.ViewModels
{
    class AdminViewModel : BaseViewModel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private AdminHandlerVM handler;

        public AdminViewModel()
        {
            this.handler = new AdminHandlerVM();

            this.userTypes = new List<string>();
            this.userTypes.Add(UserType.STUDENT.ToString());
            this.userTypes.Add(UserType.TEACHER.ToString());
            this.userTypes.Add(UserType.ADMIN.ToString());
        }

        private string _FirstName;
        public string FirstName
        {
            get { return _FirstName; }
            set
            {
                _FirstName = value;
            }
        }

        private string _LastName;
        public string LastName
        {
            get { return _LastName; }
            set
            {
                _LastName = value;
            }
        }

        private string _Mail;
        public string Mail
        {
            get { return _Mail; }
            set
            {
                _Mail = value;
            }
        }

        private DateTime _Birthdate;
        public DateTime BirthDate
        {
            get { return _Birthdate; }
            set
            {
                _Birthdate = value;
            }
        }

        private List<string> _userTypes;
        public List<string> userTypes
        {
            get { return _userTypes; }
            set
            {
                _userTypes = value;
            }
        }

        private int _userType;
        public int userType
        {
            get { return _userType; }
            set
            {
                _userType = value;
            }
        }

        private DateTime _CurrentDate;
        public DateTime CurrentDate
        {
            get { return _CurrentDate; }
            set
            {
                _CurrentDate = value;
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentDate"));
            }
        }

        private ICommand _AddUser;
        public ICommand AddUser
        {
            get
            {
                if (_AddUser == null)
                {
                    _AddUser = new GeneralCommand(
                        param => SendUser()
                        ); ;
                }
                return _AddUser;
            }

        }

        private void SendUser()
        {
            if (FirstName == "" || LastName == "" || Mail == "")
            {

            } else
            {
                User u = new User(0, FirstName, LastName, BirthDate, Mail, userType + "");
            }
        }
    }
}
