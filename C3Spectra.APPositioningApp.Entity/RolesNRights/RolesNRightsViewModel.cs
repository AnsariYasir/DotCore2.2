using System;
using System.Collections.Generic;
using System.Text;

namespace C3Spectra.APPositioningApp.Entity
{
    public class RolesNRightsViewModel : BaseViewModel
    {
        public int RNRID { get; set; }

        public int RoleID { get; set; }

        public int ActionID { get; set; }

        public string ActionName { get; set; }

        public string ControllerName { get; set; }

        public string MainMenuName { get; set; }

        public int MainMenuID { get; set; }

    }
}
