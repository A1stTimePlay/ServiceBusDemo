using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using ProcessImage.Services;

[assembly: FunctionsStartup(typeof(ProcessImage.Startup))]
namespace ProcessImage
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddSingleton<IImageResizer, ImageResizer>();
        }
    }
}
