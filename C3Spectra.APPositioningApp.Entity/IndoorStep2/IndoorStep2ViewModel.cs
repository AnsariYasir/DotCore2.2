
using C3Spectra.APPositioningApp.Entity.APTypes;
using C3Spectra.APPositioningApp.Entity.Groups;
using System;
using System.Collections.Generic;
using System.Text;

namespace C3Spectra.APPositioningApp.Entity
{
    public class IndoorStep2ViewModel : BaseViewModel
    {
        public InstallationParametersViewModel InstallationParameters { get; set; }
        public OptionalParametersViewModel OptionalParameters { get; set; }
        public CBSDViewModel CBSD { get; set; }

        public APTypeViewModel APType { get; set; }
        public GroupViewModel Group { get; set; }
    }
}
