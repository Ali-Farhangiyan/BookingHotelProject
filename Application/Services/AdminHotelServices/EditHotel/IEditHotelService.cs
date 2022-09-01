using Application.ContextInterfaces;
using Domain.Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.AdminHotelServices.EditHotel
{
    public interface IEditHotelService
    {
        Task<bool> ExecuteAsync(EditHotelDto editHotel);

        Task<EditHotelDto> ShowHotelById(int Id);
    }

    public class EditHotelService : IEditHotelService
    {
        private readonly IDatabaseContext db;

        public EditHotelService(IDatabaseContext db)
        {
            this.db = db;
        }


        public async Task<bool> ExecuteAsync(EditHotelDto editHotel)
        {
            

            var updateHotel = await db.Hotels.SingleOrDefaultAsync(h => h.Id == editHotel.Id);

            updateHotel.UpdateHotel(editHotel.Name, editHotel.Description, editHotel.NumberOfStar, editHotel.City, editHotel.Address);
            

            db.Hotels.Update(updateHotel);
            var result = await db.SaveChangesAsync();
            if (result > 0) return true;
            return false;
        }

        public async Task<EditHotelDto> ShowHotelById(int Id)
        {
            var hotel = await db.Hotels.Select(h => new EditHotelDto
            {
                Id = h.Id,
                Name = h.Name,
                Description = h.Description,
                NumberOfStar = h.NumberOfStar,
                City = h.City,
                Address = h.Address
            }).FirstOrDefaultAsync();

            return hotel ?? null!;
        }
    }

    public class EditHotelDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public int NumberOfStar { get; set; }

        public string City { get; set; } = null!;

        public string Address { get; set; } = null!;
    }
}
