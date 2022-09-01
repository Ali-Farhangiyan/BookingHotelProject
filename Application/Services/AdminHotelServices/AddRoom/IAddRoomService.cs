using Application.ContextInterfaces;
using Application.Services.AdminHotelServices.AddHotel;
using Domain.Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.AdminHotelServices.AddRoom
{
    public interface IAddRoomService
    {
        Task<bool> ExecuteAsync(AddRoomToHotelDto hotelRoom);
    }
    public class AddRoomService : IAddRoomService
    {
        private readonly IDatabaseContext db;

        public AddRoomService(IDatabaseContext db)
        {
            this.db = db;
        }


        public async Task<bool> ExecuteAsync(AddRoomToHotelDto hotelRoom)
        {
            var hotel = await db.Hotels.SingleOrDefaultAsync(h => h.Id == hotelRoom.HotelId);
            if (hotel is null) return false;

            foreach (var room in hotelRoom.Rooms)
            {
                var newRoom = new Room(room.Name, room.Capacity, room.PricePerNight, room.PriceAdditionalPersonPerNight, hotel.Id);
                hotel.AddRoom(newRoom);
            }

            var result = await db.SaveChangesAsync();
            if (result > 0) return true;
            return false; 

        }
    }

    public class AddRoomToHotelDto
    {
        public int HotelId { get; set; }
        public ICollection<AddRoomDto> Rooms { get; set; } = null!;
    }

    public class AddRoomDto
    {
        public int HotelId { get; set; }
        public string Name { get; set; } = null!;

        public int Capacity { get; set; }

        public bool IsAbilityAdditionalPerson { get; set; } = false;

        public int PricePerNight { get; set; }

        public int PriceAdditionalPersonPerNight { get; set; }
    }
}
