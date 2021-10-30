using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grading_Administraton_Shared.Entities
{
    /// <summary>
    /// Grade class that will be send over the network
    /// </summary>
    public class Grade
    {
        /// <summary>
        /// Constructor that parses a Grade JSON object to a Grade object
        /// </summary>
        /// <param name="jObject">The json object to be parsed</param>
        public Grade(JObject jObject) {
            this.Time = jObject.SelectToken("Time").Value<DateTime>();
            this.NumericalGrade = jObject.SelectToken("NumericalGrade").Value<Double>();
            this.LetterGrade = jObject.SelectToken("LetterGrade").Value<String>();
            this.Delimiter = jObject.SelectToken("Delimiter").Value<Double>();
            this.Name = "-";
        }

        /// <summary>
        /// Constructor that parses a Grade JSON object to a Grade object
        /// </summary>
        /// <param name="jObject">The json object to be parsed</param>
        public Grade(JObject jObject, JToken name)
        {
            this.Time = jObject.SelectToken("Time").Value<DateTime>();
            this.NumericalGrade = jObject.SelectToken("NumericalGrade").Value<Double>();
            this.LetterGrade = jObject.SelectToken("LetterGrade").Value<String>();
            this.Delimiter = jObject.SelectToken("Delimiter").Value<Double>();
            this.Name = name.Value<string>();
        }
        
        /// <summary>
        /// Grade constructor
        /// </summary>
        /// <param name="time">The time of the grade</param>
        /// <param name="numericalGrade">Number grade</param>
        /// <param name="letterGrade">Letter grade</param>
        /// <param name="delimiter">The grade needed to pass</param>
        public Grade(DateTime time, double numericalGrade, string letterGrade, double delimiter)
        {
            Time = time;
            NumericalGrade = numericalGrade;
            LetterGrade = letterGrade;
            Delimiter = delimiter;
        }

        public DateTime Time { get; set; }
        public string Name { get; set; }

        public double NumericalGrade { get; set; }
        public string LetterGrade { get; set; }

        public double Delimiter { get; set; }

        public bool IsPassing()
        {
            return NumericalGrade >= Delimiter;
        }

    }
}
