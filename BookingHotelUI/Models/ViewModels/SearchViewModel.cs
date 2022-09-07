namespace BookingHotelUI.Models.ViewModels
{
    public class SearchViewModel
    {
        public string City { get; set; } = null!;

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}
