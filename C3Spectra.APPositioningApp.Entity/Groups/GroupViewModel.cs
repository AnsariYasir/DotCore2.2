using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace C3Spectra.APPositioningApp.Entity.Groups
{
    public partial class GroupViewModel
    {
        public int GroupID { get; set; }
        public string GroupName { get; set; }
    }

    public partial class GroupViewModel
    {
        public List<SelectListItem> GroupNames { get; set; }

    }
}
