namespace OSC.Net.Model.GetOptions
{
    /// <summary>
    /// Get options result class.
    /// </summary>
    /// <typeparam name="T">The <see cref="Results&lt;T&gt;"/> options type.</typeparam>
    public class Result<T>
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
        /// Gets or sets the results from command execution.
        /// </summary>
        public Results<T> results { get; set; }
    }
}
