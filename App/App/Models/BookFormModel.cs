using System.ComponentModel.DataAnnotations;

namespace App.Models
{
    public class BookFormModel
    {
        [Required(ErrorMessage = "ID jest wymagane")]
        public int BookId { get; set; }

        [Required(ErrorMessage = "Tytuł jest wymagany")]
        [MaxLength(32, ErrorMessage = "Tytuł może zawierać maksymalnie 32 znaki")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Imie autora jest wymagane")]
        [MaxLength(32, ErrorMessage = "Imie autora może zawierać maksymalnie 32 znaki")]
        public string AuthorName { get; set; }

        [Required(ErrorMessage = "Nazwisko autora jest wymagane")]
        [MaxLength(32, ErrorMessage = "Nazwisko autora może zawierać maksymalnie 32 znaki")]
        public string AuthorSurname { get; set; }

        [Required(ErrorMessage = "Biblioteka jest wymagana")]
        public int LibraryId { get; set; }

        [MaxLength(1024, ErrorMessage = "Opis może zawierać maksymalnie 1024 znaki")]
        public string Description { get; set; }
    }
}
