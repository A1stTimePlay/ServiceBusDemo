using ProcessImage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessImage.Services
{
    public class ImageAnalysis : IImageAnalysis
    {
        public FileMetadata AnalyzeImage(string name, long size)
        {
            Random random = new Random();
            var color = new Color
            {
                Red = random.Next(0, 255),
                Green = random.Next(0, 255),
                Blue = random.Next(0, 255),
            };
            var result = new FileMetadata
            {
                Name = name,
                Size = size,
                Color = color,
                Category = (CategoryEnum)random.Next(0, 4)
            };

            return result;
        }
    }
}
