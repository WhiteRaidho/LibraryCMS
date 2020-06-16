using System.ComponentModel.DataAnnotations;

namespace App.ViewModels
{
    public class LocationFormModel
    {
        public int LocationId { get; set; }

        [Required(ErrorMessage = "Miasto jest wymagane")]
        [MaxLength(32, ErrorMessage = "Miasto może zawierać do 32 znaków")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Kod pocztowy jest wymagany")]
        [RegularExpression(@"\d{2}-\d{3}", ErrorMessage = "Zła składnia kodu pocztowego")]
        public string ZipCode { get; set; }

        [Required(ErrorMessage = "Ulica jest wymagana")]
        [MaxLength(64, ErrorMessage = "Nazwa ulicy może zawierać do 64 znaków")]
        public string Street { get; set; }
    }
}
