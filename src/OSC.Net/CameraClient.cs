using System;
using System.Net;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;

namespace OSC.Net
{
    /// <summary>
    /// Standard implementation of <see cref="ICameraClient"/>.
    /// </summary>
    public class CameraClient : ICameraClient
    {
        private static readonly IHttpClientFactory HttpClientFactory;

        static CameraClient()
        {
            var serviceProvider = new ServiceCollection().AddHttpClient().BuildServiceProvider();
            HttpClientFactory = serviceProvider.GetService<IHttpClientFactory>();
        }

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

        /// <inheritdoc />
        public Uri EndPoint { get; }

        /// <inheritdoc />
        HttpClient ICameraClient.GetHttpClient()
        {
            var client = HttpClientFactory.CreateClient(nameof(CameraClient));
            client.BaseAddress = EndPoint;
            return client;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CameraClient"/> class using <see cref="DefaultIpEndPoint"/>.
        /// </summary>
        public CameraClient() : this(DefaultIpEndPoint)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CameraClient"/> class.
        /// </summary>
        /// <param name="ipEndPoint">The camera ip address and port.</param>
        public CameraClient(IPEndPoint ipEndPoint) : this(new Uri($"http://{ipEndPoint}"))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CameraClient"/> class.
        /// </summary>
        /// <param name="endPoint">The camera http end point.</param>
        public CameraClient(Uri endPoint)
        {
            EndPoint = endPoint;
        }
    }
}
