using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entites
{
    public class RoomBookingDate
    {
        public int Id { get; set; }

        public Room Room { get; set; } = null!;

        public int RoomId { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public RoomBookingDate(int roomId, DateTime? startDate, DateTime? enddate)
        {
            RoomId = roomId;
            StartDate = startDate;
            EndDate = enddate;
        }

        public void CreateRoomBookingDate(int roomId, DateTime startDate, DateTime enddate)
        {
            new RoomBookingDate(roomId, startDate, enddate);
        }

        public RoomBookingDate()
        {

        }
    }
}
