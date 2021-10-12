using Grading_Administraton_Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradingAdmin_client
{
    class JSONWrapper
    {
        public static object WrapHeader(string command, object data)
        {
            return new
            {
                command = command,
                data = new
                {
                    data
                }
            };
        }

        public static object WrapLogin(string pasword, string username)
        {
            return new
            {
                username,
                pasword
            };
        }

        public static object WrapUser(User user)
        {
            return new
            {
                user
            };
        }

        public static object WrapModuleUser(Module module, User user)
        {
            return new
            {
                module,
                user
            };
        }

        public static object WrapModule(Module module)
        {
            return new
            {
                module
            };
        }

        public static object WrapGrade(Grade grade)
        {
            return new
            {
                grade
            };
        }
    }
}
