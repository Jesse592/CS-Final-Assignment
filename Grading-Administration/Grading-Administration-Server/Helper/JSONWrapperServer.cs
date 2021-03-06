using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grading_Administration_Server.Helper
{
    /// <summary>
    /// Class that holds wrapper methods for JSON object
    /// </summary>
    public static class JSONWrapperServer
    {
        public static object WrapHeader(string command, object data, int serial)
        {
            return new
            {
                command = command,
                data = data,
                serial = serial
            };
        }

        public static object WrapHeader(string command, object data)
        {
            return new
            {
                command = command,
                data = data
            };
        }

        public static object LoginFailed(int serial)
        {
            return WrapHeader("login", new
            {
                message = "Failed login"
            }, serial);
        }

        public static object LoginCorrect(Grading_Administraton_Shared.Entities.User user, int serial)
        {
            return WrapHeader("login", new
            {
                message = "Succesfull login",
                user = user
            }, serial);
        }

        public static object GenericList(string command, List<object> data, int serial)
        {
            return WrapHeader(command, data, serial);
        }

        public static object GetAllModules(List<Grading_Administraton_Shared.Entities.Module> data, int serial)
        {
            return WrapHeader("GetAllModules", data, serial);
        }

        public static object GetAllUsers(List<Grading_Administraton_Shared.Entities.User> data, int serial)
        {
            return WrapHeader("GetAllUsers", data, serial);
        }

        public static object AcknowledgeSucces(int serial)
        {
            return WrapHeader("Acknowledge", "Succes", serial);
        }

        public static object AcknowledgeFailed(int serial)
        {
            return WrapHeader("Acknowledge", "Failed", serial);
        }
    }
}
