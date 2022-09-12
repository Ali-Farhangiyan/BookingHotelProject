using Application.ContextInterfaces;
using Application.Services.UserHotelServices.GetHotelsForSearchKey;
using Application.Services.UserHotelServices.OrderForBookingHotel;
using Application.Services.UserHotelServices.SearchHotelsByNameAndDate;
using Application.Services.UserHotelServices.ShowHotelWithRelatedDate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.UserHotelServices.UserFacadeHotelService
{
    public interface IUserHotelService
    {
        public ISearchHotelsByNameAndDateService SearchHotel { get; }
        public IGetHotelsForSearchKeyService GetHotels { get; }

        public IShowHotelWithRelatedDateService ShowRelatedHotel { get; }

        public IOrderForBookingHotelService OrderForBooking { get; }
    }

    public class UserHotelService : IUserHotelService
    {
        private readonly IDatabaseContext db;

        public UserHotelService(IDatabaseContext db)
        {
            this.db = db;
        }


        private ISearchHotelsByNameAndDateService searchHotel;
        public ISearchHotelsByNameAndDateService SearchHotel =>
            searchHotel ?? new SearchHotelsByNameAndDateService(db);


        private IGetHotelsForSearchKeyService getHotels;
        public IGetHotelsForSearchKeyService GetHotels =>
            getHotels ?? new GetHotelsForSearchKeyService(db);


        private IShowHotelWithRelatedDateService showRelatedHotel;
        public IShowHotelWithRelatedDateService ShowRelatedHotel =>
            showRelatedHotel ?? new ShowHotelWithRelatedDateService(db);


        private IOrderForBookingHotelService orderForBooking;
        public IOrderForBookingHotelService OrderForBooking =>
            orderForBooking ?? new OrderForBookingHotelService(db);
    }
}
