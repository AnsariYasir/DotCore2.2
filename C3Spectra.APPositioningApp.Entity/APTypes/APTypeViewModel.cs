using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace C3Spectra.APPositioningApp.Entity.APTypes
{
    public partial class APTypeViewModel:BaseViewModel
    {
        public int APTypeID { get; set; }
        public string APTypeName { get; set; }
    }

    public partial class APTypeViewModel
    {
        public List<SelectListItem> APTypeNames { get; set; }

    }
}
