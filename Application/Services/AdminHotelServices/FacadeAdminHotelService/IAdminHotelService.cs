using Application.Services.AdminHotelServices.AddFeatureToRooms;
using Application.Services.AdminHotelServices.AddHotel;
using Application.Services.AdminHotelServices.AddRoom;
using Application.Services.AdminHotelServices.EditHotel;
using Application.Services.AdminHotelServices.EditRoom;
using Application.Services.AdminHotelServices.ShowHotelsForAdmin;
using Application.Services.AdminHotelServices.ShowRoomsThatRelatedHotel;
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

        public IShowHotelsForAdminService ShowAdminHotel { get; }

        public IShowRoomsThatRelatedHotelService ShowRooms { get; }

        public IAddFeatureToRoomsService AddFeatureToRooms { get; }

        public IAddRoomService AddRoomToHotel { get; }

        public IEditHotelService EditHotel { get; }

        public IEditRoomService EditRoom { get; }
    }
}
