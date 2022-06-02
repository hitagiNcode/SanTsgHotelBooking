using System.ComponentModel.DataAnnotations;

namespace SanTsgHotelBooking.Domain
{
    public class DumUser
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Kullanıcı adınız 50 karakterden uzun olamaz")]
        public string UserName { get; set; } = null!;
        [Required]
        [StringLength(50, ErrorMessage = "Sifreniz 50 karakterden uzun olamaz")]
        public string Password { get; set; } = null!;
        [Required]
        [StringLength(50, ErrorMessage = "Email adresiniz 50 karakterden uzun olamaz")]
        public string Email { get; set; } = null!;
        public bool isUserActive { get; set; } = true;
        // Soft delete bool
        public bool isUserDeleted { get; set; } = false;
        public DateTime RegisterDate { get; } = DateTime.Now;

    }
}
