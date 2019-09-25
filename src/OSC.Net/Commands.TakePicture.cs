using System;
using System.IO;
using System.Threading.Tasks;
using OSC.Net.Model;

namespace OSC.Net
{
    public static partial class Commands
    {
        /// <summary>
        /// Takes picture and returns uri where it can be downloaded.
        /// </summary>
        /// <param name="client">The <see cref="ICameraClient"/> method extends.</param>
        /// <param name="useLocalFileUri">Optional flag for if absolute uri returned from camera should be used or absolute uri created from <see cref="ICameraClient.EndPoint"/> and relative uri, useful if called through a proxy. Default value <c>false</c></param>
        /// <returns>Absolute uri to image.</returns>
        public static async Task<Uri> TakePicture(this ICameraClient client, bool useLocalFileUri = false)
        {
            await client.SetCaptureMode(CaptureMode.image);
            await client.SetDateTime(DateTimeOffset.Now);

            var result = await client.PostAsJson<Model.TakePicture.Result>(new
            {
                name = "camera.takePicture"
            });

            if (string.IsNullOrWhiteSpace(result.id))
            {
                throw new Exception("No id returned from take picture.");
            }

            Model.TakePicture.Status.Result status;
            do
            {
                await Task.Delay(500);
                status = await client.GetStatus<Model.TakePicture.Status.Result>(result.id);
            } while (status?.state == "inProgress");

            Uri uri;
            return
                (useLocalFileUri && Uri.TryCreate(client.EndPoint, status?.results?._localFileUrl, out uri))
                || Uri.TryCreate(status?.results?.fileUrl, UriKind.Absolute, out uri)
                    ? uri
                    : throw new Exception($"Failed to fetch status / uri for {result.id}");
        }

        /// <summary>
        /// Takes picture and downloads image to specified stream.
        /// </summary>
        /// <param name="client">The <see cref="ICameraClient"/> method extends.</param>
        /// <param name="targetStream">The target stream image is downloaded to.</param>
        /// <param name="useLocalFileUri">Optional flag for if absolute uri returned from camera should be used or absolute uri created from <see cref="ICameraClient.EndPoint"/> and relative uri, useful if called through a proxy. Default value <c>false</c></param>
        public static async Task TakePicture(this ICameraClient client, Stream targetStream, bool useLocalFileUri = false)
        {
            var uri = await client.TakePicture(useLocalFileUri);

            using (var sourceStream = await client.GetHttpClient().GetStreamAsync(uri))
            {
                await sourceStream.CopyToAsync(targetStream);
            }
        }

        /// <summary>
        /// Takes picture and downloads image to specified local path.
        /// </summary>
        /// <param name="client">The <see cref="ICameraClient"/> method extends.</param>
        /// <param name="path">The local path to download to.</param>
        /// <param name="useLocalFileUri">Optional flag for if absolute uri returned from camera should be used or absolute uri created from <see cref="ICameraClient.EndPoint"/> and relative uri, useful if called through a proxy. Default value <c>false</c></param>
        public static async Task TakePicture(this ICameraClient client, string path, bool useLocalFileUri = false)
        {
            using (var targetStream = client.CreateFile(path))
            {
                await client.TakePicture(targetStream, useLocalFileUri);
            }
        }
    }
}
