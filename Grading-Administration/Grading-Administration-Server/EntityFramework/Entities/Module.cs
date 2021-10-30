using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grading_Administration_Server.EntityFramework.models
{
    /// <summary>
    /// Class that represents a module / course
    /// </summary>
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

        /// <summary>
        /// Empty constructor needed for database
        /// </summary>
        public Module()
        {
            Participants = new List<ModuleContribution>();
        }

        /// <summary>
        /// Normal constructor for Module
        /// </summary>
        /// <param name="name">The name of the module</param>
        /// <param name="startDate">The start date of the module</param>
        /// <param name="endDate">The enddate of the module</param>
        /// <param name="eTC">The amount of ETC this module is worth</param>
        /// <param name="isNumerical">Tells if the module is ended with a number</param>
        /// <param name="participants">List off all the participants</param>
        public Module(string name, DateTime startDate, DateTime endDate, int eTC, bool isNumerical, ICollection<ModuleContribution> participants)
        {
            Name = name;
            StartDate = startDate;
            EndDate = endDate;
            ETC = eTC;
            IsNumerical = isNumerical;
            Participants = participants;
        }

        /// <summary>
        /// Method that transforms this class to a shared module, save to send to client
        /// </summary>
        /// <returns>The shared module variant</returns>
        public Grading_Administraton_Shared.Entities.Module ToSharedModule()
        {
            return new Grading_Administraton_Shared.Entities.Module(this.ModuleId, this.Name, this.StartDate, this.EndDate, this.ETC, this.IsNumerical);
        }
    }
}
