using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.ViewModels
{
    public class BorrowFormModel
    {
        public int BorrowId { get; set; }
        public string LibrarianUserId { get; set; }
        public string ReturnLibrarianUserId { get; set; }
        public string UserId { get; set; }
        public int BookId { get; set; }
        public DateTime BorrowTime { get; set; }
        public DateTime? ReturnTime { get; set; }
        public int Status { get; set; }
    }
}
