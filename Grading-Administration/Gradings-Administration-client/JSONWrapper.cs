using Grading_Administration_Server.EntityFramework.models;
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

        public static object WrapGetModules(User user)
        {
            return new
            {
                user
            };
        }

        public static object WrapGetAllGrade(User user)
        {
            return new
            {
                user
            };
        }

        public static object WrapGetGrade(Module module, User user)
        {
            return new
            {
                module,
                user
            };
        }

        public static object WrapTeachersFromModule(Module module)
        {
            return new
            {
                module
            };
        }

        public static object WrapAddGrade(Grade grade)
        {
            return new
            {
                grade
            };
        }
    }
}
