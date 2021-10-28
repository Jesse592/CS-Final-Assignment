using Grading_Administraton_Shared.Entities;
using GradingAdmin_client;
using GradingAdmin_client.Handlers;
using GradingAdmin_client.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Gradings_Administration_client.ViewModels
{
    class AddGradeViewModel : INotifyPropertyChanged
    {
        private Module module;
        private User student;
        private TeacherHandlerVM handler;

        public event PropertyChangedEventHandler PropertyChanged;

        public AddGradeViewModel(Module module, User student, TeacherHandlerVM handler)
        {
            this.module = module;
            this.student = student;
            this.handler = handler;

            this.ErrorOpacity = 0;
        }

        private string _GradeText;
        public string GradeText
        {
            get { return _GradeText; }
            set
            {
                _GradeText = value;
            }
        }

        private int _ErrorOpacity;
        public int ErrorOpacity
        {
            get { return _ErrorOpacity; }
            set
            {
                _ErrorOpacity = value;
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ErrorOpacity"));
            }
        }

        private ICommand _AddGrade;
        public ICommand AddGrade
        {
            get
            {
                if (_AddGrade == null)
                {
                    _AddGrade = new GeneralCommand(
                        param => SendGrade()
                        );;
                }
                return _AddGrade;
            }

        }

        private void SendGrade()
        {
            string letter = "";

            if (double.TryParse(GradeText, NumberStyles.AllowDecimalPoint, CultureInfo.CurrentCulture, out var parsedNumber)) {
                if (double.Parse(GradeText) >= 5.5)
                {
                    letter = "G";
                } 
                else
                {
                    letter = "O";
                }

                this.ErrorOpacity = 0;

                Grade NewGrade = new Grade(DateTime.Now, double.Parse(GradeText), letter, 5.5);
                this.handler.AddGrade(this.module, NewGrade, this.student);
            } 
            else
            {
                this.ErrorOpacity = 100;
            }

            
            
            
        }
    }
}
