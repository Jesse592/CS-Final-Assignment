using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradingAdministration_server.models
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

    }
}
