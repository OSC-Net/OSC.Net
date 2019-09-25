using System;
using System.Globalization;
using System.Threading.Tasks;
using OSC.Net.Model.SetOptions;

namespace OSC.Net
{
    public static partial class Commands
    {
        /// <summary>
        /// Gets camera current date and time.
        /// </summary>
        /// <param name="client">The <see cref="ICameraClient"/> method extends.</param>
        /// <returns>Camera current <see cref="DateTimeOffset"/>, <c>null</c> if failed to parse date.</returns>
        public static async Task<DateTimeOffset?> GetDateTime(this ICameraClient client)
        {
            var result = await client.PostAsJson<Model.GetOptions.Result<Model.GetDateTime.Options>>(new
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

        /// <summary>
        /// Sets camera current date and time.
        /// </summary>
        /// <param name="client">The <see cref="ICameraClient"/> method extends.</param>
        /// <param name="dateTime">The <see cref="DateTimeOffset"/> to set.</param>
        public static async Task SetDateTime(this ICameraClient client, DateTimeOffset dateTime)
        {
           

            var result = await client.PostAsJson<Result>(new
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
