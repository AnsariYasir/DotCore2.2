using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace C3Spectra.APPositioningApp.Entity
{
    public partial class InstallationParametersViewModel : BaseViewModel
    {
        public int InstallationParamterID { get; set; }

        [RegularExpression(@"^-?[0-9]+(.[0-9]{1,6})?$", ErrorMessage = "Please give proper lat")]
        //   [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:#.#}")]
        public double Latitude { get; set; }


        [RegularExpression(@"^-?[0-9]+(.[0-9]{1,6})?$", ErrorMessage = "Please give proper long")]
        //  [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:#.#}")]
        public double Longitude { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:#.#}")]
        [RegularExpression(@"^-?[0-9]+(.[0-9]{1,6})?$", ErrorMessage = "Please give height")]
        public decimal Height { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:#}")]
        //[Required(ErrorMessage ="Please Select Height type")]
        [Range(1, Int32.MaxValue, ErrorMessage = "Please Select Height type")]
        public int HeightTypeID { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:#}")]
        [Required(ErrorMessage = "Please give horizontal accuracy")]
        public int HorizontalAccuracy { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:#}")]
        [Required(ErrorMessage = "Please give vertical accuracy")]
        public int VerticalAccuracy { get; set; }

        public bool IndoorDeployment { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:#}")]

        public int AntennaAzimuth { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:#}")]
        //  [Required]
        public int AntennaGain { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:#}")]
        // [Required]
        public int AntennaDowntilt { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:#}")]
        [Required]
        public int EripCapability { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:#}")]
        [Required]
        public int AntennaBeamwidth { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:#}")]

        [Required(ErrorMessage = "Please select id")]
        [Range(1, Int32.MaxValue, ErrorMessage = "Please Select Height type")]
        public int AntennaModelID { get; set; }

        public bool IsSubmitted { get; set; }

        public int APID { get; set; }
    }


    public partial class InstallationParametersViewModel
    {
        public List<SelectListItem> HeightTypes { get; set; }
        public List<SelectListItem> AntennaModels { get; set; }
    }
}
