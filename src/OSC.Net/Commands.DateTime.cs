using System;
using System.Globalization;
using System.Threading.Tasks;
using OSC.Net.Model.SetOptions;

namespace OSC.Net
{
    public static partial class Commands
    {
        public static async Task<DateTimeOffset?> GetDateTime(this ICameraClient client)
        {
            var result = await client.PostASJson<Model.GetOptions.Result<Model.GetDateTime.Options>>(new
            {
                name = "camera.getOptions",
                parameters = new
                {
                    optionNames = new[]
                    {
                        "dateTimeZone"
                    }
                }
            });

            return DateTimeOffset.TryParseExact(
                    result?.results?.options?.dateTimeZone,
                    "yyyy:MM:dd HH:mm:sszzz",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.AssumeLocal,
                    out var dateTimeZone)
                ? dateTimeZone as DateTimeOffset?
                : null;
        }

        public static async Task SetDateTime(this ICameraClient client, DateTimeOffset dateTime)
        {
           

            var result = await client.PostASJson<Result>(new
            {
                name = "camera.setOptions",
                parameters = new
                {
                    options = new
                    {
                        dateTimeZone = dateTime.ToString("yyyy:MM:dd HH:mm:sszzz")
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
