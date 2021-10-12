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
        public Module(JObject jObject)
        {
            this.ModuleId = jObject.GetValue("ModuleID").Value<Int32>();
            this.Name = jObject.GetValue("Name").Value<String>();
            this.StartDate = jObject.GetValue("StartDate").Value<DateTime>();
            this.EndDate = jObject.GetValue("EndDate").Value<DateTime>();
            this.ETC = jObject.GetValue("ETC").Value<Int32>();
            this.IsNumerical = jObject.GetValue("IsNumerical").Value<Boolean>();
        }

        public int ModuleId { get; set; }
        public string Name { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int ETC { get; set; }
        public bool IsNumerical { get; set; }
    }
}
