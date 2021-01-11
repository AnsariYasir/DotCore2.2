using System;
using System.Collections.Generic;
using System.Text;

namespace C3Spectra.APPositioningApp.Entity
{
    public class RightsPerRole
    {
        public int RoleID { get; set; }
        public List<RolesNRightsViewModel> RolesNRightsList { get; set; }
    }
}
