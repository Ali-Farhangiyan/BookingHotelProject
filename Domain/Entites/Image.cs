namespace Domain.Entites
{
    public class Image
    {
        public int Id { get; private set; }

        public string Src { get; private set; } = null!;

        public int HotelId { get; private set; }

        public Hotel Hotel { get; private set; } = null!;

        public Image()
        {
            // ef
        }

        public Image(string src, int hotelId)
        {
            HotelId = hotelId;
            Src = src;
        }
    }
}
