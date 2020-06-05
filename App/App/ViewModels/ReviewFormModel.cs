﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace App.ViewModels
{
    public class ReviewFormModel
    {
        public int Id { get; set; }
        [Range(1, 5)]
        public int Rate { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }
        public int BookId { get; set; }
    }
}
