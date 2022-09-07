using Application.Services.UserHotelServices.UserFacadeHotelService;
using Microsoft.AspNetCore.Mvc;

namespace BookingHotelUI.Controllers
{
    public class ShoppingController : Controller
    {
        private readonly IUserHotelService userHotelService;

        public ShoppingController(IUserHotelService userHotelService)
        {
            this.userHotelService = userHotelService;
        }
        public async Task<IActionResult> ShowHotel(int hotelId, DateTime startDate, DateTime endDate)
        {
            var model = await userHotelService.ShowRelatedHotel.ExecuteAsync(hotelId, startDate, endDate);
            return View(model);
        }
    }
}
