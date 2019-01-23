using Model.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Payment
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        [Required]
        [Range(0, 9999999999999999.99)]
        public decimal Quantity { get; set; }
        [Required]
        public int DebId { get; set; }
        [ForeignKey("DebId")]
        public Debs Deb { get; set; }
        public Deleted Deleted { get; set; }
    }
}
