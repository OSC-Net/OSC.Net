using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using LitJson;

namespace OSC.Net
{
    internal static class HttpClientExtensions
    {
        public static async Task<HttpResponseMessage> PostAsJsonAsync(
            this HttpClient client,
            Uri uri,
            object value)
        {
            return await client.PostAsync(
                uri,
                new StringContent(
                    LitJson.JsonMapper.ToJson(value),
                    Encoding.UTF8,
                    "application/json"
                ));
        }

        public static async Task<TResult> ReadAsAsync<TResult>(this HttpContent content)
        {
            return JsonMapper.ToObject<TResult>(
                await content.ReadAsStringAsync());
        }
    }
}
