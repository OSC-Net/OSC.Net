using System;
using System.Collections.Generic;
using System.Text;

namespace OSC.Net.Model.TakePicture
{
    /// <summary>
    /// Take picture result class.
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
        /// Gets or sets the command id.
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// Gets or sets command progress.
        /// </summary>
        public Progress progress { get; set; }
    }
}
