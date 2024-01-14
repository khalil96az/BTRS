using System.ComponentModel.DataAnnotations;

namespace BTRS.Models
{
    public class Passenger
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Gender { get; set; }

        public ICollection<Passenger_User> passenger_User { get; set; }
    }
}

