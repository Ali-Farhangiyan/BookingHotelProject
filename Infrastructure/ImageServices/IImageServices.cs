using Application.ContextInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ImageServices
{
    public interface IImageServices
    {
        Task<ImageAddress> ImageUploader(IFormFileCollection files);
        string CompserImage(string src);
    }

    public class ImageService : IImageServices
    {
        private readonly IDatabaseContext db;
        private readonly IConfiguration configuration;
        private readonly RestClient restClient;

        public ImageService(IDatabaseContext db, IConfiguration configuration)
        {
            this.db = db;
            this.configuration = configuration;
            restClient = new RestClient();
            restClient.Options.Timeout = -1;
            restClient.Options.BaseUrl = new Uri(configuration["ImageUri"]);
            
        }


        public string CompserImage(string src)
        {
            var uri = configuration["ImageUri"];
            return uri + src;
        }

        public async Task<ImageAddress> ImageUploader(IFormFileCollection files)
        {
            var request = new RestRequest("/api/ApiImageUploader/", Method.Post);

            foreach (var file in files)
            {
                byte[] bytes;
                using(var ms = new MemoryStream())
                { 
                    await file.CopyToAsync(ms);
                    bytes = ms.ToArray();
                }

                request.AddFile(file.FileName, bytes, file.FileName);
            }

            var response = await restClient.ExecuteAsync(request);

            if (response.Content is null) return null!;

            var result = JsonConvert.DeserializeObject<ImageAddress>(response.Content) ?? null!;

            var imageAddress = new ImageAddress();

            foreach (var item in result.Sources)
            {
                imageAddress.Sources.Add(CompserImage(item));
            }

            return imageAddress;
        }
    }

    public class ImageAddress
    {
        public List<string> Sources { get; set; } = null!;
    }
}
