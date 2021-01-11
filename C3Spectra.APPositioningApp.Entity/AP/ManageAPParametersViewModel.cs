using System;
using System.Collections.Generic;
using System.Text;

namespace C3Spectra.APPositioningApp.Entity
{
    public class ManageAPParametersViewModal : BaseViewModel
    {
        public InstallationParametersViewModel InstallationParameters { get; set; }
        public OptionalParametersViewModel OptionalParameters { get; set; }
        public CBSDViewModel CBSD { get; set; }
    }
}
