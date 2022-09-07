using Application.Services.UserHotelServices.SearchHotelsByNameAndDate;
using Application.Services.UserHotelServices.UserFacadeHotelService;
using BookingHotelUI.Models;
using BookingHotelUI.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BookingHotelUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserHotelService userHotelService;

        public HomeController(ILogger<HomeController> logger, IUserHotelService userHotelService)
        {
            _logger = logger;
            this.userHotelService = userHotelService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SearchForm(int cityId, DateTime startDate, DateTime endDate)
        {


            var model = await userHotelService.SearchHotel.ExecuteAsync(cityId, startDate, endDate);


            TempData["startDate"] = startDate;
            TempData["endDate"] = endDate;

           
            return View(model);
           
        }

        
        public async Task<JsonResult> GetHotels(string? search)
        {
            var hotels = await userHotelService.GetHotels.ExecuteAsync(search);
            var model = new JsonResult(hotels);
            ;
            return model;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}