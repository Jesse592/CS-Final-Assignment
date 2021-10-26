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

        [Required]
        public int UserId { get; set; }
        public User User { get; set; }

        [Required]
        public int ModuleId { get; set; }
        public virtual Module Module { get; set; }

        [Required]
        public virtual ICollection<Grade> grades { get; set; }

        public ModuleContribution()
        {
            grades = new List<Grade>();
        }

        public ModuleContribution(User user, Module module, ICollection<Grade> grades)
        {
            User = user;
            Module = module;
            this.grades = grades;
        }
    }

}
