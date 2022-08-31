using Application.ContextInterfaces;
using Application.Services.AdminHotelServices.AddHotel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.AdminHotelServices.FacadeAdminHotelService
{
    public class AdminHotelService : IAdminHotelService
    {
        private readonly IDatabaseContext db;

        public AdminHotelService(IDatabaseContext db)
        {
            this.db = db;
        }


        private IAddHotelService addHotelAsync;
        public IAddHotelService AddHotelAsync =>
            addHotelAsync ?? new AddHotelService(db);
    }
}
