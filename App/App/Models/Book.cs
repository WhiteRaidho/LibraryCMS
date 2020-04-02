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
        [MinLength(2, ErrorMessage = "Book title is to short (min 2 characters)")]
        public string Title { get; set; }

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

        //TODO: Add User relation 1 user : many books

        [NotMapped]
        public string AuthorFullName { get => AuthorName + ' ' + AuthorSurname; }
    }
}
