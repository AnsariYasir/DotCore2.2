using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace C3Spectra.APPositioningApp.Entity
{
    public partial class CBSDVendorModel : BaseViewModel
    {
        public int CBSDVendorModelID { get; set; }
        public string Vendor { get; set; }
        public string Model { get; set; }
        public string SerialNumber { get; set; }
        public string SoftwareVersion { get; set; }
        public string FirmwareVersion { get; set; }
        public string HardwareVersion { get; set; }
        public string FCCID { get; set; }
        public string FrequencyLow { get; set; }
        public string FrequencyHigh { get; set; }
    }

    public partial class CBSDVendorModel
    {
        public List<SelectListItem> CBSDVendorModels { get; set; }
    }
}
