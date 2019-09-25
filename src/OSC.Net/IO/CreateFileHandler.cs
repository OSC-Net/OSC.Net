using System.IO;

namespace OSC.Net.IO
{
    /// <summary>
    /// Handler used to override file creation.
    /// </summary>
    /// <param name="path">Local file path.</param>
    /// <returns>Stream to file.</returns>
    public delegate Stream CreateFileHandler(string path);
}