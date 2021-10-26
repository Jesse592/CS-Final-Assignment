using Castle.MicroKernel.SubSystems.Conversion;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Grading_Administration_Server.EntityFramework.models
{
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

        public User()
        {
            Modules = new List<ModuleContribution>();
        }

        public Grading_Administraton_Shared.Entities.User ToSharedUser()
        {
            return new Grading_Administraton_Shared.Entities.User(this.UserId, this.FirstName, this.LastName, this.DateOfBirth, this.Email, this.UserType);
        }
    }

}