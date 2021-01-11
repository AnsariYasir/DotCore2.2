
using System;
using System.Collections.Generic;
using System.Text;

namespace C3Spectra.APPositioningApp.Entity
{
    public class IndoorStep1ViewModel : BaseViewModel
    {
        public List<FloorViewModel> Floors { get; set; }
        public string BuildingName { get; set; }
    }
}
