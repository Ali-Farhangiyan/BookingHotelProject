using Application.Services.AdminHotelServices.AddHotel;
using Application.Services.AdminHotelServices.FacadeAdminHotelService;
using Infrastructure.ImageServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Admin.Pages.AdminHotel
{
    public class AddHotelModel : PageModel
    {
        private readonly IAdminHotelService adminHotelService;
        private readonly IImageServices imageServices;

        public AddHotelModel(IAdminHotelService adminHotelService, IImageServices imageServices)
        {
            this.adminHotelService = adminHotelService;
            this.imageServices = imageServices;
        }

        [BindProperty]
        public AddHotelDto Data { get; set; }
        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPost()
        {
            //if (!ModelState.IsValid)
            //{
            //    return StatusCode(400);
            //}
            var files = Request.Form.Files;
            var resultImages = await imageServices.ImageUploader(files);

            var images = new List<ImageDto>();

            foreach (var imageAddress in resultImages.Sources)
            {
                images.Add(new ImageDto { Src = imageAddress });
            }

            Data.Images = images;

            var result = await adminHotelService.AddHotelAsync.ExecuteAsync(Data);
            if (result == true)
            {
                return StatusCode(200);
            }



            return StatusCode(400);

        }
    }
}
