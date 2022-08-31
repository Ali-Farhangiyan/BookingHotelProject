using Application.ContextInterfaces;
using Domain.Entites;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.AdminHotelServices.AddHotel
{
    public interface IAddHotelService
    {
        Task<bool> ExecuteAsync(AddHotelDto hotel);
    }

    public class AddHotelService : IAddHotelService
    {
        private readonly IDatabaseContext db;

        public AddHotelService(IDatabaseContext db)
        {
            this.db = db;
        }


        public async Task<bool> ExecuteAsync(AddHotelDto hotel)
        {
            var newHotel = new Hotel(hotel.Name, hotel.Description, hotel.NumberOfStar, hotel.City, hotel.Address);

            foreach (var room in hotel.Rooms)
            {
                var newRoom = new Room(room.Name, room.Capacity, room.PricePerNight, room.PriceAdditionalPersonPerNight, newHotel.Id);
                newHotel.AddRoom(newRoom);
            }

            foreach (var image in hotel.Images)
            {
                var newImage = new Image(image.Src, newHotel.Id);
                newHotel.AddImage(newImage);
            }

            foreach (var feature in hotel.HotelFeatures)
            {
                var newFeature = new HotelFeature(feature.Name, feature.Icon, newHotel.Id);
                newHotel.AddHotelFeatures(newFeature);
            }

            await db.Hotels.AddAsync(newHotel);
            var result = await db.SaveChangesAsync();
            if(result > 0) return true;
            return false;

        }

        


    }
    public class AddHotelDto
    {
        [Required]
        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public int NumberOfStar { get; set; }

        public string City { get; set; } = null!;

        public string Address { get; set; } = null!;

        public List<RoomDto> Rooms { get; set; } = null!;
        public List<ImageDto> Images { get; set; } = null!;

        public List<HotelFeatureDto> HotelFeatures { get; set; } = null!;
    }

    public class RoomDto
    {
        public string Name { get; set; } = null!;

        public int Capacity { get; set; }

        public bool IsAbilityAdditionalPerson { get; set; } = false;

        public int PricePerNight { get; set; }

        public int PriceAdditionalPersonPerNight { get; set; }
    }



    public class ImageDto
    {
        public string Src { get; set; } = null!;
    }

    public class HotelFeatureDto
    {
        public string Name { get; set; } = null!;
        public string Icon { get; set; } = null!;
    }
}
