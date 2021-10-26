using Grading_Administraton_Shared.Entities;
using Gradings_Administration_client.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Gradings_Administration_client
{
    /// <summary>
    /// Interaction logic for GradePopUp.xaml
    /// </summary>
    public partial class GradePopUp : Window
    {
        public GradePopUp(Grade grade)
        {
            InitializeComponent();
            this.Content = new PopUpViewModel(grade);
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }
    }
}
