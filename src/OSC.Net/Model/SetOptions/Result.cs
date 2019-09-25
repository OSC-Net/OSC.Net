namespace OSC.Net.Model.SetOptions
{
    /// <summary>
    /// Set option result class.
    /// </summary>
    public class Result
    {
        /// <summary>
        /// Gets or sets the command name.
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Gets or sets the state of command execution.
        /// </summary>
        public string state { get; set; }
        
        /// <summary>
        /// Gets or sets the error as result of command execution.
        /// </summary>
        public Error error { get; set; }
    }
}
