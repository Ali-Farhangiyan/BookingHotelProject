using Application.ContextInterfaces;
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


        private IShowHotelsForAdminService showAdminHotel;
        public IShowHotelsForAdminService ShowAdminHotel =>
            showAdminHotel ?? new ShowHotelsForAdminService(db);


        private IShowRoomsThatRelatedHotelService showRooms;
        public IShowRoomsThatRelatedHotelService ShowRooms =>
            showRooms ?? new ShowRoomsThatRelatedHotelService(db);



        private IAddFeatureToRoomsService addFeatureToRooms;
        public IAddFeatureToRoomsService AddFeatureToRooms =>
            addFeatureToRooms ?? new AddFeatureToRoomsService(db);


        private IAddRoomService addRoomToHotel;
        public IAddRoomService AddRoomToHotel =>
            addRoomToHotel ?? new AddRoomService(db);


        private IEditHotelService editHotel;
        public IEditHotelService EditHotel =>
            editHotel ?? new EditHotelService(db);


        private IEditRoomService editRoom;
        public IEditRoomService EditRoom =>
            editRoom ?? new EditRoomService(db);
    }
}
