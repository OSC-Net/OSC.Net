using System;
using System.Net;
using System.Net.Http;
using OSC.Net.Http;

namespace OSC.Net
{
    /// <summary>
    /// Standard implementation of <see cref="ICameraClient"/>.
    /// </summary>
    public class CameraClient : ICameraClient
    {
        private static HttpClient DefaultCreateClient(string name) => new HttpClient();
        /// <summary>
        /// The Default Camera Ip Address.
        /// </summary>
        /// <value>192.168.42.1</value>
        public const string DefaultIp = "192.168.42.1";

        /// <summary>
        /// The Default Camera Port.
        /// </summary>
        /// <value>80</value>
        public const int DefaultPort = 80;

        /// <summary>
        /// The Default Camera ip end point.
        /// </summary>
        /// <value>192.168.42.1:80</value>
        public static readonly IPEndPoint DefaultIpEndPoint = new IPEndPoint(IPAddress.Parse(DefaultIp), DefaultPort);

        private HttpClientFactoryHandler CreateClient { get; }

        /// <inheritdoc />
        public Uri EndPoint { get; }

        /// <inheritdoc />
        HttpClient ICameraClient.GetHttpClient()
        {
            var client = CreateClient(nameof(CameraClient));
            client.BaseAddress = EndPoint;
            return client;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CameraClient"/> class using <see cref="DefaultIpEndPoint"/>.
        /// </summary>
        /// <param name="createClient">Optional handler to override http client creation.</param>
        public CameraClient(HttpClientFactoryHandler createClient = null) : this(DefaultIpEndPoint, createClient)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CameraClient"/> class.
        /// </summary>
        /// <param name="ipEndPoint">The camera ip address and port.</param>
        /// <param name="createClient">Optional handler to override http client creation.</param>
        public CameraClient(IPEndPoint ipEndPoint, HttpClientFactoryHandler createClient = null) : this(new Uri($"http://{ipEndPoint}"), createClient)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CameraClient"/> class.
        /// </summary>
        /// <param name="endPoint">The camera http end point.</param>
        /// <param name="createClient">Optional handler to override http client creation.</param>
        public CameraClient(Uri endPoint, HttpClientFactoryHandler createClient = null)
        {
            EndPoint = endPoint;
            CreateClient = createClient ?? DefaultCreateClient;
        }
    }
}
