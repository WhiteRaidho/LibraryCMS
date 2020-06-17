using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.ViewModels
{
    public class AvalibleBookListItemViewModel
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string AuthorFullName { get; set; }
    }
}
