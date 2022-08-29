namespace Domain.Entites
{
    public class Booking
    {
        public int Id { get; private set; }

        public int RoomId { get; private set; }
        public Room Room { get; private set; } = null!;

        public DateTime StartDate { get; private set; }

        public DateTime EndDate { get; private set; }

        public string UserId { get; private set; } = null!;

        public Booking()
        {
            // ef
        }

        public Booking( int roomId, DateTime startDate, DateTime enddate, string userId)
        {
            RoomId = roomId;
            StartDate = startDate;
            EndDate = enddate;
            UserId = userId;
        }
    }
}
