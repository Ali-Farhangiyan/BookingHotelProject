using Application.ContextInterfaces;
using Application.Services.AdminHotelServices.AddFeatureToRooms;
using Application.Services.AdminHotelServices.AddHotel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.AdminHotelServices.ShowRoomsThatRelatedHotel
{
    public interface IShowRoomsThatRelatedHotelService
    {
        Task<List<RoomAdminDto>> ExecuteAsync(int HotelId);
    }

    public class ShowRoomsThatRelatedHotelService : IShowRoomsThatRelatedHotelService
    {
        private readonly IDatabaseContext db;

        public ShowRoomsThatRelatedHotelService(IDatabaseContext db)
        {
            this.db = db;
        }
        public async Task<List<RoomAdminDto>> ExecuteAsync(int HotelId)
        {
            var rooms = await db.Rooms
                .Include(r => r.Hotel)
                .Include(r => r.RoomFeatures)
                .Where(r => r.HotelId == HotelId)
                .Select(r => new RoomAdminDto
                {
                    Name = r.Name,
                    Capacity = r.Capacity,
                    PricePerNight = r.PricePerNight,
                    IsAbilityAdditionalPerson = r.IsAbilityAdditionalPerson,
                    PriceAdditionalPersonPerNight = r.PriceAdditionalPersonPerNight,
                    Id = r.Id,
                    HotelName = r.Hotel.Name,
                    RoomsFeature = r.RoomFeatures.Select(rf => new RoomFeatureDto
                    {
                        Icon = rf.Icon,
                        Name = rf.Name
                    }).ToList()
                }).ToListAsync();

            return rooms;
                

        }
    }

    public class RoomAdminDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public int Capacity { get; set; }

        public bool IsAbilityAdditionalPerson { get; set; } = false;

        public int PricePerNight { get; set; }

        public int PriceAdditionalPersonPerNight { get; set; }

        public string HotelName { get; set; } = null!;

        public ICollection<RoomFeatureDto>? RoomsFeature { get; set; }
    }
}
