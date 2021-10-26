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
    class StudentViewModel : BaseViewModel, INotifyPropertyChanged
    {
        public User student;
        private StudentHandlerVM handler;

        public event PropertyChangedEventHandler PropertyChanged;

        public StudentViewModel(User student)
        {

            this.Grades = new ObservableCollection<Grade>();
            this.Modules = new ObservableCollection<Module>();

            this.student = student;
            this.handler = new StudentHandlerVM(student, this);
        }

        public string StudentName
        {
            get { return this.student.FirstName + " " + this.student.LastName; }
        }

        public int StudentID
        {
            get { return this.student.UserId; }
        }

        private ObservableCollection<Grade> _Grades;
        public ObservableCollection<Grade> Grades
        {
            get{ return _Grades; }
            set
            {
                _Grades = value;
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Grades"));
            }
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

        private Grade _SelectedGrade;
        public Grade SelectedGrade
        {
            get { return _SelectedGrade; }
            set
            {
                _SelectedGrade = value;
                GradePopUp popup = new GradePopUp(SelectedGrade);
                popup.Show();
            }
        }

        private Module _SelectedModule;
        public Module SelectedModule
        {
            get { return _SelectedModule; }
            set
            {
                _SelectedModule = value;
                ModulePopUp popup = new ModulePopUp(SelectedModule);
                popup.Show();
            }
        }

        
    }
}
