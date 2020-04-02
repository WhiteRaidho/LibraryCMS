using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace App.Models
{
    [Table("Libraries")]
    public class Library
    {
        [Key]
        public int LibraryId { get; set; }

        [Required, MaxLength(32, ErrorMessage = "Name is to long (max 32 characters)")]
        public string Name { get; set; }

        [Required]
        public Location Location { get; set; }
    }
}
