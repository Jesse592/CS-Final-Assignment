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
    /// <summary>
    /// Command that is called when a databinded button is pressed
    /// </summary>
    class UpdateViewCommand : ICommand
    {
        private LoginViewModel LginView;

        /// <summary>
        /// Constructor of the command
        /// </summary>
        /// <param name="loginModel">The login panel to switch to</param>
        public UpdateViewCommand(LoginViewModel loginModel)
        {
            this.LginView = loginModel;
        }

        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// Tells if the command can be executed
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns>If the command can execute</returns>
        public bool CanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Executes the command
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            
        }
    }
}
