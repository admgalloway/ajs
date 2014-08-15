using System.Collections.Generic;
using System.IO;
using WeeWorld.ADS.Data.Models;

namespace WeeWorld.ADS.Services.Abstract
{
    public interface IStorageService
    {
        string UploadImage(string filename, string contentType, Stream image);

        string GetImageUrl(int appId);
    }
}