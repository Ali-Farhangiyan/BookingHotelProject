using Microsoft.AspNetCore.Mvc;
using StaticFiles.Attributes;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;


namespace StaticFiles.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiImageUploaderController : ControllerBase
    {
        private readonly IHostingEnvironment env;

        public ApiImageUploaderController(IHostingEnvironment env)
        {
            this.env = env;
        }

        [HttpPost]
        [DisableFormValueModelBinding]
        public async Task<IActionResult> Post()
        {
            var files = Request.Form.Files;

            if (files.Any())
            {
                return Ok(await UploadImage(files));
            }
            else
            {
                return BadRequest();
            }

        }

        private async Task<ImageAddress> UploadImage(IFormFileCollection files)
        {
            var date = DateTime.Now;
            var folder = $@"Images\{date.Year}\{date.Month}-{date.Day}\";
            var uploadFolder = Path.Combine(env.WebRootPath, folder);
            if (!Directory.Exists(uploadFolder)) Directory.CreateDirectory(uploadFolder);


            var Adds = new ImageAddress();
            foreach (var file in files)
            {
                var guid = Guid.NewGuid();
                var fileName = guid + file.FileName;
                
                var path = Path.Combine(uploadFolder, fileName);


                using(var fs = new FileStream(path,FileMode.Create))
                {
                    
                    await file.CopyToAsync(fs);
                    
                }

                Adds.Sources.Add(folder + fileName);
                
            }

            return Adds;


        }
    }

    public class ImageAddress
    {

        public List<string> Sources { get; set; } = new List<string>();
    }
}
