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
    }
}
