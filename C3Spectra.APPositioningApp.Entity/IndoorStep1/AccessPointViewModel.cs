using C3Spectra.APPositioningApp.Entity.APTypes;
using C3Spectra.APPositioningApp.Entity.Groups;
using System;
using System.Collections.Generic;
using System.Text;

namespace C3Spectra.APPositioningApp.Entity.IndoorStep1
{
   public class AccessPointViewModel
    {
        public APTypeViewModel APType { get; set; }
        public GroupViewModel Group { get; set; }
    }
}
