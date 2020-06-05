using System.ComponentModel.DataAnnotations;

namespace App.ViewModels
{
    public class LocationFormModel
    {
        [Required(ErrorMessage = "Id jest wymagane")]
        public int LocationId { get; set; }

        [Required]
        [MaxLength(32, ErrorMessage = "Nazwa może zawierać do 32 znaków")]
        public string Name { get; set; }

        [RegularExpression(@"\d{2}-\d{3}", ErrorMessage = "Zła składnia kodu pocztowego")]
        public string ZipCode { get; set; }

        [MaxLength(64, ErrorMessage = "NAzwa ulicy może zawierać do 64 znaków")]
        public string Street { get; set; }
    }
}
