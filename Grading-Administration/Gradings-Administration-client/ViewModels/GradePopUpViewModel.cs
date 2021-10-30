using Grading_Administraton_Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gradings_Administration_client.ViewModels
{
    class GradePopUpViewModel
    {
        private Grade grade;

        public GradePopUpViewModel(Grade grade)
        {
            this.grade = grade;
        }

        public string Name
        {
            get { return grade.Name; }
        }

        public string Date
        {
            get { return "Datum: " + grade.Time.ToString("dddd dd MMMM yyyy"); }
        }

        public string Norm
        {
            get { return "Overgangsnorm: " + grade.Delimiter; }
        }

        public string Passed
        {
            get
            {
                if (grade.IsPassing())
                {
                    return "Behaald: Ja";
                }

                return "Behaald: Nee";
            }
        }
    }
}
