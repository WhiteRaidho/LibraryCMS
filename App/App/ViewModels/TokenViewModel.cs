using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.ViewModels
{
    public class TokenViewModel
    {
        public string Token { get; set; }
        public string Refresh { get; set; }
        public DateTime Expires { get; set; }
    }
}
