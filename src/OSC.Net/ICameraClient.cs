using System;
using System.Net.Http;

namespace OSC.Net
{
    /// <summary>
    /// Represents a Camera Client.
    /// </summary>
    public interface ICameraClient
    {
        /// <summary>
        /// Gets the camera end point.
        /// </summary>
        Uri EndPoint { get; }

        /// <summary>
        /// Gets a http client with camera base end point.
        /// </summary>
        /// <returns></returns>
        HttpClient GetHttpClient();
    }
}