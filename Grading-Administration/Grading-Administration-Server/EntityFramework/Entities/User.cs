using Castle.MicroKernel.SubSystems.Conversion;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Grading_Administration_Server.EntityFramework.models
{
    /// <summary>
    /// Class that represents a user in the database
    /// </summary>
    public class User
    {

        public int UserId { get; set; }

        [MaxLength(100)]
        [Column(TypeName = "varchar")]
        public string FirstName { get; set; }

        [MaxLength(100)]
        [Column(TypeName = "varchar")]
        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        [MaxLength(100)]
        [Column(TypeName = "varchar")]
        public string Email { get; set; }

        [MaxLength(100)]
        [Column(TypeName = "varchar")]
        public string UserType { get; set; }

        public virtual ICollection<ModuleContribution> Modules { get; set; }

        /// <summary>
        /// Empty constructor for user
        /// </summary>
        public User()
        {
            Modules = new List<ModuleContribution>();
        }

        /// <summary>
        /// Transforms this user to a Shared user, save to send to clients
        /// </summary>
        /// <returns>The shared user</returns>
        public Grading_Administraton_Shared.Entities.User ToSharedUser()
        {
            return new Grading_Administraton_Shared.Entities.User(this.UserId, this.FirstName, this.LastName, this.DateOfBirth, this.Email, this.UserType);
        }
    }

}