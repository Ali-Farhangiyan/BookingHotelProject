using Application.ContextInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.UserHotelServices.ShowHotelWithRelatedDate
{
    public interface IShowHotelWithRelatedDateService
    {
        Task<ShowHotelWithRelatedDateDto> ExecuteAsync(int hotelId, DateTime startDate, DateTime endDate);
    }

    public class ShowHotelWithRelatedDateService : IShowHotelWithRelatedDateService
    {
        private readonly IDatabaseContext db;

        public ShowHotelWithRelatedDateService(IDatabaseContext db)
        {
            this.db = db;
        }

        public async Task<ShowHotelWithRelatedDateDto> ExecuteAsync(int hotelId, DateTime startDate, DateTime endDate)
        {

            var stay = (int)(endDate - startDate).TotalDays;

            

            var date = await db.RoomBookingDate
                .Include(d => d.Room)
                .Where(c => c.StartDate == null || c.StartDate >= endDate)
                .Where(c => c.EndDate == null || c.EndDate <= startDate)
                .Select(c => c.RoomId)
                .ToListAsync();

            var totalRooms =  db.Rooms
                .Include(r => r.RoomFeatures)
                .Where(r => r.HotelId == hotelId)
                .ToLookup(room => date.Contains(room.Id), r => new ShowRoomsDto
                {
                    Id = r.Id,
                    LengthOfStay = stay,
                    Name = r.Name,
                    PriceForOneNight = r.PricePerNight,
                    RoomFeatures = r.RoomFeatures.Select(rf => new ShowHotelsFeatureDto
                    {
                        Name = rf.Name,
                        Icon = rf.Icon,
                    }).ToList(),
                }).ToList();

            var hotel = await db.Hotels
                .Include(h => h.Rooms.Where(r => r.HotelId == hotelId))
                .ThenInclude(r => r.RoomFeatures)
                .Include(h => h.Images)
                .Include(h => h.City)
                .Select(hotel => new ShowHotelWithRelatedDateDto
                {
                    Id = hotel.Id,
                    HotelName = hotel.Name,
                    Description = hotel.Description,
                    Images = hotel.Images.Select(i => new ImageDto
                    {
                        Src = i.Src
                    }).ToList(),
                    HotelsFeatures = hotel.HotelFeatures.Select(hf => new ShowHotelsFeatureDto
                    {
                        Icon = hf.Icon,
                        Name = hf.Name
                    }).ToList(),
                    Rooms = totalRooms
                })
                .FirstOrDefaultAsync(h => h.Id == hotelId);

            return hotel;

        }
    }

    public class ShowHotelWithRelatedDateDto
    {
        public int Id { get; set; }

        public string HotelName { get; set; }

        public string Description { get; set; }

        public List<ImageDto> Images { get; set; }

        public List<ShowHotelsFeatureDto> HotelsFeatures { get; set; }
        public List<IGrouping<bool, ShowRoomsDto>> Rooms { get; set; }

        
    }

    public class ImageDto
    {
        public string Src { get; set; }
    }

    public class ShowHotelsFeatureDto
    {
        public string Name { get; set; }

        public string Icon { get; set; }


    }

    public class ShowRoomsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int LengthOfStay { get; set; }

        public int PriceForOneNight { get; set; }

        public List<ShowHotelsFeatureDto> RoomFeatures { get; set; }


    }
}
