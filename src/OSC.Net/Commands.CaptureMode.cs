using System;
using System.Threading.Tasks;
using OSC.Net.Model;

namespace OSC.Net
{
    public static partial class Commands
    {
        public static async Task<CaptureMode> GetCaptureMode(this ICameraClient client)
        {
            var result = await client.PostASJson<Model.GetCaptureMode.Result>(new
            {
                name = "camera.getOptions",
                parameters = new
                {
                    optionNames = new[]
                    {
                        "captureMode"
                    }
                }
            });

            return result?.results?.options?.captureMode ?? CaptureMode.unknown;
        }

        public static async Task SetCaptureMode(this ICameraClient client, CaptureMode captureMode)
        {
            switch (captureMode)
            {
                case CaptureMode.image:
                case CaptureMode.video:
                    break;

                // ReSharper disable once RedundantCaseLabel
                case CaptureMode._other:
                // ReSharper disable once RedundantCaseLabel
                case CaptureMode.unknown:
                default:
                    throw new ArgumentOutOfRangeException(nameof(captureMode), captureMode, null);
            }

            var result = await client.PostASJson<Model.SetCaptureMode.Result>(new
            {
                name = "camera.setOptions",
                parameters = new
                {
                    options = new
                    {
                        captureMode = captureMode.ToString("F")
                    }
                }
            });

            if (result?.error != null)
            {
                throw new Exception($"Failed to set options (code: {result.error.code}, message: {result.error.message}).");
            }
        }
    }
}
