using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace Grading_Administraton_Shared.Entities
{
    public class User
    {
        public User(JObject data)
        {
            this.UserId = data.SelectToken("UserId").Value<Int32>();
            this.FirstName = data.SelectToken("FirstName").Value<string>();
            LastName = data.SelectToken("LastName").Value<string>();
            DateOfBirth = data.SelectToken("DateOfBirth").Value<DateTime>();
            Email = data.SelectToken("Email").Value<string>();
            UserType = data.SelectToken("UserType").Value<string>();
        }

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