using Application.ContextInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.UserHotelServices.ShowPopularHotels
{
    public interface IShowPopularHotelService
    {
        Task<List<PopularHotelDto>> ExecuteAsync();
    }

    public class ShowPopularHotelService : IShowPopularHotelService
    {
        private readonly IDatabaseContext db;

        public ShowPopularHotelService(IDatabaseContext db)
        {
            this.db = db;
        }


        public async Task<List<PopularHotelDto>> ExecuteAsync()
        {
            var hotels = await db.Hotels
                .Include(hotel => hotel.City)
                .Include(hotel => hotel.Images)
                .OrderByDescending(hotel => hotel.Rate)
                .Select(hotel => new PopularHotelDto
                {
                    City = hotel.City.CityName,
                    Name = hotel.Name,
                    NumberOfStar = hotel.NumberOfStar,
                    Image = hotel.Images.FirstOrDefault().Src,
                    Rate = hotel.Rate
                }).ToListAsync();

            return hotels ?? null!;
        }
    }

    public class PopularHotelDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public int NumberOfStar { get; set; }

        public double Rate { get; set; }

        public string Image { get; set; } = null!;

        public string City { get; set; } = null!;
    }
}
