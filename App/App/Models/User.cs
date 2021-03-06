﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace App.Models
{
    [Table("Users")]
    public class User
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string UserID { get; set; } = Guid.NewGuid().ToString();
        
        [Required, MinLength(6, ErrorMessage = "User name too short"), MaxLength(32, ErrorMessage = "User name too long")]
        public string UserName { get; set; }
        [Required, MinLength(6, ErrorMessage = "Password too short"), MaxLength(32, ErrorMessage = "Password too long")]
        public string UserPassword { get; set; }
        
        [Required, RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Email is not valid.")]
        public string Email { get; set; }

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        //[DataType(DataType.Text)]
        //public DateTime BirthDate { get; set; }
        
        public bool IsAdmin { get; set; }

        public string Token { get; set; }
        public string RefreshToken { get; set; }

        public ICollection<Borrow> Borrows { get; set; }

        //HACK for librarian only, books that librarian borrow to someone and one that he get returned
        public ICollection<Borrow> BorrowedBooks { get; set; }
        public ICollection<Borrow> ReturnedBooks { get; set; }

        public ICollection<Review> Reviews { get; set; }
    }
}
