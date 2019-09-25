using System;
using System.Net.Http;
using System.Threading.Tasks;
using OSC.Net.Http;

namespace OSC.Net
{
    /// <summary>
    /// Contains <see cref="ICameraClient"/> commands extension methods.
    /// </summary>
    public static partial class Commands
    {
        private static Uri ExecuteUri { get; } = new Uri("osc/commands/execute", UriKind.Relative);
        private static Uri ExecuteStatusUri { get; } = new Uri("osc/commands/status", UriKind.Relative);

        private static async Task<TResult> PostAsJson<TResult>(this ICameraClient client, object value, Uri uri = null)
        {
            var response = await client.GetHttpClient().PostAsJsonAsync(
                uri ?? ExecuteUri,
                value
            );

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsAsync<TResult>();
            return result;
        }

        private static async Task<TStatusResult> GetStatus<TStatusResult>(this ICameraClient client, string id)
        {
            var result = await client.PostAsJson<TStatusResult>(
                uri: ExecuteStatusUri,
                value: new
                {
                    id
                });
            return result;
        }
    }
}
