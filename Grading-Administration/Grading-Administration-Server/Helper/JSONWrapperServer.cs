using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grading_Administration_Server.Helper
{
    public static class JSONWrapperServer
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

        public static object LoginFailed()
        {
            return WrapHeader("login", new
            {


            });
        }

    }
}
