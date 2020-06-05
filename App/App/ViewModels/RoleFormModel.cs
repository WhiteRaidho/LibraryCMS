using App.Models;
using System.ComponentModel.DataAnnotations;

namespace App.ViewModels
{
    public class RoleFormModel
    {
        [Required(ErrorMessage = "Id jest wymagane")]
        public int RoleId { get; set; }

        [Required(ErrorMessage = "Użytkownik jest wymagany")]
        public string UserId { get; set; }

        [Required(ErrorMessage = "Biblioteka jest wymagana")]
        public int LibraryId { get; set; }

        [Required(ErrorMessage = "Nadanie roli jest wymagane")]
        public UserRole UserRole { get; set; }
    }
}
