using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradingAdministration_server
{
    struct Setting
    {
        public int PortNumber { get; set; }
        public string IPAdress { get; set; }
        public string DBPassword { get; set; }
        public string DBUserName { get; set; }
        public string DBIPAdress { get; set; }
    }
}
