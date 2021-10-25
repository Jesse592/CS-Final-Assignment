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

        public StudentHandler(GradingDBContext gradingDBContext) : base()
        {
            GradingDBContext = gradingDBContext;
        }

        public void GetGrades(JObject student)
        {

        }

        public void GetAllGrades(JObject student)
        {
            Console.WriteLine("FUCKYEAH");
        }
        
        public void GetModules(JObject student)
        {

        }

        protected override void Init()
        {
            this.Actions.Add("GetGrades", GetGrades);
            this.Actions.Add("GetAllGrades", GetAllGrades);
            this.Actions.Add("GetModules", GetModules);
        }
    }
}
