using Application.Services.UserHotelServices.SearchHotelsByNameAndDate;

namespace BookingHotelUI.Models.ViewModels
{
    public class HomeViewModel
    {
        public List<IGrouping<bool, SearchHotelsByNameAndDateDto>> SearchHotel { get; set; } = null!;
    }
}
