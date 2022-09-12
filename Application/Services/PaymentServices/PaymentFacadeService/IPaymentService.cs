using Application.ContextInterfaces;
using Domain.Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.PaymentServices.PaymentFacadeService
{
    public interface IPaymentService
    {
        Task<PaymentOfBookingDto> PayForBooking(int bookingId);

        Task<PaymentDto> GetPaymentAsync(Guid id);

        Task<bool> VerifyPaymentAsync(Guid Id, string Authority, long RefId);
    }

    public class PaymentService : IPaymentService
    {
        private readonly IIdentityDatabaseContext identityDb;
        private readonly IDatabaseContext db;

        public PaymentService(IIdentityDatabaseContext identityDb, IDatabaseContext db)
        {
            this.identityDb = identityDb;
            this.db = db;
        }

        public async Task<PaymentDto> GetPaymentAsync(Guid id)
        {
            var payment = await db.Payments
                .Include(p => p.Booking)
                .SingleOrDefaultAsync(p => p.Id == id);

            var user = await identityDb.Users
                .SingleOrDefaultAsync(u => u.Id == payment.Booking.UserId);

            var description = "hello ali";
            return new PaymentDto
            {
                Amount = payment.Booking.GetTotalPrice(),
                Email = user.Email,
                UserId = user.Id,
                PhoneNumber = user.PhoneNumber,
                Id = payment.Id,
                Description = description

            };
        }

        public async Task<PaymentOfBookingDto> PayForBooking(int bookingId)
        {
            var booking = await db.Bookings
                .SingleOrDefaultAsync(b => b.Id == bookingId);

            if (booking is null)
            {
                return null!;
            }

            var payment = await db.Payments
                .SingleOrDefaultAsync(p => p.BookingId == booking.Id);

            if (payment is null)
            {
                payment = new Domain.Entites.Payment(booking.GetTotalPrice(), booking.Id);
                await db.Payments.AddAsync(payment);
                await db.SaveChangesAsync();
            }

            return new PaymentOfBookingDto
            {
                Amount = payment.Amount,
                PaymentId = payment.Id
            };
        }

        public async Task<bool> VerifyPaymentAsync(Guid Id, string Authority, long RefId)
        {
            var payment = await db.Payments
                .Include(p => p.Booking)
                .ThenInclude(b => b.Room)
                .ThenInclude(r => r.RoomBookingDates)
                .SingleOrDefaultAsync(p => p.Id == Id);

            if(payment is null) { return false; }
            payment.Booking.ChangeStautsToPaid();
            var roomId = payment.Booking.GetRoomId();
            var startDate = payment.Booking.GetStartDate();
            var endDate = payment.Booking.GetEndDate();
            var bookingdate = new RoomBookingDate(roomId, startDate, endDate);
            await db.RoomBookingDate.AddAsync(bookingdate);

            payment.PaymentIsDone(Authority, RefId);
            var result = await db.SaveChangesAsync();
            if (result > 0) return true;
            return false;
        }
    }

    public class PaymentOfBookingDto
    {
        public Guid PaymentId { get; set;}

        public int Amount { get; set; }
    }

    public class PaymentDto
    {
        public Guid Id { get; set; }

        public string PhoneNumber { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string Email { get; set; } = null!;

        public int Amount { get; set; }
        public string UserId { get; set; } = null!;
    }
}
