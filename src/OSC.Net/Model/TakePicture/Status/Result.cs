using System;
using System.Collections.Generic;
using System.Text;

namespace OSC.Net.Model.TakePicture.Status
{
    /// <summary>
    /// Take picture status class.
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
        /// Gets or sets command progress.
        /// </summary>
        public Progress progress { get; set; }
        
        /// <summary>
        /// Gets or sets results of command.
        /// </summary>
        public Results results { get; set; }
    }
}
