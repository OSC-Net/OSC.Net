namespace OSC.Net.Model
{
    /// <summary>
    /// Camera image capture modes enumeration.
    /// </summary>
    public enum CaptureMode
    {
        /// <summary>
        /// Still image mode.
        /// </summary>
        image,

        /// <summary>
        /// Video mode.
        /// </summary>
        video,

        /// <summary>
        /// Other (usually standby)
        /// </summary>
        _other,

        /// <summary>
        /// Unknown mode.
        /// </summary>
        unknown
    }
}
