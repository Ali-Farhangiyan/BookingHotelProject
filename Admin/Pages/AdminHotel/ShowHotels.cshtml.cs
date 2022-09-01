using Application.Pagination;
using Application.Services.AdminHotelServices.FacadeAdminHotelService;
using Application.Services.AdminHotelServices.ShowHotelsForAdmin;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Admin.Pages.AdminHotel
{
    public class ShowHotelsModel : PageModel
    {
        private readonly IAdminHotelService adminHotelService;

        public ShowHotelsModel(IAdminHotelService adminHotelService)
        {
            this.adminHotelService = adminHotelService;
        }

        public PaginatedList<ShowHotelAdminDto> ShowHotel { get; set; }
        public async Task OnGet(RequestAdminHotelDto request)
        {
            ShowHotel = await adminHotelService.ShowAdminHotel.ExecuteAsync(request);
        }
    }
}
