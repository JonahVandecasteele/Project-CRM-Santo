using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Microsoft.WindowsAzure.Storage.Queue;
using System.Web;
using System.IO;

namespace CRMSanto.BusinessLayer
{
    class StorageHelper
    {
        public static bool AddImage(string ConnectionString, string Container, Stream img, string ImageName)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConfigurationManager.ConnectionStrings[ConnectionString].ToString());
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference(Container);
            container.SetPermissions(new BlobContainerPermissions
            {
                PublicAccess =
                    BlobContainerPublicAccessType.Blob
            });

            CloudBlockBlob blockBlob = container.GetBlockBlobReference(ImageName);
            blockBlob.UploadFromStream(img);
            return true;
        }
    }
}
