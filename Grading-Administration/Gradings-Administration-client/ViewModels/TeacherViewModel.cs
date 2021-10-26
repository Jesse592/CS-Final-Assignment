using Grading_Administraton_Shared.Entities;
using Gradings_Administration_client.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradingAdmin_client.ViewModels
{
    class TeacherViewModel : BaseViewModel, INotifyPropertyChanged
    {
        private User teacher;

        public event PropertyChangedEventHandler PropertyChanged;

        public TeacherViewModel(User teacher)
        {
            this.teacher = teacher;

            this.Modules = new ObservableCollection<Module>();
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
    }
}
