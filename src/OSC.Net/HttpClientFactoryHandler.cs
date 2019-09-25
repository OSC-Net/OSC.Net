using System.Net.Http;

namespace OSC.Net
{
    public delegate HttpClient HttpClientFactoryHandler(string name);
}