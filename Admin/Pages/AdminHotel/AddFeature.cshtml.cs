using Application.Services.AdminHotelServices.AddFeatureToRooms;
using Application.Services.AdminHotelServices.FacadeAdminHotelService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Admin.Pages.AdminHotel
{
    public class AddFeatureModel : PageModel
    {
        private readonly IAdminHotelService adminHotelService;

        public AddFeatureModel(IAdminHotelService adminHotelService)
        {
            this.adminHotelService = adminHotelService;
        }

        [BindProperty]
        public RoomsFeatureDto RoomsFeature { get; set; }
        public void OnGet(int roomId)
        {
           
        }

        public async Task<StatusCodeResult> OnPost()
        {
            var result = await adminHotelService.AddFeatureToRooms.ExecuteAsync(RoomsFeature);
            if (result == true)
            {
                return StatusCode(200);
            }
            else
            {
                return StatusCode(400);
            }

        }
    }
}
