using System;
using System.Collections.Generic;
using System.Text;

namespace C3Spectra.APPositioningApp.Entity
{
    public class BaseViewModel
    {
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime LastModifiedOn { get; set; }
        public int LastModifiedBy { get; set; }
    }
}
