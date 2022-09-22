using Application.Services.CommentServices.AddComment;
using Application.Services.CommentServices.CommentFacadeService;
using Application.Services.UserHotelServices.SearchHotelsByNameAndDate;
using Application.Services.UserHotelServices.UserFacadeHotelService;
using BookingHotelUI.Models;
using BookingHotelUI.Models.ViewModels;
using Domain.Entites;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BookingHotelUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserHotelService userHotelService;
        private readonly UserManager<User> userManager;
        private readonly ICommentService commentService;

        public HomeController(ILogger<HomeController> logger,
            IUserHotelService userHotelService, 
            UserManager<User> userManager,
            ICommentService commentService)
        {
            _logger = logger;
            this.userHotelService = userHotelService;
            this.userManager = userManager;
            this.commentService = commentService;
        }

        public async Task<IActionResult> Index()
        {
            var model = new HomeViewModel
            {
                PopularHotels = await userHotelService.PopularHotels.ExecuteAsync()
            };


            return View(model);
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

        public async Task<StatusCodeResult> AddComment(AddCommentDto comment)
        {
            var userId = userManager.GetUserId(User);
            comment.UserId = userId;
            var userData = await userHotelService.GetUserData.ExecuteAsync(userId);
            comment.UserName = userData.FullName;
            var result = await commentService.AddComment.ExecuteAsync(comment);
            if (result)
            {
                return StatusCode(200);

            }
            else
            {
                return StatusCode(400);

            }
        }

        public async Task<IActionResult> Track(int? bookingId)
        {
            var model = await userHotelService.TrackOrder.TrackOrderAsync(bookingId);
            return View(model);
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