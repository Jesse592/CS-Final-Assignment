using Grading_Administraton_Shared.Entities;
using GradingAdmin_client.Handlers;
using Gradings_Administration_client;
using Gradings_Administration_client.ViewModels;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

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
            this.Combined = new Dictionary<Module, List<Grade>>();

            this.student = student;
            this.handler = new StudentHandlerVM(student, this);
        }

        public string StudentName
        {
            get { return this.student.FirstName + " " + this.student.LastName; }
        }

        public string WelcomMessage
        {
            get
            {
                return "Goedendag, " + StudentName;
            }
        }

        private Dictionary<Module, List<Grade>> _Combined;
        public Dictionary<Module, List<Grade>> Combined
        {
            get { return _Combined; }
            set
            {
                _Combined = value;
            }
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

        public void AddModule(Module module)
        {
            App.Current.Dispatcher?.Invoke(() =>
            {
                this.Modules.Add(module);
            });
            
        }

        public void AddGrade(Grade grade)
        {
            App.Current.Dispatcher?.Invoke(() =>
            {
                this.Grades.Add(grade);
            });
        }

        private ICommand _DownloadListCommand;
        public ICommand DownloadListCommand
        {
            get
            {
                if (_DownloadListCommand == null)
                {
                    _DownloadListCommand = new GeneralCommand(
                        param => DownloadList()
                        );
                }
                return _DownloadListCommand;
            }
        }

        private void DownloadList()
        {
            int YIndex = 0;

            using (PdfDocument document = new PdfDocument())
            {
                PdfPage page = document.Pages.Add();

                PdfGraphics graphics = page.Graphics;

                PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 12);

                foreach (Module m in this.Combined.Keys)
                {
                    graphics.DrawString(m.Name, font, PdfBrushes.Black, new PointF(0, YIndex));
                    YIndex += 15;

                    foreach (Grade g in this.Combined[m])
                    {
                        graphics.DrawString("-" + g.Name + ": " + g.NumericalGrade, font, PdfBrushes.Black, new PointF(10, YIndex));
                        YIndex += 15;
                    }
                }

                document.Save("Cijferlijst.pdf");
            }
        }
    }
}
