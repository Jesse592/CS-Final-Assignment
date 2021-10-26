using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grading_Administraton_Shared.Entities
{
    public class Module
    {
        public Module(JObject jObject, JArray teachers)
        {
            this.teachers = new List<User>();

            this.ModuleId = jObject.SelectToken("ModuleId").Value<Int32>();
            this.Name = jObject.SelectToken("Name").Value<String>();
            this.StartDate = jObject.SelectToken("StartDate").Value<DateTime>();
            this.EndDate = jObject.SelectToken("EndDate").Value<DateTime>();
            this.ETC = jObject.SelectToken("ETC").Value<Int32>();
            this.IsNumerical = jObject.SelectToken("IsNumerical").Value<Boolean>();

            foreach (JObject o in teachers)
            {
                this.teachers.Add(new User(o.SelectToken("UserId").Value<Int32>(), o.SelectToken("FirstName").Value<string>(), o.SelectToken("LastName").Value<string>(), o.SelectToken("DateOfBirth").Value<DateTime>(), o.SelectToken("Email").Value<string>(), o.SelectToken("UserType").Value<string>()));
            }
        }
        
        public Module(int moduleId, string name, DateTime startDate, DateTime endDate, int eTC, bool isNumerical)
        {
            ModuleId = moduleId;
            Name = name;
            StartDate = startDate;
            EndDate = endDate;
            ETC = eTC;
            IsNumerical = isNumerical;
        }

        public int ModuleId { get; set; }
        public string Name { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int ETC { get; set; }
        public bool IsNumerical { get; set; }

        public List<User> teachers { get; set; }
    }
}
