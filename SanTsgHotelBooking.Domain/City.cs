using System.ComponentModel.DataAnnotations;

namespace SanTsgHotelBooking.Domain
{
    public class City
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Şehir adı 50 karakterden uzun olamaz")]
        public string CityName { get; set; } = null!;

        [Required]
        [StringLength(2, ErrorMessage = "Ulke kodu 2 karakterden uzun olamaz")]
        public string CountryCode { get; set; } = null!;
    }
}
