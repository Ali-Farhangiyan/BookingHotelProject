using Application.Services.UserHotelServices.SearchHotelsByNameAndDate;
using Application.Services.UserHotelServices.ShowPopularHotels;

namespace BookingHotelUI.Models.ViewModels
{
    public class HomeViewModel
    {
        public List<IGrouping<bool, SearchHotelsByNameAndDateDto>>? SearchHotel { get; set; } 

        public List<PopularHotelDto>? PopularHotels { get; set; }

        public SearchViewModel? Search { get; }
    }
}
