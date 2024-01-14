using System.ComponentModel.DataAnnotations;

namespace BTRS.Models
{
    public class Admin
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }


        public string FullName { get; set; }


        public ICollection<BusTrip> busTrips { get; set; }
    }
}
