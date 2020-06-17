using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.ViewModels
{
    public class BorrowListItemModel
    {
        public int BorrowId { get; set; }
        public string Title { get; set; }
        public string AuthorFullName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
