using System;
using System.Threading.Tasks;
using OSC.Net.Model;
using OSC.Net.Model.SetOptions;

namespace OSC.Net
{
    public static partial class Commands
    {
        /// <summary>
        /// Gets camera current capture mode.
        /// </summary>
        /// <param name="client">The <see cref="ICameraClient"/> method extends.</param>
        /// <returns>Current <see cref="CaptureMode"/>.</returns>
        public static async Task<CaptureMode> GetCaptureMode(this ICameraClient client)
        {
            var result = await client.PostAsJson<Model.GetOptions.Result<Model.GetCaptureMode.Options>>(new
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

            return Enum.TryParse(result?.results?.options?.captureMode, true, out CaptureMode mode)
                ? mode
                : CaptureMode.unknown;
        }

        /// <summary>
        /// Sets camera capture mode.
        /// </summary>
        /// <param name="client">The <see cref="ICameraClient"/> method extends.</param>
        /// <param name="captureMode">Supported capture modes <see cref="CaptureMode.image"/> and <see cref="CaptureMode.video"/>.</param>
        /// <returns></returns>
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

            var result = await client.PostAsJson<Result>(new
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
