using Application.Services.PaymentServices.PaymentFacadeService;
using Application.Services.UserHotelServices.OrderForBookingHotel;
using Application.Services.UserHotelServices.UserFacadeHotelService;
using BookingHotelUI.Models.ViewModels;
using Domain.Entites;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BookingHotelUI.Controllers
{
    public class ShoppingController : Controller
    {
        private readonly IUserHotelService userHotelService;
        private readonly UserManager<User> userManager;
        private readonly IPaymentService paymentService;

        public ShoppingController(IUserHotelService userHotelService, UserManager<User> userManager
            ,IPaymentService paymentService)
        {
            this.userHotelService = userHotelService;
            this.userManager = userManager;
            this.paymentService = paymentService;
        }
        public async Task<IActionResult> ShowHotel(int hotelId, DateTime startDate, DateTime endDate)
        {
            var model = await userHotelService.ShowRelatedHotel.ExecuteAsync(hotelId, startDate, endDate);
            return View(model);
        }

        public async Task<IActionResult> RegisterBooking(BookingViewModel model)
        {
            var lengthOfStay = (int) (model.EndDate - model.StartDate).TotalDays;
           
            var ss = new BookingViewModel
            {
                EndDate = model.EndDate,
                HotelId = model.HotelId,
                RoomId = model.RoomId,
                LengthOfStay = lengthOfStay,
                Price = model.Price,
                StartDate = model.StartDate,
                Image = model.Image,
                HotelName = model.HotelName,
                RoomName = model.RoomName
            };

            TempData["BookingViewMode"] = JsonConvert.SerializeObject(ss);



            if (User.Identity.IsAuthenticated)
            {
                var user = await userManager.GetUserAsync(User);
                var infoUser = new BookingRegisterViewModel
                {
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    NationalCode = user.NationalCode,
                    PhoneNumber = user.PhoneNumber
                };

                return View(infoUser);
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterBooking(BookingRegisterViewModel register)
        {
            var userFind = await userManager.FindByNameAsync(register.NationalCode);

            if(userFind is null)
            {
                //return RedirectToAction("Register", "Account", new
                //{
                //    FirstName = register.FirstName,
                //    LastName = register.LastName,
                //    NationalCode = register.NationalCode,
                //    PhoneNumber = register.PhoneNumber,
                //    Email = register.Email
                //});

                var newUser = new User
                {
                    FirstName = register.FirstName,
                    LastName = register.LastName,
                    Email = register.Email,
                    PhoneNumber = register.PhoneNumber,
                    UserName = register.NationalCode,
                    NationalCode = register.NationalCode,
                };

                var result = await userManager.CreateAsync(newUser, register.PhoneNumber);
            }

            var ss = new BookingRegisterViewModel
            {
                FirstName = register.FirstName,
                LastName = register.LastName,
                NationalCode = register.NationalCode,
                PhoneNumber = register.PhoneNumber,
                Email = register.Email,
                PassengerName = register.PassengerName,
                PassengerFamilyName = register.PassengerFamilyName
            };

            TempData["RegisterInfo"] = JsonConvert.SerializeObject(ss);

            return RedirectToAction("ConfirmDataBooking");
        }

        public async Task<IActionResult> ConfirmDataBooking()
        {
            return View();
        }
        //ShippingPayment
        public async Task<IActionResult> PayAction(OrderDto data)
        {
            
            var user = await userManager.FindByEmailAsync(data.UserEmail);
            data.UserId = user.Id;

            var bookingId = await userHotelService.OrderForBooking.ExecuteAsync(data);
            TempData["RoomIdForRoomBooking"] = data.RoomId;
            // register payment
            var payment = await paymentService.PayForBooking(bookingId);

            // send to payment gate
            return RedirectToAction("Index", "Pay", new { PaymentId = payment.PaymentId, UserIdd = user.Id });
            return View();
        }

        public async Task<IActionResult> CheckOut(string? userId)
        {
            if(userId is not null)
            {
                var model = await userHotelService.GetUserData.ShowResultAfterPay(userId);
                return View(model);
            }

            return View();
            
        }
    }
}
