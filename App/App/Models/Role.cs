using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace App.Models
{
    [Table("Roles")]
    public class Role
    {
        [Key]
        public int RoleId { get; set; }

        [Required]
        public User User { get; set; }

        [Required]
        public Library Library { get; set; }

        [Required]
        public UserRole UserRole { get; set; }

        
    }
    
    public enum UserRole { Default, Librarian, Manager }
}
