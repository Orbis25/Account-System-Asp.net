using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModels
{
    public class UpdateUserNameViewModelAppUs
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string UserName { get; set; }
    }
}
