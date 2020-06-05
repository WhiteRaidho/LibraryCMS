using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace App.ViewModels
{
    public class ReviewFormModel
    {
        [Required(ErrorMessage = "Id jest wymagane")]
        public int Id { get; set; }

        [Range(1, 5)]
        public int Rate { get; set; }

        [MaxLength(1024, ErrorMessage = "Maksymalna ilość znaków 1024")]
        public string Description { get; set; }
    }
}
