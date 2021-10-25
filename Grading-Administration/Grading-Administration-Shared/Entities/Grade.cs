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
            this.Time = jObject.GetValue("Time").Value<DateTime>();
            this.NumericalGrade = jObject.GetValue("NumericalGrade").Value<Double>();
            this.LetterGrade = jObject.GetValue("LetterGrade").Value<String>();
            this.Delimiter = jObject.GetValue("Delimiter").Value<Double>();
        }
        
        public Grade(DateTime time, double numericalGrade, string letterGrade, double delimiter)
        {
            Time = time;
            NumericalGrade = numericalGrade;
            LetterGrade = letterGrade;
            Delimiter = delimiter;
        }

        public Grade(Grading_Administration_Server.EntityFramework.models.Grade grade)
        {
            Time = grade.Time;
            NumericalGrade = grade.NumericalGrade;
            LetterGrade = grade.LetterGrade;
            Delimiter = grade.Delimiter;
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
