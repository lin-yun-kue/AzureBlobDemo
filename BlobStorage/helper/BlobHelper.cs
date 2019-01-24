using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.RetryPolicies;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlobStorage.helper
{
    public class BlobHelper
    {
        public string StorageConnectionString { get; set; }
        public string ErrorMessage { get; set; }

        private CloudStorageAccount _storageAccount;
        private CloudBlobClient _blobClient;

        public BlobHelper(string connectString)
        {
            this.StorageConnectionString = connectString;

            _storageAccount = CloudStorageAccount.Parse(this.StorageConnectionString);
            _blobClient = _storageAccount.CreateCloudBlobClient();
        }

        public bool UploadFile(string container, string fileName, Stream stream)
        {
            bool status = true;
            try
            {
                CloudBlobContainer blobContainer = _blobClient.GetContainerReference(container);
                blobContainer.CreateIfNotExistsAsync().Wait();
                CloudBlockBlob blockBlob = blobContainer.GetBlockBlobReference(fileName);
                blockBlob.UploadFromStream(stream);
            }
            catch (Exception e)
            {
                ErrorMessage = e.Message;
                status = false;
            }
            return status;
        }

        public bool UploadLargeFile(string container, string fileName, Stream stream)
        {
            TimeSpan backOffPeriod = TimeSpan.FromSeconds(2);
            int retryCount = 1;
            BlobRequestOptions bro = new BlobRequestOptions()
            {
                SingleBlobUploadThresholdInBytes = 50 * 1024 * 1024, 
                ParallelOperationThreadCount = 5,
                RetryPolicy = new ExponentialRetry(backOffPeriod, retryCount),
            };
            _blobClient.DefaultRequestOptions = bro;
            CloudBlobContainer blobContainer = _blobClient.GetContainerReference(container);

            CloudBlockBlob blob = blobContainer.GetBlockBlobReference(fileName);

            blob.UploadFromStream(stream);

            return true;
        }

        public bool DeleteFile(string container, string fileName)
        {
            bool status = true;
            try
            {
                CloudBlobContainer blobContainer = _blobClient.GetContainerReference(container);
                CloudBlockBlob blockBlob = blobContainer.GetBlockBlobReference(fileName);
                blockBlob.DeleteIfExists();
            }
            catch (Exception e)
            {
                ErrorMessage = e.Message;
                status = false;
            }
            return status;
        }

        public string GetFileUrl(string container, string fileName)
        {
            CloudBlobContainer blobContainer = _blobClient.GetContainerReference(container);
            CloudBlockBlob blockBlob = blobContainer.GetBlockBlobReference(fileName);
            return (blockBlob.Exists()) ? blockBlob.Uri.AbsoluteUri : "";
        }
    }
}
