using System;
using System.IO;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using ProcessImage.Services;

namespace ProcessImage
{
    [StorageAccount("BlobConnectionString")]
    public class ProcessMediumImageFunction
    {
        private readonly IImageResizer _imageResizer;
        public ProcessMediumImageFunction(IImageResizer imageResizer)
        {
            _imageResizer = imageResizer;
        }
        [FunctionName("ProcessMediumImageFunction")]
        public void Run(
            [BlobTrigger("raw-image/{name}")] Stream inputBlob,
            [Blob("medium-image/{name}", FileAccess.Write)] Stream mediumImageBlob,
            string name,
            ILogger log)
        {
            log.LogInformation($"C# Blob trigger function Processed blob\n Name:{name} \n Size: {inputBlob.Length} Bytes");
            try
            {
                this._imageResizer.ResizeMedium(inputBlob, mediumImageBlob);
                log.LogInformation("Reduced image saved to blob storage");
            }
            catch (Exception ex) 
            {
                log.LogError("Resize fails. " + ex.Message, ex);
            }
        }
    }
}
