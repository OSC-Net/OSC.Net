using System;
using System.Collections.Generic;
using System.Text;

namespace OSC.Net.Model.TakePicture.Status
{

    public class Result
    {
        public string name { get; set; }
        public string state { get; set; }
        public Progress progress { get; set; }
        public Results results { get; set; }
    }
}
