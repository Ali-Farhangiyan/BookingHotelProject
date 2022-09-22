using Application.Services.UserHotelServices.GetUserData;

namespace BookingHotelUI.Areas.Customer.Models
{
    public class CustomerViewModel
    {
        public GetUserDto UserData { get; set; } = null!;

        public List<GetBookingDataDto> BookingData { get; set; } = null!;
    }
}
