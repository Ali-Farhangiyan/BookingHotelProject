using Application.ContextInterfaces;
using Application.Services.UserHotelServices.GetUserData;
using Domain.Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.UserHotelServices.TrackOrder
{
    public interface ITrackOrderService
    {
        Task<GetBookingDataDto> TrackOrderAsync(int? bookingId);
    }

    public class TrackOrderService : ITrackOrderService
    {
        private readonly IDatabaseContext db;

        public TrackOrderService(IDatabaseContext db)
        {
            this.db = db;
        }

        public async Task<GetBookingDataDto> TrackOrderAsync(int? bookingId)
        {
            var booking = await db.Bookings
                .Include(booking => booking.Room)
                .ThenInclude(room => room.Hotel)
                .Where(booking => booking.BookingCode == bookingId)
                .OrderByDescending(booking => booking.Id)
                .Select(booking => new GetBookingDataDto
                {
                    RoomName = booking.Room.Name,
                    HotelName = booking.Room.Hotel.Name,
                    PriceOneNight = booking.PriceOneNight,
                    TotalPrice = (booking.PriceOneNight * booking.LengthOfStay),
                    BookingCode = booking.BookingCode,
                    StartDate = (DateTime)booking.StartDate,
                    EndDate = (DateTime)booking.EndDate,
                    LengthOfStay = booking.LengthOfStay,
                    PassengerFamilyName = booking.PassengerFamilyName,
                    PassengerName = booking.PassengerName,
                    BookingStatus = booking.BookingStatus.ToString()
                }).FirstOrDefaultAsync();

            return booking ?? null!;
        }
    }
}
