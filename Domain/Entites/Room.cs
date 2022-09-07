namespace Domain.Entites
{
    public class Room
    {
        public int Id { get; private set; }

        public string Name { get; private set; } = null!;

        public int Capacity { get; private set; }

        public bool IsAbilityAdditionalPerson { get; private set; } = false;

        public int PricePerNight { get; private set; }

        public int PriceAdditionalPersonPerNight { get; private set; }

        public int HotelId { get; private set; }

        public Hotel Hotel { get; private set; } = null!;

        private readonly List<RoomFeature> features = new List<RoomFeature>();
        public ICollection<RoomFeature> RoomFeatures => features;

        private readonly List<Booking> bookings = new List<Booking>();
        public ICollection<Booking> Bookings => bookings.AsReadOnly();

        private readonly List<RoomBookingDate> roomBookingDates = new List<RoomBookingDate>();
        public ICollection<RoomBookingDate> RoomBookingDates => roomBookingDates.AsReadOnly();



        public Room()
        {
            // ef 
        }

        public Room(string name,int capacity, int pricePerNight, int priceAdditionalPersonPerNight, int hotelId)
        {
            Name = name;
            Capacity = capacity;
            PricePerNight = pricePerNight;
            PriceAdditionalPersonPerNight = priceAdditionalPersonPerNight;
            HotelId = hotelId;
        }

        public void AddFeature(RoomFeature feature)
        {
            features.Add(feature);
        }

        public void AddFeature(ICollection<RoomFeature> listOfFeatures)
        {
            features.AddRange(listOfFeatures);
        }

        public void AddBooking(Booking booking)
        {
            bookings.Add(booking);
        }

        public void AddRoomBookingDate(RoomBookingDate roomBookingDate)
        {
            roomBookingDates.Add(roomBookingDate);
        }

        public void UpdateRoom(string name, int capacity, int pricePerNight, int priceAdditionalPersonPerNight, int hotelId)
        {
            Name = name;
            Capacity = capacity;
            PricePerNight = pricePerNight;
            PriceAdditionalPersonPerNight = priceAdditionalPersonPerNight;
            HotelId = hotelId;
        }


    }
}
