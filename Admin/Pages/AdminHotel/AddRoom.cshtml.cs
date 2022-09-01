using Application.Services.AdminHotelServices.AddRoom;
using Application.Services.AdminHotelServices.FacadeAdminHotelService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Admin.Pages.AdminHotel
{
    public class AddRoomModel : PageModel
    {
        private readonly IAdminHotelService adminHotelService;

        public AddRoomModel(IAdminHotelService adminHotelService)
        {
            this.adminHotelService = adminHotelService;
        }

        [BindProperty]
        public AddRoomToHotelDto RoomHotel { get; set; }
        public void OnGet()
        {
        }

        public async Task<StatusCodeResult> OnPost()
        {
            var result = await adminHotelService.AddRoomToHotel.ExecuteAsync(RoomHotel);
            if(result == true)
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
