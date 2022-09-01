using Application.Services.AdminHotelServices.EditHotel;
using Application.Services.AdminHotelServices.FacadeAdminHotelService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Admin.Pages.AdminHotel
{
    public class EditHotelModel : PageModel
    {
        private readonly IAdminHotelService adminHotelService;

        public EditHotelModel(IAdminHotelService adminHotelService)
        {
            this.adminHotelService = adminHotelService;
        }

        [BindProperty]
        public EditHotelDto Hotel { get; set; }
        public async Task OnGet(int id)
        {
            Hotel = await adminHotelService.EditHotel.ShowHotelById(id);
        }

        public async Task<IActionResult> OnPost()
        {
            var result = await adminHotelService.EditHotel.ExecuteAsync(Hotel);
            if (result == true) return RedirectToPage("ShowHotels");
            return Page();
        }
    }
}
