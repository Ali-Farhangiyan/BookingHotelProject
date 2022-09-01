using Application.Services.AdminHotelServices.AddHotel;
using Application.Services.AdminHotelServices.FacadeAdminHotelService;
using Application.Services.AdminHotelServices.ShowRoomsThatRelatedHotel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Admin.Pages.AdminHotel
{
    public class RoomFeatureModel : PageModel
    {
        private readonly IAdminHotelService adminHotelService;

        public RoomFeatureModel(IAdminHotelService adminHotelService)
        {
            this.adminHotelService = adminHotelService;
        }

        public List<RoomAdminDto> Rooms { get; set; }

        
        public async Task OnGet(int hotelId)
        {
            Rooms = await adminHotelService.ShowRooms.ExecuteAsync(hotelId);
        }
    }
}
