namespace OSC.Net.Model.GetOptions
{
    /// <summary>
    /// Get Options Results class.
    /// </summary>
    public class Results<T>
    {
        /// <summary>
        /// Options returned.
        /// </summary>
        public T options { get; set; }
    }
}