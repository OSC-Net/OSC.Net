using System;
using System.IO;
using System.Net.Http;
using static System.Console;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using OSC.Net.Model;

namespace OSC.Net.Console
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var serviceProvider = new ServiceCollection().AddHttpClient().BuildServiceProvider();
            var httpClientFactory = serviceProvider.GetService<IHttpClientFactory>();


            var client = Uri.TryCreate(
                Environment.GetEnvironmentVariable("OSC.Net.Console.EndPoint"),
                UriKind.Absolute,
                out var endpoint)
                ? new CameraClient(endpoint, httpClientFactory.CreateClient)
                : new CameraClient(httpClientFactory.CreateClient);

            var pictureUri = await client.TakePicture(true);
            WriteLine("PictureUri: {0}", pictureUri);

            var imageStream = new MemoryStream();
            await client.TakePicture(imageStream, true);
            WriteLine("Downloaded {0} bytes.", imageStream.Length);

            await client.TakePicture("test.jpg", true);
            WriteLine("test.jpg {0}.", File.Exists("test.jpg") ? "exists" : "missing");

            var captureMode = await client.GetCaptureMode();
            WriteLine("CaptureMode: {0}", captureMode);

            await client.SetCaptureMode(CaptureMode.image);
        }
    }
}
