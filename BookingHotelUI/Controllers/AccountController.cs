using BookingHotelUI.Models.ViewModels;
using Domain.Entites;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookingHotelUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel register)
        {
            if (!ModelState.IsValid)
            {
                return View(register);
                ViewData["RegisterNOt"] = ModelState.ValidationState;
            }

            var user = new User
            {
                FirstName = register.FirstName,
                LastName = register.LastName,
                Email = register.Email,
                PhoneNumber = register.PhoneNumber,
                UserName = register.NationalCode,
                NationalCode = register.NationalCode,
            };

            var result = await userManager.CreateAsync(user, register.PhoneNumber);

            if (result.Succeeded) return RedirectToAction("Login");
            foreach (var item in result.Errors)
            {
                ViewData["RegisterNOt"] = item.Description;
            }
            return View(register);
            
            //ViewData["RegisterNOt"] = result.Errors;

        }

        public IActionResult Login(string returnUrl = "/")
        {
            return View(new LoginViewModel
            {
                ReturnUrl = returnUrl
            });
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel login)
        {
            if (!ModelState.IsValid)
            {
                return View(login);
            }
            await signInManager.SignOutAsync();
            var userFind = await userManager.FindByNameAsync(login.UserName);
            if(userFind is null)
            {
                ViewData["NotUserFound"] = "The username or password is incorrect";
                return View(login);
            }
            var result = await signInManager.PasswordSignInAsync(userFind, login.Password, login.IsRememberMe, true);
            if (result.Succeeded) return RedirectToAction("Index", "Home");

            ViewData["NotUserFound"] = "The username or password is incorrect";
            return View(login);

        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
