using Application.ContextInterfaces;
using Application.Services.UserHotelServices.GetHotelsForSearchKey;
using Application.Services.UserHotelServices.GetUserData;
using Application.Services.UserHotelServices.OrderForBookingHotel;
using Application.Services.UserHotelServices.SearchHotelsByNameAndDate;
using Application.Services.UserHotelServices.ShowHotelWithRelatedDate;
using Application.Services.UserHotelServices.ShowPopularHotels;
using Application.Services.UserHotelServices.TrackOrder;
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

        public IGetUserDataService GetUserData { get; }

        public ITrackOrderService TrackOrder { get; }

        public IShowPopularHotelService PopularHotels { get; }
    }

    public class UserHotelService : IUserHotelService
    {
        private readonly IDatabaseContext db;
        private readonly IIdentityDatabaseContext identityDb;

        public UserHotelService(IDatabaseContext db, IIdentityDatabaseContext identityDb)
        {
            this.db = db;
            this.identityDb = identityDb;
        }


        private ISearchHotelsByNameAndDateService searchHotel;
        public ISearchHotelsByNameAndDateService SearchHotel =>
            searchHotel ?? new SearchHotelsByNameAndDateService(db);


        private IGetHotelsForSearchKeyService getHotels;
        public IGetHotelsForSearchKeyService GetHotels =>
            getHotels ?? new GetHotelsForSearchKeyService(db);


        private IShowHotelWithRelatedDateService showRelatedHotel;
        public IShowHotelWithRelatedDateService ShowRelatedHotel =>
            showRelatedHotel ?? new ShowHotelWithRelatedDateService(db, identityDb);


        private IOrderForBookingHotelService orderForBooking;
        public IOrderForBookingHotelService OrderForBooking =>
            orderForBooking ?? new OrderForBookingHotelService(db);


        private IGetUserDataService getUserData;
        public IGetUserDataService GetUserData =>
            getUserData ?? new GetUserDataService(identityDb,db);


        private ITrackOrderService trackOrder;
        public ITrackOrderService TrackOrder =>
            trackOrder ?? new TrackOrderService(db);


        private IShowPopularHotelService popularHotels;
        public IShowPopularHotelService PopularHotels =>
            popularHotels ?? new ShowPopularHotelService(db);
    }
}
