using Application.Services.AdminHotelServices.AddHotel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.AdminHotelServices.FacadeAdminHotelService
{
    public interface IAdminHotelService
    {
        public IAddHotelService AddHotelAsync { get; }
    }
}
