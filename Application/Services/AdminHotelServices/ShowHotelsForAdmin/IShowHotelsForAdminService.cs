using Application.ContextInterfaces;
using Application.Pagination;
using Domain.Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Cache;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.AdminHotelServices.ShowHotelsForAdmin
{
    public interface IShowHotelsForAdminService
    {
        Task<PaginatedList<ShowHotelAdminDto>> ExecuteAsync(RequestAdminHotelDto request);
    }

    public class ShowHotelsForAdminService : IShowHotelsForAdminService
    {
        private readonly IDatabaseContext db;

        public ShowHotelsForAdminService(IDatabaseContext db)
        {
            this.db = db;
        }


        public async Task<PaginatedList<ShowHotelAdminDto>> ExecuteAsync(RequestAdminHotelDto request)
        {
            var query = db.Hotels
                .Include(h => h.Rooms)
                .ThenInclude(r => r.RoomFeatures)
                .Include(h => h.HotelFeatures)
                .Include(h => h.Images)
                .AsQueryable();

            query = ApplyRequest(request, query);

            var hotels = await query.Select(h => new ShowHotelAdminDto
            {
                Id = h.Id,
                City = h.City,
                Name = h.Name,
                NumberOfRooms = h.Rooms.Count,
                NumberOfStars = h.NumberOfStar
            }).ToListAsync();

            return PaginatedList<ShowHotelAdminDto>.Create(hotels, request.PageSize, request.PageIndex);

        }

        private static IQueryable<Hotel> ApplyRequest(RequestAdminHotelDto request, IQueryable<Hotel> query)
        {
            if (request.AdminSortingHotel == AdminSortingHotel.Newest)
            {
                query = query.OrderByDescending(h => h.Id);
            }

            if (request.AdminSortingHotel == AdminSortingHotel.Oldest)
            {
                query = query.OrderBy(h => h.Id);
            }

            if (request.AdminSortingHotel == AdminSortingHotel.MostStar)
            {
                query = query.OrderByDescending(h => h.NumberOfStar).ThenBy(h => h.Name);
            }

            if (request.AdminSortingHotel == AdminSortingHotel.MinStar)
            {
                query = query.OrderBy(h => h.NumberOfStar).ThenBy(h => h.Name);
            }

            if (!string.IsNullOrEmpty(request.SearchKey))
            {
                query = query.Where(h => h.Name.Contains(request.SearchKey) || h.City.Contains(request.SearchKey));
            }

            return query;
        }

        
    }

    public class ShowHotelAdminDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public string City { get; set; } = null!;

        public int NumberOfRooms { get; set; }

        public int NumberOfStars { get; set; }
    }

    public class RequestAdminHotelDto
    {
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 1;

        public string? SearchKey { get; set; }

        public AdminSortingHotel AdminSortingHotel { get; set; } = AdminSortingHotel.Newest;
    }

    public enum AdminSortingHotel
    {
        Newest = 1,
        Oldest = 2,
        MostStar = 3,
        MinStar = 4,
    }
}
