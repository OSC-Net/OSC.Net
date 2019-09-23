﻿using System;
using System.IO;
using System.Threading.Tasks;
using OSC.Net.Model;

namespace OSC.Net
{
    public static partial class Commands
    {
        public static async Task<Uri> TakePicture(this ICameraClient client)
        {
            await client.SetCaptureMode(CaptureMode.image);

            var result = await client.PostASJson<Model.TakePicture.Result>(new
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

            return Uri.TryCreate(status?.results?.fileUrl, UriKind.Absolute, out var uri)
                ? uri
                : throw new Exception($"Failed to fetch status / uri for {result.id}");
        }

        public static async Task TakePicture(this ICameraClient client, Stream targetStream)
        {
            var uri = await client.TakePicture();

            using (var sourceStream = await client.GetHttpClient().GetStreamAsync(uri))
            {
                await sourceStream.CopyToAsync(targetStream);
            }
        }

        public static async Task TakePicture(this ICameraClient client, string path)
        {
            using (var targetStream = File.Open(path, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                await client.TakePicture(targetStream);
            }
        }
    }
}