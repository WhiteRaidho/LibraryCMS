using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace App.ViewModels
{
    public class BookFormModel
    {
        [Required(ErrorMessage = "Tytuł jest wymagany")]
        [MaxLength(32, ErrorMessage = "Tytuł jest zbyt długi (max 32 znaków)")]
        [MinLength(1, ErrorMessage = "Tytuł jest zbyt krótki (min 1 znak)")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Opis jest wymagany")]
        [MaxLength(1024, ErrorMessage = "Opis jest zbyt długi (max 1024 znaków)")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Imię autora jest wymagane")]
        [MaxLength(32, ErrorMessage = "Imię jest zbyt długie (max 32 characters)")]
        [MinLength(2, ErrorMessage = "Imię jest zbyt krótkie (min 2 characters)")]
        public string AuthorName { get; set; }

        [Required(ErrorMessage = "Nazwisko autora jest wymagane")]
        [MaxLength(32, ErrorMessage = "Nazwisko jest zbyt długie (max 32 characters)")]
        [MinLength(2, ErrorMessage = "Nazwisko jest zbyt krótkie (min 2 characters)")]
        public string AuthorSurname { get; set; }

        public List<BookCopieModel> BookCopies { get; set; }
    }
}
