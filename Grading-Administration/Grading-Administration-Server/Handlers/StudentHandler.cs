using Grading_Administration_Server.EntityFramework;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grading_Administration_Server.Handlers
{
    class StudentHandler : Handler
    {
        private readonly GradingDBContext GradingDBContext;

        public void GetGrades(JObject student)
        {
            Console.WriteLine("FUCKYEAH");
        }

        public void GetModules(JObject student)
        {

        }

        protected override void Init()
        {
            this.Actions.Add("GetGrades", GetGrades);
            this.Actions.Add("GetModules", GetModules);
        }
    }
}
