using GradingAdmin_client.ViewModels;
using Gradings_Administration_client.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Gradings_Administration_client.Commands
{
    class UpdateViewCommand : ICommand
    {
        private LoginViewModel LginView;

        public UpdateViewCommand(LoginViewModel loginModel)
        {
            this.LginView = loginModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            /*
                switch (LginView.User.UserType)
                {
                    case "Student":
                        this.LginView.SelectedViewModel = new StudentViewModel(LginView.User);
                        break;
                    case "Teacher":
                        this.LginView.SelectedViewModel = new TeacherViewModel(LginView.User);
                        break;
                    case "Admin":
                        this.LginView.SelectedViewModel = new AdminViewModel();
                        break;
                    default:
                        this.LginView.SendError("Onjuiste gebruiker");
                        break;
                }
            */

        }
    }
}
