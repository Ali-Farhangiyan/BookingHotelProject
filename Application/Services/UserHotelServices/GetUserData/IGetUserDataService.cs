using Application.ContextInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.UserHotelServices.GetUserData
{
    public interface IGetUserDataService
    {
        Task<GetUserDto> ExecuteAsync(string userId); 
    }

    public class GetUserDataService : IGetUserDataService
    {
        private readonly IIdentityDatabaseContext identityDb;

        public GetUserDataService(IIdentityDatabaseContext identityDb)
        {
            this.identityDb = identityDb;
        }
        public async Task<GetUserDto> ExecuteAsync(string userId)
        {
            var user = await identityDb.Users
                .Where(u => u.Id == userId)
                .Select(u => new GetUserDto
                {
                    Email = u.Email,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    FullName = u.FirstName + u.LastName,
                    NatinalCode = u.NationalCode,
                    PhoneNumber = u.PhoneNumber
                }).FirstOrDefaultAsync();

            return user;
        }
    }

    public class GetUserDto
    {
        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string FullName { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string NatinalCode { get; set; } = null!;
    }
}
