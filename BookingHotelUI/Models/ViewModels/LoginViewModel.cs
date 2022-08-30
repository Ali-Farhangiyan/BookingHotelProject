using System.ComponentModel.DataAnnotations;

namespace BookingHotelUI.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string UserName { get; set; } = null!;
        [Required]
        [RegularExpression(@"^(?:0|98|\+98|\+980|0098|098|00980)?(9\d{9})$", ErrorMessage = "your number is not matched")]
        public string Password { get; set; } = null!;

        public bool IsRememberMe { get; set; } = false;

        public string? ReturnUrl { get; set; } 
    }
} 
