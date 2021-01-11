using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace C3Spectra.APPositioningApp.Entity
{
    public partial class CBSDViewModel : BaseViewModel
    {
        public int CBSDID { get; set; }
        [Range(1, Int32.MaxValue, ErrorMessage = "Please select VendorModel")]
        [Required]
        public int CBSDVendorModelID { get; set; }

        public string SoftwareVersion { get; set; }
        public string HardwareVersion { get; set; }
        public string FirmwareVersion { get; set; }

        public bool IsSubmitted { get; set; }
        public int APID { get; set; }
    }

    public partial class CBSDViewModel
    {
        public List<SelectListItem> CBSDVendorModels { get; set; }
    }
}
