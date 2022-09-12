using Application.ContextInterfaces;
using Domain.Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.UserHotelServices.OrderForBookingHotel
{
    public interface IOrderForBookingHotelService
    {
        Task<int> ExecuteAsync(OrderDto order);
    }

    public class OrderForBookingHotelService : IOrderForBookingHotelService
    {
        private readonly IDatabaseContext db;

        public OrderForBookingHotelService(IDatabaseContext db)
        {
            this.db = db;
        }

        
        
        public async Task<int> ExecuteAsync(OrderDto order)
        {
            var bookingId =await GenerateBookingId();
            var booking = new Booking(order.RoomId, order.UserId,
                order.StartDate, order.EndDate, bookingId,
                order.PassengerName, order.PassengerFamilyName, order.Price);


            await db.Bookings.AddAsync(booking);
            var result = await db.SaveChangesAsync();
            if (result > 0) return booking.Id;
            return -1;
        }

        private async Task<int> GenerateBookingId()
        {
            var random = new Random();

            var rand = random.Next(123456789, 984979959);

            var bookingIds = await db.Bookings.Select(b => b.BookingCode).ToListAsync();

            if (bookingIds.Contains(rand))
            {
                rand = random.Next(123456789, 984979959);
                return rand;
            }
            else
            {
                return rand;
            }
        }
    }

    public class OrderDto
    {
        public int RoomId { get;  set; }
        public int Price { get;  set; }

        public DateTime? StartDate { get;  set; }

        public DateTime? EndDate { get;  set; }

        public string UserId { get;  set; } = null!;

        public string PassengerName { get;  set; } = null!;

        public string PassengerFamilyName { get;  set; } = null!;
    }
}
