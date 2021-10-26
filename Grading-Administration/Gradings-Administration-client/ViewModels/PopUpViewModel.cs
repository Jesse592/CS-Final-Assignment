using Grading_Administraton_Shared.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gradings_Administration_client.ViewModels
{
    class PopUpViewModel : INotifyPropertyChanged
    {
        private Grade grade;
        public event PropertyChangedEventHandler PropertyChanged;

        public PopUpViewModel(Grade grade)
        {
            this.grade = grade;
        }

        public string Grade
        {
            get { return this.grade.NumericalGrade.ToString(); }
        }
    }
}
