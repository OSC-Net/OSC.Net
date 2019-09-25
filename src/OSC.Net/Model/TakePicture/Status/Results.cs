namespace OSC.Net.Model.TakePicture.Status
{
    /// <summary>
    /// Take Picture Results class.
    /// </summary>
    public class Results
    {
        /// <summary>
        /// Gets or sets absolute uri to image.
        /// </summary>
        public string fileUrl { get; set; }

        /// <summary>
        /// Gets or sets relative uri to image.
        /// </summary>
        public string _localFileUrl { get; set; }
    }
}