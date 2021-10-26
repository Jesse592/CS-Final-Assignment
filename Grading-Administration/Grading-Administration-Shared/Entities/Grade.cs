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
    public class Grade
    {
        public Grade(JObject jObject)
        {
            this.Time = jObject.SelectToken("mcGrades.Time").Value<DateTime>();
            this.NumericalGrade = jObject.SelectToken("mcGrades.NumericalGrade").Value<Double>();
            this.LetterGrade = jObject.SelectToken("mcGrades.LetterGrade").Value<String>();
            this.Delimiter = jObject.SelectToken("mcGrades.Delimiter").Value<Double>();
        }
        
        public Grade(DateTime time, double numericalGrade, string letterGrade, double delimiter)
        {
            Time = time;
            NumericalGrade = numericalGrade;
            LetterGrade = letterGrade;
            Delimiter = delimiter;
        }

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
