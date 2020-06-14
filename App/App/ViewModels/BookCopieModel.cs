using App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.ViewModels
{
    public class BookCopieModel
    {
        public int BookId { get; set; }
        public LibraryListItemModel Library { get; set; }
    }
}
