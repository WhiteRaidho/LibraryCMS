using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace App.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Login jest wymagany")]
        [MinLength(6, ErrorMessage = "Login jest za krótki, minimalna długość to 6 znaków"), MaxLength(32, ErrorMessage = "Login jest za długi, maksymalna długość to 32 znaki")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Email jest wymagany")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Email jest niepoprawny.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Hasło jest wymagane")]
        [MinLength(6, ErrorMessage = "Hasło jest za krótkie, minimalna długość to 6 znaków"), MaxLength(32, ErrorMessage = "Hasło jest za długie, maksymalna długość to 32 znaki")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Imię jest wymagane")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Nazwisko jest wymagane")]
        public string LastName { get; set; }
    }
}
