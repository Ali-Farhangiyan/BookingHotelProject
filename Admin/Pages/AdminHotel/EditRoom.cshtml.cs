using Application.Services.AdminHotelServices.FacadeAdminHotelService;
using Application.Services.AdminHotelServices.ShowRoomsThatRelatedHotel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Admin.Pages.AdminHotel
{
    public class EditRoomModel : PageModel
    {
        private readonly IAdminHotelService adminHotelService;

        public EditRoomModel(IAdminHotelService adminHotelService)
        {
            this.adminHotelService = adminHotelService;
        }

        [BindProperty]
        public RoomAdminDto Room { get; set; }
        public async Task OnGet(int roomId)
        {
            Room = await adminHotelService.EditRoom.ShowRoomById(roomId);
        }

        public async Task<IActionResult> OnPost()
        {
            var result = await adminHotelService.EditRoom.ExecuteAsync(Room);
            if(result == true)
            {
                return RedirectToPage("ShowHotels");
            }
            return Page();
        }
    }
}
