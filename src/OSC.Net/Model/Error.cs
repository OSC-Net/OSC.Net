namespace OSC.Net.Model
{
    /// <summary>
    /// Command error class.
    /// </summary>
    public class Error
    {
        /// <summary>
        /// Gets or sets the error code.
        /// </summary>
        public string code { get; set; }
        
        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        public string message { get; set; }
    }
}