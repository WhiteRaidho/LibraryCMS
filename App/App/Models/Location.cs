using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace App.Models
{
    [Table("Locations")]
    public class Location
    {
        [Key]
        public int LocationId { get; set; }

        [Required, MaxLength(32, ErrorMessage = "Name is to long (max 32 characters)")]
        public string Name { get; set; }

        [RegularExpression(@"\d{2}-\d{3}")]
        public string ZipCode { get; set; }

        [MaxLength(64, ErrorMessage ="Street name is to long (max 64 characters)")]
        public string Street { get; set; }

        public ICollection<Library> Libraries { get; set; }
    }
}
