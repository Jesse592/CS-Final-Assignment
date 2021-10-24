using Grading_Administraton_Shared.Entities;
using Gradings_Administration_client.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradingAdmin_client.ViewModels
{
    class TeacherViewModel : BaseViewModel
    {
        private User teacher;

        public TeacherViewModel(User teacher)
        {
            this.teacher = teacher;
        }
    }
}
