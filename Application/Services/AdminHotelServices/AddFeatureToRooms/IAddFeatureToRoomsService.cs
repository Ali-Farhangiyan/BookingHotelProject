using Application.ContextInterfaces;
using Domain.Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.AdminHotelServices.AddFeatureToRooms
{
    public interface IAddFeatureToRoomsService
    {
        Task<bool> ExecuteAsync(RoomsFeatureDto roomFeature);

    }

    public class AddFeatureToRoomsService : IAddFeatureToRoomsService
    {
        private readonly IDatabaseContext db;

        public AddFeatureToRoomsService(IDatabaseContext db)
        {
            this.db = db;
        }


        public async Task<bool> ExecuteAsync(RoomsFeatureDto roomFeature)
        {
            var room = await db.Rooms
                .SingleOrDefaultAsync(r => r.Id == roomFeature.RoomId);

            if (room is null) return false;

            foreach (var item in roomFeature.RoomFeatures)
            {
                var roomf = new RoomFeature(item.Name, item.Icon, room.Id);
                room.AddFeature(roomf);
            }

            var result = await db.SaveChangesAsync();
            if (result > 0) return true;
            return false;


        }
    }

    public class RoomsFeatureDto
    {
        public int RoomId { get; set; }
        public ICollection<RoomFeatureDto> RoomFeatures { get; set; } = null!;
    }

    public class RoomFeatureDto
    {
        
        public string Name { get; set; } = null!;

        public string Icon { get; set; } = null!;
    }
    
}
