using System.Configuration;
using System.IO;
using System.Web.Mvc;
using WeeWorld.ADS.Services.Abstract;
using Microsoft.WindowsAzure.Storage;

namespace WeeWorld.ADS.Controllers.Web
{
    public class ContentController : Controller
    {
        private readonly IStorageService storageService;

        public ContentController(IStorageService storageService)
        {
            this.storageService = storageService;
        }

        /// <summary>accept image upload for app icons</summary>
        [HttpPost]
        public JsonResult Upload()
        {
            var image = Request.Files["image"];
            string filename = string.Format("{0}{1}", "placeholder", ".png");
            string filetype = image.ContentType;

            var result = storageService.UploadImage(filename, filetype, image.InputStream);
            
            return new JsonResult {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                Data = new {
                    success = true,
                    url = result
                }
            };
        }
    }
}