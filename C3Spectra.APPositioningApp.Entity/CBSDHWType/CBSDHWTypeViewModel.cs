using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace C3Spectra.APPositioningApp.Entity
{
    public partial class CBSDHWTypeViewModel : BaseViewModel
    {

        public int CBSDHWTypeID { get; set; }
        public string CBSDHWType { get; set; }
    }

    public partial class CBSDHWTypeViewModel
    {
        public List<SelectListItem> CBSDHWTypes { get; set; }
    }
}
