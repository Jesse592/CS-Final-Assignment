using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Grading_Administration_Server.EntityFramework.models
{
    public class ModuleContribution
    {
        [Key]
        public int ContributionId { get; set; }

        public User User { get; set; }

        public Module Module { get; set; }

        public ICollection<Grade> grades { get; set; }

    }

}
