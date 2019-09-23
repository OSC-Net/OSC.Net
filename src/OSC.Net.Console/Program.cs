using System.IO;
using static System.Console;
using System.Threading.Tasks;
using OSC.Net.Model;

namespace OSC.Net.Console
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var client = new CameraClient();

            var pictureUri = await client.TakePicture();
            WriteLine("PictureUri: {0}", pictureUri);

            var imageStream = new MemoryStream();
            await client.TakePicture(imageStream);
            WriteLine("Downloaded {0} bytes.", imageStream.Length);

            await client.TakePicture("test.jpg");
            WriteLine("test.jpg {0}.", File.Exists("test.jpg") ? "exists" : "missing");

            var captureMode = await client.GetCaptureMode();
            WriteLine("CaptureMode: {0}", captureMode);

            await client.SetCaptureMode(CaptureMode.image);
        }
    }
}
