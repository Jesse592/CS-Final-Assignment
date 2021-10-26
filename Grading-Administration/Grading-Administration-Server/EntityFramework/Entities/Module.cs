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
        [Key]
        [Required]
        public int ModuleId { get; set; }

        [MaxLength(100)]
        [Column(TypeName = "varchar")]
        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int ETC { get; set; }

        public bool IsNumerical { get; set; }

        [Required]
        public ICollection<ModuleContribution> Participants { get; set; }

        public override string ToString()
        {
            return $"{ModuleId} - {Name}";
        }

        public Module()
        {
            Participants = new List<ModuleContribution>();
        }

        public Module(string name, DateTime startDate, DateTime endDate, int eTC, bool isNumerical, ICollection<ModuleContribution> participants)
        {
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
