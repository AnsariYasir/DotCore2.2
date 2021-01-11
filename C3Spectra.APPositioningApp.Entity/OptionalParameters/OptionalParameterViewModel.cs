using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace C3Spectra.APPositioningApp.Entity
{
    public partial class OptionalParametersViewModel : BaseViewModel
    {
        [ScaffoldColumn(false)]
        public int OptionalParameterID { get; set; }
        [DataType(DataType.Text)]
        [Range(1, Int32.MaxValue, ErrorMessage = "Please select callSigns")]
        public int CallSignID { get; set; }


        [Range(1, Int32.MaxValue, ErrorMessage = "Please select Cbsd Info")]
        public int CbsdInfoID { get; set; }


        [Range(1, Int32.MaxValue, ErrorMessage = "Please select Grouping Param")]
        public int GroupingParamID { get; set; }

        public bool IsSubmitted { get; set; }

        public int APID { get; set; }

    }

    public partial class OptionalParametersViewModel
    {
        [ScaffoldColumn(false)]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Please select callSigns")]
        public List<SelectListItem> CallSigns { get; set; }
        [Required(ErrorMessage = "Please select CbsdInfos")]
        public List<SelectListItem> CbsdInfos { get; set; }
        [Required(ErrorMessage = "Please select GroupingParams")]
        public List<SelectListItem> GroupingParams { get; set; }
    }
}
