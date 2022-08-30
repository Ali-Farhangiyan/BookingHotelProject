using System.ComponentModel.DataAnnotations;

namespace BookingHotelUI.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public string FirstName { get; set; } = null!;
        [Required]
        public string LastName { get; set; } = null!;
        [Required]
        [MaxLength(10, ErrorMessage ="your natinal code must 10 charcters")]
        [MinLength(10, ErrorMessage = "your natinal code must 10 charcters")]
        public string NationalCode { get; set; } = null!;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
        [RegularExpression(@"^(?:0|98|\+98|\+980|0098|098|00980)?(9\d{9})$", ErrorMessage = "your number is not matched")]
        public string PhoneNumber { get; set; } = null!;
    } 
} 
