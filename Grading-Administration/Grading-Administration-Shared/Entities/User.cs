using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace Grading_Administraton_Shared.Entities
{
    /// <summary>
    /// User class that is send to the client
    /// </summary>
    public class User
    {
        /// <summary>
        /// Constructor that parses a User JSON object to a User object
        /// </summary>
        /// <param name="jObject">The json object to be parsed</param>
        public User(JObject data)
        {
            this.UserId = data.SelectToken("UserId").Value<Int32>();
            this.FirstName = data.SelectToken("FirstName").Value<string>();
            LastName = data.SelectToken("LastName").Value<string>();
            DateOfBirth = data.SelectToken("DateOfBirth").Value<DateTime>();
            Email = data.SelectToken("Email").Value<string>();
            UserType = data.SelectToken("UserType").Value<string>();
        }


        /// <summary>
        /// Constructor for User
        /// </summary>
        /// <param name="userId">The userID</param>
        /// <param name="firstName">The first name of the user</param>
        /// <param name="lastName">The last name of the user</param>
        /// <param name="dateOfBirth">The Date of birth of the user</param>
        /// <param name="email">The email adress of the user</param>
        /// <param name="userType">The usertype of the user</param>
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