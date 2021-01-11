using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace C3Spectra.APPositioningApp.Entity
{
    public partial class SASViewModel : BaseViewModel
    {
        public int SASID { get; set; }
        public string SASName { get; set; }
    }

    public partial class SASViewModel
    {
        public List<SelectListItem> SASNames { get; set; }

    }
}
