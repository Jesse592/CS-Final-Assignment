

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

            this.UserList = new List<User>();
            this.ModuleList = new List<Module>();

            this.userTypes = new List<string>();
            this.userTypes.Add(UserType.STUDENT.ToString());
            this.userTypes.Add(UserType.TEACHER.ToString());
            this.userTypes.Add(UserType.ADMIN.ToString());

            this.ModuleSuccesOpacity = 0;
            this.ModuleFailOpacity = 0;

            this.UserFailDeleteOpacity = 0;
            this.UserSuccesDeleteOpacity = 0;

            this.handler.GetAllUsers();
            this.handler.GetAllModules();
        }

        #region AddUser

        private string _FirstName;
        public string FirstName
        {
            get { return _FirstName; }
            set
            {
                _FirstName = value;
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("FirstName"));
            }
        }

        private string _LastName;
        public string LastName
        {
            get { return _LastName; }
            set
            {
                _LastName = value;
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("LastName"));
            }
        }

        private string _Mail;
        public string Mail
        {
            get { return _Mail; }
            set
            {
                _Mail = value;
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Mail"));
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

        private int _UserSuccesOpacity;
        public int UserSuccesOpacity
        {
            get { return _UserSuccesOpacity; }
            set
            {
                _UserSuccesOpacity = value;
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("UserSuccesOpacity"));
            }
        }

        private int _UserFailOpacity;
        public int UserFailOpacity
        {
            get { return _UserFailOpacity; }
            set
            {
                _UserFailOpacity = value;
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("UserFailOpacity"));
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

        internal void UpdateUserStatus(JObject obj)
        {
            JToken message = obj.SelectToken("data");

            if (message.ToString() == "Succes")
            {
                this.UserFailOpacity = 0;
                this.UserSuccesOpacity = 100;
                this.handler.GetAllUsers();

                this.FirstName = "";
                this.LastName = "";
                this.UserName = "";
                this.Password = "";
                this.ECTAmount = "";
                this.Mail = "";
            }
            else
            {
                this.UserFailOpacity = 100;
                this.UserSuccesOpacity = 0;
            }
        }

        #endregion

        #region AddModule

        private string _ModuleName;
        public string ModuleName
        {
            get { return _ModuleName; }
            set
            {
                _ModuleName = value;
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ModuleName"));
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
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ECTAmount"));
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

        internal void UpdateModuleStatus(JObject obj)
        {
            JToken message = obj.SelectToken("data");

            if (message.ToString() == "Succes")
            {
                this.ModuleFailOpacity = 0;
                this.ModuleSuccesOpacity = 100;
                this.handler.GetAllModules();

                this.ModuleName = "";
                this.ECTAmount = "";
            }
            else
            {
                this.ModuleFailOpacity = 100;
                this.ModuleSuccesOpacity = 0;
            }
        }

        #endregion

        private List<User> _UserList;
        public List<User> UserList
        {
            get { return _UserList; }
            set
            {
                _UserList = value;
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("UserList"));
            }
        }

        private List<Module> _ModuleList;
        public List<Module> ModuleList
        {
            get { return _ModuleList; }
            set
            {
                _ModuleList = value;
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ModuleList"));
            }
        }

        #region DeleteUser

        private User _SelectedDeleteUser;
        public User SelectedDeleteUser
        {
            get { return _SelectedDeleteUser; }
            set
            {
                _SelectedDeleteUser = value;
            }
        }

        private int _UserSuccesDeleteOpacity;
        public int UserSuccesDeleteOpacity
        {
            get { return _UserSuccesDeleteOpacity; }
            set
            {
                _UserSuccesDeleteOpacity = value;
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("UserSuccesDeleteOpacity"));
            }
        }

        private int _UserFailDeleteOpacity;
        public int UserFailDeleteOpacity
        {
            get { return _UserFailDeleteOpacity; }
            set
            {
                _UserFailDeleteOpacity = value;
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("UserFailDeleteOpacity"));
            }
        }

        internal void UpdateDeleteUserStatus(JObject obj)
        {
            JToken message = obj.SelectToken("data");

            if (message.ToString() == "Succes")
            {
                this.UserSuccesDeleteOpacity = 100;
                this.UserFailDeleteOpacity = 0;
                this.handler.GetAllUsers();
            }
            else
            {
                this.UserFailDeleteOpacity = 100;
                this.UserSuccesDeleteOpacity = 0;
            }
        }

        private ICommand _DeleteUserCommand;
        public ICommand DeleteUserCommand
        {
            get
            {
                if (_DeleteUserCommand == null)
                {
                    _DeleteUserCommand = new GeneralCommand(
                        param => DeleteUser()
                        );
                }
                return _DeleteUserCommand;
            }
        }

        private void DeleteUser()
        {
            this.handler.DeleteUser(SelectedDeleteUser);
        }

        #endregion

        #region DeleteModule

        private Module _SelectedDeleteModule;
        public Module SelectedDeleteModule
        {
            get { return _SelectedDeleteModule; }
            set
            {
                _SelectedDeleteModule = value;
            }
        }

        private int _ModuleSuccesDeleteOpacity;
        public int ModuleSuccesDeleteOpacity
        {
            get { return _ModuleSuccesDeleteOpacity; }
            set
            {
                _ModuleSuccesDeleteOpacity = value;
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ModuleSuccesDeleteOpacity"));
            }
        }

        private int _ModuleFailDeleteOpacity;
        public int ModuleFailDeleteOpacity
        {
            get { return _ModuleFailDeleteOpacity; }
            set
            {
                _ModuleFailDeleteOpacity = value;
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ModuleFailDeleteOpacity"));
            }
        }

        private ICommand _DeleteModuleCommand;
        public ICommand DeleteModuleCommand
        {
            get
            {
                if (_DeleteModuleCommand == null)
                {
                    _DeleteModuleCommand = new GeneralCommand(
                        param => DeleteModule()
                        );
                }
                return _DeleteModuleCommand;
            }
        }

        private void DeleteModule()
        {
            this.handler.DeleteModule(SelectedDeleteModule);
        }

        internal void UpdateDeleteModuleStatus(JObject obj)
        {
            JToken message = obj.SelectToken("data");

            if (message.ToString() == "Succes")
            {
                this.ModuleSuccesDeleteOpacity = 100;
                this.ModuleFailDeleteOpacity = 0;
                this.handler.GetAllModules();
            }
            else
            {
                this.ModuleFailDeleteOpacity = 100;
                this.ModuleSuccesDeleteOpacity = 0;
            }
        }

        #endregion

        #region LinkTeacher

        private List<User> _TeacherList;
        public List<User> TeacherList
        {
            get { return _TeacherList; }
            set
            {
                _TeacherList = value;
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TeacherList"));
            }
        }

        private User _SelectedTeacher;
        public User SelectedTeacher
        {
            get { return _SelectedTeacher; }
            set
            {
                _SelectedTeacher = value;
            }
        }

        private Module _SelectedTeacherModule;
        public Module SelectedTeacherModule
        {
            get { return _SelectedTeacherModule; }
            set
            {
                _SelectedTeacherModule = value;
            }
        }

        private int _TeacherLinkOpacity;
        public int TeacherLinkOpacity
        {
            get { return _TeacherLinkOpacity; }
            set
            {
                _TeacherLinkOpacity = value;
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TeacherLinkOpacity"));
            }
        }

        private int _TeacherLinkFailOpacity;
        public int TeacherLinkFailOpacity
        {
            get { return _TeacherLinkFailOpacity; }
            set
            {
                _TeacherLinkFailOpacity = value;
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TeacherLinkFailOpacity"));
            }
        }

        private ICommand _LinkTeacherCommand;
        public ICommand LinkTeacherCommand
        {
            get
            {
                if (_LinkTeacherCommand == null)
                {
                    _LinkTeacherCommand = new GeneralCommand(
                        param => LinkTeacher()
                        );
                }
                return _LinkTeacherCommand;
            }
        }

        private void LinkTeacher()
        {
            if (SelectedTeacher != null && SelectedTeacherModule != null)
            {
                this.handler.AddTeacherToModule(SelectedTeacher, SelectedTeacherModule);
            }
        }

        internal void UpdateTeacherLinkStatus(JObject obj)
        {
            JToken message = obj.SelectToken("data");

            if (message.ToString() == "Succes")
            {
                this.TeacherLinkFailOpacity = 0;
                this.TeacherLinkOpacity = 100;
            }
            else
            {
                this.TeacherLinkFailOpacity = 100;
                this.TeacherLinkOpacity = 0;
            }
        }

        #endregion

        private List<User> _StudentList;
        public List<User> StudentList
        {
            get { return _StudentList; }
            set
            {
                _StudentList = value;
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("StudentList"));
            }
        }

        private User _SelectedStudent;
        public User SelectedStudent
        {
            get { return _SelectedStudent; }
            set
            {
                _SelectedStudent = value;
            }
        }

        private Module _SelectedStudentModule;
        public Module SelectedStudentModule
        {
            get { return _SelectedStudentModule; }
            set
            {
                _SelectedStudentModule = value;
            }
        }

        private int _StudentLinkOpacity;
        public int StudentLinkOpacity
        {
            get { return _StudentLinkOpacity; }
            set
            {
                _StudentLinkOpacity = value;
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("StudentLinkOpacity"));
            }
        }

        private int _StudentLinkFailOpacity;
        public int StudentLinkFailOpacity
        {
            get { return _StudentLinkFailOpacity; }
            set
            {
                _StudentLinkFailOpacity = value;
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("StudentLinkFailOpacity"));
            }
        }

        private ICommand _LinkStudentCommand;
        public ICommand LinkStudentCommand
        {
            get
            {
                if (_LinkStudentCommand == null)
                {
                    _LinkStudentCommand = new GeneralCommand(
                        param => LinkStudent()
                        );
                }
                return _LinkStudentCommand;
            }
        }

        private void LinkStudent()
        {
            if (SelectedStudent != null && SelectedStudentModule != null)
            {
                this.handler.AddStudentToModule(SelectedStudent, SelectedStudentModule);
            }
        }

        internal void UpdateStudentLinkStatus(JObject obj)
        {
            JToken message = obj.SelectToken("data");

            if (message.ToString() == "Succes")
            {
                this.StudentLinkFailOpacity = 0;
                this.StudentLinkOpacity = 100;
            }
            else
            {
                this.StudentLinkFailOpacity = 100;
                this.StudentLinkOpacity = 0;
            }
        }
    }
}
