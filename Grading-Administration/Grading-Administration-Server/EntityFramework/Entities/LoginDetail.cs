using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grading_Administration_Server.EntityFramework.models
{
    /// <summary>
    /// Class that holds login details of a user
    /// </summary>
    public class LoginDetail
    {
        [Required]
        public int UserId { get; set; }
        public User User { get; set; }

        [Key]
        [MaxLength(100)]
        [Column(TypeName = "varchar")]
        public string UserName { get; set; }

        [Required]
        [MaxLength(100)]
        [Column(TypeName = "varchar")]
        public string Password { get; set; }

    }
}
