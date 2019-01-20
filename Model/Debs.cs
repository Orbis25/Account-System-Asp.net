using Model.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    public class Debs
    {
        public int Id { get; set; }
        [Required]
        [Range(0, 9999999999999999.99)]
        public decimal Money { get; set; }
        public string Description { get; set; }
        public DateTime DateTime { get; set; }
        [Required]
        public int AccountId { get; set; }
        [ForeignKey("AccountId")]
        public Account Account { get; set; }
        public Deleted Deleted { get; set; } 
        public IEnumerable<Payment> Payments { get; set; }
    }
}