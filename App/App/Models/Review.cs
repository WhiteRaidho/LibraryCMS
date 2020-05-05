using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace App.Models
{
    [Table("Reviews")]
    public class Review
    {
        [Key]
        public int ReviewId { get; set; }

        [Required, Range(1, 10)]
        public int Rate { get; set; }

        [MaxLength(1024, ErrorMessage ="Review is too long (max 1024 characteres)")]
        public string Description { get; set; }

        [Required]
        public User User { get; set; }

        [Required]
        public Book Book { get; set; }
    }
}
