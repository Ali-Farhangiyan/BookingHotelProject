using Application.Services.PaymentServices.PaymentFacadeService;
using Dto.Payment;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using ZarinPal.Class;

namespace BookingHotelUI.Controllers
{
    public class PayController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly IPaymentService paymentService;
        private readonly UserManager<Domain.Entites.User> userManager;
        private readonly string merchendId;

        private readonly Payment _payment;
        private readonly Authority authority;
        private readonly Transactions transactions;

        public PayController(IConfiguration configuration, IPaymentService paymentService,
            UserManager<Domain.Entites.User> userManager)
        {
            this.configuration = configuration;
            this.paymentService = paymentService;
            this.userManager = userManager;
            merchendId = configuration["ZarinPalMerchendId"];

            var expose = new Expose();
            _payment = expose.CreatePayment();
            authority = expose.CreateAuthority();
            transactions = expose.CreateTransactions();
        }


        public async Task<IActionResult> Index(Guid paymentId, string UserIdd)
        {
            var payment = await paymentService.GetPaymentAsync(paymentId);
            if (payment is null) return NotFound();
            string userId;
            if (User.Identity.IsAuthenticated)
            {
                userId = userManager.GetUserId(User);
            }
            else
            {
                userId = UserIdd;
            }
            
            if (userId != payment.UserId) return BadRequest();

            var callBackUrl = Url.Action(nameof(Verify), "Pay", new { payment.Id }, protocol: Request.Scheme);

            var resultZarinPalRequest = await _payment.Request(new DtoRequest()
            {
                Amount = payment.Amount,
                CallbackUrl = callBackUrl,
                Description = payment.Description,
                Email = payment.Email,
                MerchantId = merchendId,
                Mobile = payment.PhoneNumber
            }, Payment.Mode.sandbox);


            return Redirect($"https://sandbox.zarinpal.com/pg/StartPay/{resultZarinPalRequest.Authority}");
            
            
        }

        public async Task<IActionResult> Verify(Guid Id, string Authority)
        {
            var status = HttpContext.Request.Query["Status"];

            if(status != "" && status.ToString().ToLower() == "ok" && Authority != "")
            {
                // check the id is valid or no 
                var payment = await paymentService.GetPaymentAsync(Id);
                if (payment is null) return NotFound();

                var verification = await _payment.Verification(new DtoVerification
                {
                    Amount = payment.Amount,
                    Authority = Authority,
                    MerchantId = merchendId,

                }, Payment.Mode.sandbox);

                if (verification.Status == 100)
                {
                    var verifyResult = await paymentService.VerifyPaymentAsync(Id, Authority, verification.RefId);

                    if (verifyResult == true)
                    {
                        TempData["messageSuccess"] = "Payment is done!";
                        return RedirectToAction("CheckOut", "Shopping",new {UserId = payment.UserId});
                    }
                    else
                    {
                        TempData["messageFail"] = "Payment is done, but not registred!";
                        return RedirectToAction("CheckOut", "Shopping");
                    }
                }
                else
                {
                    TempData["messageFail"] = "your pay is not successful, plz try again";
                    return RedirectToAction("CheckOut", "Shopping");
                }

            }
            TempData["messageFail"] = "your pay is fail";
            return RedirectToAction("CheckOut", "Shopping");
        }
    }
}
