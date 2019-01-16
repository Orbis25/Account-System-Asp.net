using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModels
{
    public class FilterDebsViewModel
    {
        [Display(Name = "var")]
        public int IdAccount { get; set; }
        public string From { get; set; }
        public string To { get; set; }
    }
}
