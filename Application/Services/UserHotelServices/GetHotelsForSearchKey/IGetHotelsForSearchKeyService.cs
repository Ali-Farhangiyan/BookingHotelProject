using Application.ContextInterfaces;
using Domain.Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.UserHotelServices.GetHotelsForSearchKey
{
    public interface IGetHotelsForSearchKeyService
    {
        Task<List<SearchHotelNameDto>> ExecuteAsync(string searckKey);
    }

    public class GetHotelsForSearchKeyService : IGetHotelsForSearchKeyService
    {
        private readonly IDatabaseContext db;

        public GetHotelsForSearchKeyService(IDatabaseContext db)
        {
            this.db = db;
        }

        public async Task<List<SearchHotelNameDto>> ExecuteAsync(string searckKey)
        {
            if (string.IsNullOrEmpty(searckKey))
            {
                var cities = await db.Cities
                .Select(city => new SearchHotelNameDto
                {
                    Text = city.CityName,
                    Id = city.Id
                }).ToListAsync();

                return cities;
            }
            else
            {
                var cities = await db.Cities
                .Where(city => city.CityName.Contains(searckKey))
                .Select(city => new SearchHotelNameDto
                {
                    Text = city.CityName,
                    Id = city.Id
                }).ToListAsync();

                return cities;
            }
            
        }
    }

    public class SearchHotelNameDto
    {
        public int Id { get; set; }
        public string? Text { get; set; }  
    }
}
