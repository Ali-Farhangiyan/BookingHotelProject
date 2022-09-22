using Application.Services.UserHotelServices.UserFacadeHotelService;
using BookingHotelUI.Areas.Customer.Models;
using Domain.Entites;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookingHotelUI.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly IUserHotelService userHotelService;
        private readonly UserManager<User> userManager;

        public HomeController(IUserHotelService userHotelService, UserManager<User> userManager)
        {
            this.userHotelService = userHotelService;
            this.userManager = userManager;
        }
        

        public async Task<IActionResult> Index()
        {
            var userId = userManager.GetUserId(User);
            var model = new CustomerViewModel
            {
                BookingData = await userHotelService.GetUserData.GetBookingData(userId),
                UserData = await userHotelService.GetUserData.ExecuteAsync(userId)
            };

            return View(model);
        }
    }
}
