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
    /// Grade class that is stored in the database
    /// </summary>
    public class Grade
    {
        [Key]
        [Required]
        public int gradeID {get; set;}

        [Required]
        public int ContributionId { get; set; }
        public ModuleContribution Contribution { get; set; }

        public DateTime Time { get; set; }

        public double NumericalGrade { get; set; }

        [MaxLength(100)]
        [Column(TypeName = "varchar")]
        public string LetterGrade { get; set; }

        public double Delimiter { get; set; }

        /// <summary>
        /// Returns if this grade is a passing grade
        /// </summary>
        /// <returns>If this grade is a passing grade</returns>
        public bool IsPassing()
        {
            return NumericalGrade >= Delimiter;
        }

        /// <summary>
        /// Empty constructor for Grase (needed for database
        /// </summary>
        public Grade()
        {
        }

        /// <summary>
        /// Main constructor for Grade
        /// </summary>
        /// <param name="contribution">The user and module the grade is conneccted to</param>
        /// <param name="time">The time of the grade creation</param>
        /// <param name="numericalGrade">Number grade given</param>
        /// <param name="letterGrade">String grade given</param>
        /// <param name="delimiter">The value needed to pass</param>
        public Grade(ModuleContribution contribution, DateTime time, double numericalGrade, string letterGrade, double delimiter)
        {
            Contribution = contribution;
            Time = time;
            NumericalGrade = numericalGrade;
            LetterGrade = letterGrade;
            Delimiter = delimiter;
        }

        /// <summary>
        /// Transforms ths grade class to a class that is save to send to a user
        /// </summary>
        /// <returns></returns>
        public Grading_Administraton_Shared.Entities.Grade ToSharedGrade()
        {
            return new Grading_Administraton_Shared.Entities.Grade(this.Time, this.NumericalGrade, this.LetterGrade, this.Delimiter);
        }
    }
}
