using System;
using System.Collections.Generic;

namespace Grading_Administration_Server.EntityFramework.models
{
    public class User
    {

        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Email { get; set; }
        public UserType UserType { get; set; }

        public ICollection<ModuleContribution> Modules { get; set; }
    }

}