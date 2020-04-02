using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace App.Models
{
    public class AppUser
    {
        [Key]
        public string UserID { get; set; }

        [MinLength(6, ErrorMessage = "User name too short"), MaxLength(32, ErrorMessage = "User name too long")]
        public string UserName { get; set; }
        [MinLength(6, ErrorMessage = "Password too short"), MaxLength(32, ErrorMessage = "Password too long")]
        public string UserPassword { get; set; }

        //[RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Email is not valid.")]
        //public string Email { get; set; }

        //public string FirstName { get; set; }
        //public string LastName { get; set; }

        //[DataType(DataType.Text)]
        //public DateTime BirthDate { get; set; }
    }
}
