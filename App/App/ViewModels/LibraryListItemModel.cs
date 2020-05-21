using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.ViewModels
{
    public class LibraryListItemModel
    {
        //id, nazwa, i lokalizacja
        public int LibraryID { get; set; }
        public string Name { get; set; }
        public string LocationName { get; set; }
        public string LocationStreet { get; set; }
    }
}
