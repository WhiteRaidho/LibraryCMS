using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace App.Models
{
    [Table("Users")]
    public class User
    {
        [Key]
        public string UserID { get; set; }

        [MinLength(6, ErrorMessage = "User name too short"), MaxLength(32, ErrorMessage = "User name too long")]
        [Required, MinLength(6, ErrorMessage = "User name too short"), MaxLength(32, ErrorMessage = "User name too long")]
        public string UserName { get; set; }
        [MinLength(6, ErrorMessage = "Password too short"), MaxLength(32, ErrorMessage = "Password too long")]
        [Required, MinLength(6, ErrorMessage = "Password too short"), MaxLength(32, ErrorMessage = "Password too long")]
        public string UserPassword { get; set; }

        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Email is not valid.")]
        [Required, RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Email is not valid.")]
        public string Email { get; set; }

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        //[DataType(DataType.Text)]
        //public DateTime BirthDate { get; set; }

        public string UserRoles { get; set; }

        public ICollection<Book> Books { get; set; }
        public ICollection<Review> Reviews { get; set; }
    }
}
