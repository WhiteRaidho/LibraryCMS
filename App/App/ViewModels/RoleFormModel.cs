using App.Models;
using System.ComponentModel.DataAnnotations;

namespace App.ViewModels
{
    public class RoleFormModel
    {
        public int RoleId { get; set; }

        [Required(ErrorMessage = "Użytkownik jest wymagany")]
        public string UserId { get; set; }

        [Required(ErrorMessage = "Biblioteka jest wymagana")]
        [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = "Biblioteka jest wymagana")]
        public int LibraryId { get; set; }

        [Required(ErrorMessage = "Nadanie roli jest wymagane")]
        public int UserRole { get; set; }
    }
}
