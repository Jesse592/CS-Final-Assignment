using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Grading_Administration_Server.EntityFramework.models
{
    /// <summary>
    /// Class that represents a contribution of a user to a module
    /// </summary>
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

        /// <summary>
        /// Empty constructor for ModuleContribution
        /// </summary>
        public ModuleContribution()
        {
            grades = new List<Grade>();
        }

        /// <summary>
        /// Normal constructor for ModuleContribution
        /// </summary>
        /// <param name="user">The user in the contribution</param>
        /// <param name="module">The module in this contribution</param>
        /// <param name="grades">the grades the user has in this module</param>
        public ModuleContribution(User user, Module module, ICollection<Grade> grades)
        {
            User = user;
            Module = module;
            this.grades = grades;
        }
    }

}
