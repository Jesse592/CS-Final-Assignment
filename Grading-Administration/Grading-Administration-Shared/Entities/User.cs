using System;
using System.Collections.Generic;

namespace Grading_Administraton_Shared.Entities
{
    public class User
    {
        public User(int userId, string firstName, string lastName, DateTime dateOfBirth, string email, string userType)
        {
            UserId = userId;
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            Email = email;
            UserType = userType;
        }

        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Email { get; set; }
        public string UserType { get; set; }
    }

}