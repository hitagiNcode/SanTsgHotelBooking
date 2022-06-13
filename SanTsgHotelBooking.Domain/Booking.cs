using System.ComponentModel.DataAnnotations;

namespace SanTsgHotelBooking.Domain
{
    public class Booking
    {
        [Key]
        public int Id { get; set; }

        public string ReservationNumber { get; set; } = null!;

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public double Price { get; set; }
    }
}
