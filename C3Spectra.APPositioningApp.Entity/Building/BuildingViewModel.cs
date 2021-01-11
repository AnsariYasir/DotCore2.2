using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace C3Spectra.APPositioningApp.Entity
{
    public class BuildingViewModel : BaseViewModel
    {
        [ScaffoldColumn(false)]
        public int BuildingId { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Please enter building name"), MaxLength(100)]
        public string Name { get; set; }

        //[Required(ErrorMessage = "Please enter description")]
        public string Description { get; set; }

    }
}
