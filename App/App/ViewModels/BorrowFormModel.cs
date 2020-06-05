using System;
using System.ComponentModel.DataAnnotations;

namespace App.ViewModels
{
    public class BorrowFormModel
    {
        [Required(ErrorMessage = "Id jest wymagane")]
        public int BorrowId { get; set; }

        [Required(ErrorMessage = "Bibliotekarz jest wymagany")]
        public string LibrarianUserId { get; set; }

        public string ReturnLibrarianUserId { get; set; }

        [Required(ErrorMessage = "Użytkownik jest wymagany")]
        public string UserId { get; set; }

        [Required(ErrorMessage = "Książka jest wymagana")]
        public int BookId { get; set; }

        [Required(ErrorMessage ="Czas wypożyczenia jest wymagany")]

        public DateTime BorrowTime { get; set; }

        public DateTime? ReturnTime { get; set; }

        [Required(ErrorMessage ="Status wypożyczenia jest wymagany")]
        public int Status { get; set; }
    }
}
