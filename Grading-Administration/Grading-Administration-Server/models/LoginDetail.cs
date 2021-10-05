using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradingAdministration_server.models
{
    public class LoginDetail
    {

        public User User { get; set; }

        [Key]
        public string UserName { get; set; }
        public string Password { get; set; }

    }
}
