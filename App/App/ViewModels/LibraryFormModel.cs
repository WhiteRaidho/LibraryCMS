using System.ComponentModel.DataAnnotations;

namespace App.ViewModels
{
    public class LibraryFormModel
    {
        [Required(ErrorMessage = "Id jest wymagane")]
        public int LibraryId { get; set; }
        [Required]
        [MaxLength(32, ErrorMessage = "Nazwa może zawierać maksymalnie 32 znaki")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Miejscowość jest wymagana")]
        public int LocationId { get; set; }
    }
}
