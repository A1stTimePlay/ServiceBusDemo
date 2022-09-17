using System;
using System.IO;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using ProcessImage.Services;

namespace ProcessImage
{
    [StorageAccount("BlobConnectionString")]
    public class ProcessSmallImageFunction
    {
        private readonly IImageResizer _imageResizer;
        public ProcessSmallImageFunction(IImageResizer imageResizer)
        {
            _imageResizer = imageResizer;
        }
        [FunctionName("ProcessSmallImageFunction")]
        public void Run(
            [BlobTrigger("raw-image/{name}")] Stream inputBlob,
            [Blob("small-image/{name}", FileAccess.Write)] Stream smallImageBlob,
            string name,
            ILogger log)
        {
            log.LogInformation($"C# Blob trigger function Processed blob\n Name:{name} \n Size: {inputBlob.Length} Bytes");
            try
            {
                this._imageResizer.ResizeSmall(inputBlob, smallImageBlob);
                log.LogInformation("Reduced image saved to blob storage");
            }
            catch (Exception ex)
            {
                log.LogError("Resize fails" + ex.Message, ex);
            }
        }
    }
}
