

using Grading_Administration_Server.EntityFramework.models;
using Grading_Administraton_Shared.Entities;
using GradingAdmin_client.Handlers;
using Gradings_Administration_client.ViewModels;
using Newtonsoft.Json.Linq;
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
            this.handler = new AdminHandlerVM(this);

            this.userTypes = new List<string>();
            this.userTypes.Add(UserType.STUDENT.ToString());
            this.userTypes.Add(UserType.TEACHER.ToString());
            this.userTypes.Add(UserType.ADMIN.ToString());

            this.ModuleSuccesOpacity = 0;
            this.ModuleFailOpacity = 0;
        }

        #region AddUser

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

        private string _UserName;
        public string UserName
        {
            get { return _UserName; }
            set
            {
                _UserName = value;
            }
        }

        private string _Password;
        public string Password
        {
            get { return _Password; }
            set
            {
                _Password = value;
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

        private string _userType;
        public string userType
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

        private ICommand _AddUserCommand;
        public ICommand AddUserCommand
        {
            get
            {
                if (_AddUserCommand == null)
                {
                    _AddUserCommand = new GeneralCommand(
                        param => AddUser()
                        );
                }
                return _AddUserCommand;
            }
        }

        private void AddUser()
        {
            if (this.FirstName != "" && this.LastName != "" && this.Mail != "" && this.UserName != "" && this.Password != "")
            {
                User u = new User(0, this.FirstName, this.LastName, this.BirthDate, this.Mail, this.userType);
                this.handler.NewUser(u, this.UserName, this.Password);
            }
        }

        #endregion

        private string _ModuleName;
        public string ModuleName
        {
            get { return _ModuleName; }
            set
            {
                _ModuleName = value;
            }
        }

        private DateTime _StartDate;
        public DateTime StartDate
        {
            get { return _StartDate; }
            set
            {
                _StartDate = value;
            }
        }

        private DateTime _EndDate;
        public DateTime EndDate
        {
            get { return _EndDate; }
            set
            {
                _EndDate = value;
            }
        }

        private string _ECTAmount;
        public string ECTAmount
        {
            get { return _ECTAmount; }
            set
            {
                _ECTAmount = value;
            }
        }

        public List<string> GradeTypes
        {
            get { return new List<string>() { "Numerisch", "Letter" }; }
        }

        private string _GradeType;
        public string GradeType
        {
            get { return _GradeType; }
            set
            {
                _GradeType = value;
            }
        }

        private int _ModuleSuccesOpacity;
        public int ModuleSuccesOpacity
        {
            get { return _ModuleSuccesOpacity; }
            set
            {
                _ModuleSuccesOpacity = value;
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ModuleSuccesOpacity"));
            }
        }

        private int _ModuleFailOpacity;
        public int ModuleFailOpacity
        {
            get { return _ModuleFailOpacity; }
            set
            {
                _ModuleFailOpacity = value;
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ModuleFailOpacity"));
            }
        }

        private ICommand _AddModuleCommand;
        public ICommand AddModuleCommand
        {
            get
            {
                if (_AddModuleCommand == null)
                {
                    _AddModuleCommand = new GeneralCommand(
                        param => AddModule()
                        );
                }
                return _AddModuleCommand;
            }
        }

        private void AddModule()
        {
            if (this.ModuleName != "" && this.ECTAmount != "" && int.TryParse(this.ECTAmount, out var n))
            {
                bool isNumerical = false;

                if (this.GradeType == "Numerisch")
                {
                    isNumerical = true;
                }

                Module m = new Module(0, this.ModuleName, this.StartDate, this.EndDate, int.Parse(this.ECTAmount), isNumerical);

                this.handler.NewModule(m);
            }
        }

        internal void UpdateStatus(JObject obj)
        {
            JToken message = obj.SelectToken("data");

            if (message.ToString() == "Succes")
            {
                this.ModuleFailOpacity = 0;
                this.ModuleSuccesOpacity = 100;
            }
            else
            {
                this.ModuleFailOpacity = 100;
                this.ModuleSuccesOpacity = 0;
            }
        }

    }
}
