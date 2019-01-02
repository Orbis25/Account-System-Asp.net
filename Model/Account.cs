using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Account
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Range(0, 9999999999999999.99)]
        public decimal Total { get; set; }
        [Required]
        public int ClientId { get; set; }
        [ForeignKey("ClientId")]
        public Client Client { get; set; }
        [Required]
        public int RequestId { get; set; }
        public Request Request { get; set; }
        public List<Debs> Debs { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        [Required]
        public bool State { get; set; }
        

    }
}
