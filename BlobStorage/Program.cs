using BlobStorage.helper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlobStorage
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectString = ConfigurationManager.AppSettings["AzureStorage"];
            var blobHelper = new BlobHelper(connectString);

            //upload file
            //var basePath = AppDomain.CurrentDomain.BaseDirectory;
            //var filePath = $"{basePath}image\\pic1.png";
            //var filePath = $"{basePath}image\\pic2.png";
            //using (var fileStream = System.IO.File.OpenRead(filePath))
            //{
            //    blobHelper.UploadFile("kyletest", "test.png", fileStream);
            //}

            //-----------------
            //delete file
            //blobHelper.DeleteFile("kyletest", "test.png");

            //upload large file
            //var sw = new Stopwatch();
            //sw.Reset();
            //sw.Start();

            //var basePath = AppDomain.CurrentDomain.BaseDirectory;
            //var filePath = $"{basePath}video\\video1.mp4";
            //using (var fileStream = System.IO.File.OpenRead(filePath))
            //{
            //    //blobHelper.UploadFile("kyletest", "test_video.mp4", fileStream);
            //    blobHelper.UploadLargeFile("kyletest", "test_video.mp4", fileStream);
            //}
            //sw.Stop();
            //var result = sw.Elapsed.TotalMilliseconds;
            //Console.WriteLine(result);
            //Console.ReadLine();
        }
    }
}
