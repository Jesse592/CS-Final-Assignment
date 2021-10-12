using Grading_Administration_Server.EntityFramework.models;
using GradingAdmin_client.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradingAdmin_client.ViewModels
{
    class StudentViewModel
    {
        public User student;
        private StudentHandlerVM handler;

        public StudentViewModel(User student)
        {
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

        private ICollection<Grade> _Grades;
        public ICollection<Grade> Grades
        {
            get{ return _Grades; }
            set
            {
                _Grades = value;
            }
        }
    }
}
