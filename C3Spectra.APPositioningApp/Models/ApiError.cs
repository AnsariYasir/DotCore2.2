using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace C3Spectra.APPositioningApp.Web.Models
{
    public class ApiError
    {

        public string Message { get; set; }
        public string Detail { get; set; }
        public string InnerException { get; set; }

    }
}
