using Application.ContextInterfaces;
using Domain.Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.UserHotelServices.GetUserData
{
    public interface IGetUserDataService
    {
        Task<GetUserDto> ExecuteAsync(string userId);

        Task<List<GetBookingDataDto>> GetBookingData(string userId);

        Task<GetBookingDataDto> ShowResultAfterPay(string userId);
    }

    public class GetUserDataService : IGetUserDataService
    {
        private readonly IIdentityDatabaseContext identityDb;
        private readonly IDatabaseContext db;

        public GetUserDataService(IIdentityDatabaseContext identityDb, IDatabaseContext db)
        {
            this.identityDb = identityDb;
            this.db = db;
        }
        public async Task<GetUserDto> ExecuteAsync(string userId)
        {
            var user = await identityDb.Users
                .Where(u => u.Id == userId)
                .Select(u => new GetUserDto
                {
                    Email = u.Email,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    FullName = u.FirstName + u.LastName,
                    NatinalCode = u.NationalCode,
                    PhoneNumber = u.PhoneNumber
                }).FirstOrDefaultAsync();

            return user;
        }

        public async Task<List<GetBookingDataDto>> GetBookingData(string userId)
        {
            var booking = await db.Bookings
                .Include(booking => booking.Room)
                .ThenInclude(room => room.Hotel)
                .Where(booking => booking.UserId == userId)
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
                }).ToListAsync();

            return booking ?? null!;
        }

        public async Task<GetBookingDataDto> ShowResultAfterPay(string userId)
        {
            var booking = await db.Bookings
                .Include(booking => booking.Room)
                .ThenInclude(room => room.Hotel)
                .Where(booking => booking.UserId == userId)
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

    public class GetUserDto
    {
        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string FullName { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string NatinalCode { get; set; } = null!;
    }

    public class GetBookingDataDto
    {
        public string RoomName { get;  set; } = null!;

        public string HotelName { get; set; } = null!;
        public int PriceOneNight { get;  set; }
        public int TotalPrice { get;  set; }

        public int? BookingCode { get;  set; } = null;

        public int LengthOfStay { get;  set; }

        public DateTime StartDate { get;  set; }

        public DateTime EndDate { get;  set; }

        public string PassengerName { get;  set; } = null!;

        public string PassengerFamilyName { get;  set; } = null!;

        public string BookingStatus { get; set; } = null!;

    }
}
