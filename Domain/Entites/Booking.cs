namespace Domain.Entites
{
    public class Booking
    {
        public int Id { get; private set; }

        public int RoomId { get; private set; }
        public int PriceOneNight { get; private set; }

        public int? BookingCode { get; private set; } = null;

        public int LengthOfStay { get; private set; }
        public Room Room { get; private set; } = null!;

        public DateTime? StartDate { get; private set; }

        public DateTime? EndDate { get; private set; } 

        public string UserId { get; private set; } = null!;

        public string PassengerName { get; private set; } = null!;

        public string PassengerFamilyName { get; private set; } = null!;

        public BookingStatus BookingStatus { get; private set; } = BookingStatus.UnPaid;

        public Booking()
        {
            // ef
        }

        public Booking( int roomId, string userId,
            DateTime? startDate,
            DateTime? enddate, 
            int bookingCode,
            string passengerName, string passengerFullName, int priceOneNight)
        {
            RoomId = roomId;
            StartDate = startDate;
            EndDate = enddate;
            UserId = userId;
            BookingCode = bookingCode;
            PassengerName = passengerName;
            PassengerFamilyName = passengerFullName;
            PriceOneNight = priceOneNight;
            SetLengthOfStay();
        }

        private void SetLengthOfStay()
        {
            if(EndDate is not null && StartDate is not null)
            {
                var length = EndDate - StartDate;
                LengthOfStay = (int) length.Value.TotalDays;
            }
            
        }

        public void SetBookingCode(int? bookingCode)
        {
            BookingCode = bookingCode;
        }

        public void ChangeStautsToPaid()
        {
            BookingStatus = BookingStatus.Paid;
        }

        public int GetTotalPrice()
        {
            return LengthOfStay * PriceOneNight;
        }

        public int GetRoomId()
        {
            return RoomId;
        }

        public DateTime? GetStartDate()
        {
            return StartDate;
        }

        public DateTime? GetEndDate()
        {
            return EndDate;
        }


    }

    public enum BookingStatus
    {
        UnPaid = 1,
        Paid = 2
    }
}
