using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace App.Models
{
    [Table("Books")]
    public class Book
    {
        [Key]
        public int BookId { get; set; }

        [Required]
        [MaxLength(32, ErrorMessage = "Book title is to long (max 32 characters)")]
        [MinLength(1, ErrorMessage = "Book title is to short (min 1 characters)")]
        public string Title { get; set; }

        [MaxLength(1024, ErrorMessage = "Description is to long (max 1024 characters)")]
        public string Description { get; set; }

        [Required]
        [MaxLength(32, ErrorMessage = "Name is to long (max 32 characters)")]
        [MinLength(2, ErrorMessage = "Name is to short (min 2 characters)")]
        public string AuthorName { get; set; }

        [Required]
        [MaxLength(32, ErrorMessage = "Surname is to long (max 32 characters)")]
        [MinLength(2, ErrorMessage = "Surname is to short (min 2 characters)")]
        public string AuthorSurname { get; set; }

        [Required]
        public Library Library { get; set; }

        public ICollection<Borrow> Borrows { get; set; }

        [NotMapped]
        public string AuthorFullName { get => AuthorName + ' ' + AuthorSurname; }
    }
}
