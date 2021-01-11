using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace C3Spectra.APPositioningApp.Entity
{
    public class ManageRoleNRightsViewModel : BaseViewModel
    {
        public int RoleID { get; set; }
        public List<RolesNRightsViewModel> RolesNRightsViewMdoels { get; set; }

        public List<SelectListItem> RolesViewModels { get; set; }
    }
}
