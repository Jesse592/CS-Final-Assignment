using Grading_Administraton_Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gradings_Administration_client.ViewModels
{
    class ModulePopUpViewModel
    {
        private Module module;

        public ModulePopUpViewModel(Module module)
        {
            this.module = module;
        }

        public string Name
        {
            get { return this.module.Name; }
        }

        public string StartDate
        {
            get { return "Start datum: \t" + this.module.StartDate.Day + "/" + this.module.StartDate.Month + "/" + this.module.StartDate.Year; }
        }

        public string EndDate
        {
            get { return "Eind datum: \t" + this.module.EndDate.Day + "/" + this.module.EndDate.Month + "/" + this.module.EndDate.Year; }
        }

        public string ECT
        {
            get { return "Te behalen ECT's: \t" + this.module.ETC; }
        }

        public string Teachers
        {
            get
            {
                string teachers = "";

                for (int i = 0; i < this.module.teachers.Count; i++)
                {
                    if(i == 0)
                    {
                        teachers += this.module.teachers[i].FirstName + " " + this.module.teachers[i].LastName;
                    } 
                    else
                    {
                        teachers += ", " + this.module.teachers[i].FirstName + " " + this.module.teachers[i].LastName;
                    }
                }

                return "Docenten bij module: " + teachers;
            }
        }
    }
}
