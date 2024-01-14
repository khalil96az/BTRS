using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BTRS.Models
{
    public class Passenger_User
    {
        [Key]
        public int ID { set; get; }

        [ForeignKey("PassengerID")]
        public Passenger passenger { set; get; }

        [ForeignKey("BusTripID")]
        public BusTrip busTrip { set; get; }
    }
}
