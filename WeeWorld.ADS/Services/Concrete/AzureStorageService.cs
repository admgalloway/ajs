using System;
using System.Configuration;
using System.IO;
using System.Net.Mime;
using WeeWorld.ADS.Services.Abstract;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace WeeWorld.ADS.Services.Concrete
{
    public class AzureStorageService : IStorageService
    {
        private readonly CloudStorageAccount storageAccount;
        private readonly CloudBlobClient blobClient;
        private readonly CloudBlobContainer container;

        public AzureStorageService()
        {
            var connstring = ConfigurationManager.ConnectionStrings["StorageConnection"].ConnectionString;

            this.storageAccount = CloudStorageAccount.Parse(connstring);
            this.blobClient = storageAccount.CreateCloudBlobClient();
            this.container = blobClient.GetContainerReference("app-icons");
        }

        public string UploadImage(string filename, string contentType, Stream image)
        {
            if (image == null)
                throw new ArgumentException("image cannot be null");
            
            var blob = container.GetBlockBlobReference(filename);

            blob.Properties.ContentType = contentType;
            blob.UploadFromStream(image);
            return blob.Uri.ToString();
        }

        public string GetImageUrl(int appId)
        {
            string filename = string.Format("{0}.png", appId);
            var img = container.GetBlockBlobReference(filename);

            if (img.Exists())
                return img.Uri.ToString();

            // image doesnt exist, return
            var placeholder = container.GetBlockBlobReference("placeholder.png");
            return placeholder.Uri.ToString();

        }
    }
}