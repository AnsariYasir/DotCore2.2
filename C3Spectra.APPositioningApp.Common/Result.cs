using System;
using System.Collections.Generic;
using System.Text;

namespace C3Spectra.APPositioningApp.Common
{
    public class Result
    {
        public Status Status { get; set; }
        public string Message { get; set; }

        public string Values { get; set; }
       
        //public object ResultObj { get; set; }
    }

    public enum Status
    {
        Success = 1,
        Failure = 2
    };
}
