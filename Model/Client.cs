using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    public class Client
    {
        public int Id { get; set; }
        [Required]
        [StringLength(60)]
        public string Name { get; set; }
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 10)]
        public string PhoneNumber { get; set; }
        public bool ProfileUpdated { get; set; }
        [Required]
        [StringLength(12)]
        public string Dni { get; set; }
        [Required]
        [StringLength(100)]
        public string Address { get; set; }
        public string Avatar { get; set; }
        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        public ApplicationUser ApplicationUser { get; set; }
    }
}