using Grading_Administraton_Shared.Entities;
using GradingAdmin_client.Handlers;
using Gradings_Administration_client;
using Gradings_Administration_client.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GradingAdmin_client.ViewModels
{
    public class TeacherViewModel : BaseViewModel, INotifyPropertyChanged
    {
        private User teacher;
        private TeacherHandlerVM handler;
        private AddGradeWindow window;

        public event PropertyChangedEventHandler PropertyChanged;

        public TeacherViewModel(User teacher)
        {
            this.teacher = teacher;
            this.handler = new TeacherHandlerVM(this);

            this.Modules = new ObservableCollection<Module>();
            this.Students = new ObservableCollection<User>();
        }

        private ObservableCollection<Module> _Modules;
        public ObservableCollection<Module> Modules
        {
            get { return _Modules; }
            set
            {
                _Modules = value;
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Modules"));
            }
        }

        private ObservableCollection<User> _Students;
        public ObservableCollection<User> Students
        {
            get { return _Students; }
            set
            {
                _Students = value;
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Students"));
            }
        }

        internal void AddModule(Module module)
        {
            App.Current.Dispatcher?.Invoke(() =>
            {
                this.Modules.Add(module);
            });
        }

        internal void AddStudent(User user)
        {
            App.Current.Dispatcher?.Invoke(() =>
            {
                this.Students.Add(user);
            });
        }

        private Module _SelectedModule;
        public Module SelectedModule
        {
            get { return _SelectedModule; }
            set
            {
                _SelectedModule = value;
                this.handler.GetStudentsPerModule(SelectedModule);
            }
        }

        private User _SelectedStudent;
        public User SelectedStudent
        {
            get { return _SelectedStudent; }
            set
            {
                _SelectedStudent = value;
                this.window = new AddGradeWindow(SelectedModule, SelectedStudent, this.handler);
                this.window.Show();
            }
        }

        public void CloseWindow(string message)
        {
            this.window.Close();
            MessageBox.Show(message);
        }
    }
}
