using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModels
{
    public class UpdateRequestViewModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public bool State { get; set; }
    }
}
