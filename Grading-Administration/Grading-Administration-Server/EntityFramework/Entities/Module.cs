using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grading_Administration_Server.EntityFramework.models
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

        public Module(int moduleId, string name, DateTime startDate, DateTime endDate, int eTC, bool isNumerical, ICollection<ModuleContribution> participants)
        {
            ModuleId = moduleId;
            Name = name;
            StartDate = startDate;
            EndDate = endDate;
            ETC = eTC;
            IsNumerical = isNumerical;
            Participants = participants;
        }

        public Grading_Administraton_Shared.Entities.Module ToSharedModule()
        {
            return new Grading_Administraton_Shared.Entities.Module(this.ModuleId, this.Name, this.StartDate, this.EndDate, this.ETC, this.IsNumerical);
        }
    }
}
