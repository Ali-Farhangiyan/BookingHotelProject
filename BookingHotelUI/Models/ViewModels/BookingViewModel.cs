namespace BookingHotelUI.Models.ViewModels
{
    public class BookingViewModel
    {
        public int HotelId { get; set; }
        public int RoomId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int LengthOfStay { get; set; }

        public int Price { get; set; }

        public string Image { get; set; } = null!;

        public string HotelName { get; set; } = null!;
        public string RoomName { get; set; } = null!;
    }
}
