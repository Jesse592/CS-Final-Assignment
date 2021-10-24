using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grading_Administration_Server.EntityFramework.models
{
    public class Grade
    {
        [Key]
        [Column(Order = 1)]
        public ModuleContribution Contribution { get; set; }

        [Key]
        [Column(Order = 2)]
        public DateTime Time { get; set; }

        public double NumericalGrade { get; set; }
        public string LetterGrade { get; set; }

        public double Delimiter { get; set; }

        public bool IsPassing()
        {
            return NumericalGrade >= Delimiter;
        }

        public Grade(ModuleContribution contribution, DateTime time, double numericalGrade, string letterGrade, double delimiter)
        {
            Contribution = contribution;
            Time = time;
            NumericalGrade = numericalGrade;
            LetterGrade = letterGrade;
            Delimiter = delimiter;
        }

        public Grading_Administraton_Shared.Entities.Grade ToSharedGrade()
        {
            return new Grading_Administraton_Shared.Entities.Grade(this.Time, this.NumericalGrade, this.LetterGrade, this.Delimiter);
        }
    }
}
