using Application.ContextInterfaces;
using Application.Services.AdminHotelServices.AddHotel;
using Application.Services.AdminHotelServices.ShowRoomsThatRelatedHotel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.AdminHotelServices.EditRoom
{
    public interface IEditRoomService
    {
        Task<bool> ExecuteAsync(RoomAdminDto room);

        Task<RoomAdminDto> ShowRoomById(int roomId);
    }

    public class EditRoomService : IEditRoomService
    {
        private readonly IDatabaseContext db;

        public EditRoomService(IDatabaseContext db)
        {
            this.db = db;
        }
        public async Task<bool> ExecuteAsync(RoomAdminDto room)
        {
            var updatedRoom = await db.Rooms.SingleOrDefaultAsync(r => r.Id == room.Id);
            if (updatedRoom is null) return false;
            updatedRoom.UpdateRoom(room.Name, room.Capacity, room.PricePerNight, room.PriceAdditionalPersonPerNight, updatedRoom.HotelId);

            var result = await db.SaveChangesAsync();
            if(result > 0) return true;
            return false;
        }

        public async Task<RoomAdminDto> ShowRoomById(int roomId)
        {
            var room = await db.Rooms
                .Include(r => r.RoomFeatures)
                .Where(r => r.Id == roomId)
                .Select(r => new RoomAdminDto
                {
                    Id = r.Id,
                    Name = r.Name,
                    Capacity = r.Capacity,
                    PricePerNight = r.PricePerNight,
                    PriceAdditionalPersonPerNight = r.PriceAdditionalPersonPerNight,
                    IsAbilityAdditionalPerson = r.IsAbilityAdditionalPerson
                    
                }).FirstOrDefaultAsync();

            return room ?? null!;
        }
    }
}
