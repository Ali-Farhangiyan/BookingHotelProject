using Application.ContextInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.UserHotelServices.SearchHotelsByNameAndDate
{
    public interface ISearchHotelsByNameAndDateService
    {
        Task<List<IGrouping<bool, SearchHotelsByNameAndDateDto>>> ExecuteAsync(int cityId, DateTime startDate, DateTime endDate);
    }

    public class SearchHotelsByNameAndDateService : ISearchHotelsByNameAndDateService
    {
        private readonly IDatabaseContext db;

        public SearchHotelsByNameAndDateService(IDatabaseContext db)
        {
            this.db = db;
        }

        public async Task<List<IGrouping<bool,SearchHotelsByNameAndDateDto>>> ExecuteAsync(int cityId,
            DateTime startDate, DateTime endDate)
        {
            var stay = (int)(endDate - startDate).TotalDays;
            //var city = await db.Hotels.FirstOrDefaultAsync(h => h.Id == hotelId);



            var hotelsCity = await db.Hotels
                .Include(h => h.City)
                .Include(h => h.Rooms)
                .ThenInclude(r => r.Bookings)
                .Include(h => h.Images)
                .Where(h => h.CityId == cityId).ToListAsync();

            //var hotelsCity = await db.Cities
            //    .Include(c => c.Hotels)
            //    .Where(c => c.Id == cityId).ToListAsync();



            var date = await db.RoomBookingDate
                .Include(d => d.Room)
                .Where(c => c.StartDate == null || c.StartDate >= endDate )
                .Where(c => c.EndDate == null || c.EndDate <= startDate)
                .Select(c => c.RoomId)
                .ToListAsync();


            var hotels = hotelsCity.GroupBy(hotel => hotel.Rooms.Any(room => date.Contains(room.Id)), h => new SearchHotelsByNameAndDateDto
            {
                Id = h.Id,
                Name = h.Name,
                Address = h.Address,
                Star = h.NumberOfStar,
                Image = h.Images.FirstOrDefault().Src,
                StartPrice = h.Rooms.Min(r => r.PricePerNight),
                StayOfLength = stay
            }).ToList();

            //var hotelss = hotelsCity
            //    .ToLookup(c => c.Rooms.Any(r => date.Contains(r.Id)))
            //    .SelectMany(gh => gh.Select(h => new SearchHotelsByNameAndDateDto
            //    {
            //        Id = h.Id,
            //        Name = h.Name,
            //        Address = h.Address,
            //        Star = h.NumberOfStar,
            //        Image = h.Images.FirstOrDefault().Src,
            //        StartPrice = h.Rooms.Min(r => r.PricePerNight),
            //        StayOfLength = stay
            //    }));
               

            //var hotels = await db.Hotels
            //    .Include(h => h.Rooms)
            //    .ThenInclude(r => r.Bookings)
            //    .Include(h => h.Images)
            //    .Where(c => c.City == city && c.Rooms.Any(r => date.Contains(r.Id)))
            //    .Select(h => new SearchHotelsByNameAndDateDto
            //    {
            //        Id = h.Id,
            //        Name = h.Name,
            //        Address = h.Address,
            //        Star = h.NumberOfStar,
            //        Image = h.Images.FirstOrDefault().Src,
            //        StartPrice = h.Rooms.Min(r => r.PricePerNight),
            //        StayOfLength = stay
            //    }).ToListAsync();

            return hotels;
        }
    }

    public class SearchHotelsByNameAndDateDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Image { get; set; } = null!;

        public int Star { get; set; }

        public string Address { get; set; } = null!;

        public int StayOfLength { get; set; }

        public int StartPrice { get; set; }

        public int Rate { get; set; }


    }
}
