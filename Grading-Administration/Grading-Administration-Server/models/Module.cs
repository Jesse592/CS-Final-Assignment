using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradingAdministration_server.models
{
    public class Module
    {
        public int ModuleId { get; set; }
        public string Name { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        
        public int ETC { get; set; }
        public bool IsNumerical { get; set; }

        public ICollection<ModuleContribution> Participants { get; set; }

        public override string ToString()
        {
            return $"{ModuleId} - {Name}";
        }
    }
}
