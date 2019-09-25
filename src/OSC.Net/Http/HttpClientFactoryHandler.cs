using System.Net.Http;

namespace OSC.Net.Http
{
    /// <summary>
    /// Handler used to obtain a new <see cref="HttpClient" />
    /// </summary>
    /// <param name="name">The name of the client.</param>
    /// <returns><see cref="HttpClient" /></returns>
    public delegate HttpClient HttpClientFactoryHandler(string name);
}