using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Request
    {
        public int Id { get; set; }
        public bool State { get; set; }
        public DateTime CreatedAt { get; set; }
        public string ApplicationUserId{ get; set; }
        [ForeignKey("ApplicationUserId")]
        public ApplicationUser ApplicationUser { get; set; }
    }
}
