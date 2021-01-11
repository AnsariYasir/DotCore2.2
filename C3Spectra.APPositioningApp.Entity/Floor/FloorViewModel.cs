using C3Spectra.APPositioningApp.Common.CustomDataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace C3Spectra.APPositioningApp.Entity
{
    public partial class FloorViewModel : BaseViewModel
    {
        [ScaffoldColumn(false)]
        public int FloorID { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Please enter Floor name"), MaxLength(100)]
        [Display(Name = "Floor Name")]
        //[RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        public string FloorName { get; set; }

        //[Required(ErrorMessage = "Please enter description")]
        //[RegularExpression("^[a-zA-Z]*$", ErrorMessage = "Only Alphabets allowed.")]
        public string Description { get; set; }

        public string FloorPlanSHPPath { get; set; }

        public int BuildingID { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:#.#}")]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        public int FloorNo { get; set; }

        public string FloorPlanOrginalFileName { get; set; }

        [Required(ErrorMessage = "Please enter lat")]
        [RegularExpression(@"^-?[0-9]+(.[0-9]{1,6})?$", ErrorMessage = "Please give proper lat")]
        public double Latitude { get; set; }

        [Required(ErrorMessage = "Please enter long")]
        [RegularExpression(@"^-?[0-9]+(.[0-9]{1,6})?$", ErrorMessage = "Please give proper long")]
        public double Longitude { get; set; }

    }

    public partial class FloorViewModel
    {

        [ValidateShapeFileAttribute]
        //public HttpPostedFileBase FloorPlan { get; set; }
        public IFormFile FloorPlan { get; set; }

        public bool RemoveFloorPlan { get; set; }

        public List<SelectListItem> Buildings { get; set; }

        public string BuildingName { get; set; }
    }
}
