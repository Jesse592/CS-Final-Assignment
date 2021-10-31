using GradingAdmin_client.ViewModels;
using Gradings_Administration_client;
using Grading_Administraton_Shared.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
using System.Collections.ObjectModel;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using System.Drawing;

namespace GradingAdmin_client.Handlers
{
    /// <summary>
    /// Handles all the communication and data for the student viewmodel
    /// </summary>
    class StudentHandlerVM
    {
        private ConnectionManager manager;
        private StudentViewModel vm;

        private User currentUser;

        /// <summary>
        /// Constructor for the student handler
        /// </summary>
        /// <param name="student">The student the user is logged in as</param>
        /// <param name="view">The view this handler handles</param>
        public StudentHandlerVM(User student, StudentViewModel view)
        {
            // Getting the manager to the server that is active
            this.manager = ConnectionManager.GetConnectionManager();
            this.vm = view;

            this.currentUser = student;

            // Requesting all the needed grading data on startup
            GetStudentData();
        }

        /// <summary>
        /// Asks the server for all the grades in for this user
        /// </summary>
        public void GetStudentData()
        {
            
            // Building the command and sending to the server
            this.manager.SendCommand(
                JObject.FromObject(
                    JSONWrapper.WrapHeader("GetAllGrades", JSONWrapper.WrapUser(this.currentUser))
                    ), StudentDataCallback);
        }

        /// <summary>
        /// Method that is called when the server responds to the getgrades command
        /// </summary>
        /// <param name="jObject">The response from the server</param>
        public void StudentDataCallback(JObject jObject)
        {
            JToken array = jObject.SelectToken("data") as JArray;

            Dictionary<Module, List<Grade>> GradesCombined = new Dictionary<Module, List<Grade>>();

            // Looping trough all the modules in the data
            foreach (JObject o in array)
            {
                this.vm.AddModule(new Module(o.SelectToken("module") as JObject, o.SelectToken("teachers") as JArray));
                List<Grade> gradeWithKey = new List<Grade>();

                // Going to all the grades in this module
                foreach(JObject g in o.SelectToken("mcGrades") as JArray)
                {
                    this.vm.AddGrade(new Grade(g, o.SelectToken("module.Name")));
                    gradeWithKey.Add(new Grade(g, o.SelectToken("module.Name")));
                }

                GradesCombined.Add(new Module(o.SelectToken("module") as JObject, o.SelectToken("teachers") as JArray), gradeWithKey);
            }

            // Calling the VM to set the list
            this.vm.Grades = new ObservableCollection<Grade>(this.vm.Grades.OrderByDescending(grade => grade.Time).ToList());
            this.vm.Modules = new ObservableCollection<Module>(this.vm.Modules.OrderBy(module => module.EndDate));
            this.vm.Combined = new Dictionary<Module, List<Grade>>(GradesCombined);
        }

        /// <summary>
        /// Method that handles creating a pdf from a grading list and saving it to a file
        /// </summary>
        /// <param name="Combined">The list</param>
        /// <param name="path"></param>
        public void DownloadList(Dictionary<Module, List<Grade>> Combined, string path)
        {
            int YIndex = 0;

            using (PdfDocument document = new PdfDocument())
            {
                PdfPage page = document.Pages.Add();

                PdfGraphics graphics = page.Graphics;

                PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 12);

                foreach (Module m in Combined.Keys)
                {
                    graphics.DrawString(m.Name, font, PdfBrushes.Black, new PointF(0, YIndex));
                    YIndex += 15;

                    foreach (Grade g in Combined[m])
                    {
                        graphics.DrawString("-" + g.Name + ": " + g.NumericalGrade, font, PdfBrushes.Black, new PointF(10, YIndex));
                        YIndex += 15;
                    }
                }

                document.Save(path);
            }
        }

    }
}
