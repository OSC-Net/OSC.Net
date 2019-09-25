using System;
using System.IO;
using System.Net;
using System.Net.Http;
using OSC.Net.Http;
using OSC.Net.IO;

namespace OSC.Net
{
    /// <summary>
    /// Standard implementation of <see cref="ICameraClient"/>.
    /// </summary>
    public class CameraClient : ICameraClient
    {
        private static Stream DefaultCreateFile(string path)
            => File.Open(path, FileMode.Create, FileAccess.Write, FileShare.None);

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
        private CreateFileHandler CreateFile { get; }

        /// <inheritdoc />
        public Uri EndPoint { get; }

        /// <inheritdoc />
        HttpClient ICameraClient.GetHttpClient()
        {
            var client = CreateClient(nameof(CameraClient));
            client.BaseAddress = EndPoint;
            return client;
        }

        /// <inheritdoc />
        Stream ICameraClient.CreateFile(string path) => CreateFile(path);

        /// <summary>
        /// Initializes a new instance of the <see cref="CameraClient"/> class using <see cref="DefaultIpEndPoint"/>.
        /// </summary>
        /// <param name="createFile">Optional handler to override local file creation.</param>
        /// <param name="createClient">Optional handler to override http client creation.</param>
        public CameraClient(CreateFileHandler createFile = null, HttpClientFactoryHandler createClient = null) : this(DefaultIpEndPoint, createFile, createClient)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CameraClient"/> class.
        /// </summary>
        /// <param name="ipEndPoint">The camera ip address and port.</param>
        /// <param name="createFile">Optional handler to override local file creation.</param>
        /// <param name="createClient">Optional handler to override http client creation.</param>
        public CameraClient(IPEndPoint ipEndPoint, CreateFileHandler createFile = null, HttpClientFactoryHandler createClient = null) : this(new Uri($"http://{ipEndPoint}"), createFile, createClient)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CameraClient"/> class.
        /// </summary>
        /// <param name="endPoint">The camera http end point.</param>
        /// <param name="createFile">Optional handler to override local file creation.</param>
        /// <param name="createClient">Optional handler to override http client creation.</param>
        public CameraClient(Uri endPoint, CreateFileHandler createFile = null, HttpClientFactoryHandler createClient = null)
        {
            EndPoint = endPoint;
            CreateFile = createFile ?? DefaultCreateFile;
            CreateClient = createClient ?? DefaultCreateClient;
        }
    }
}
