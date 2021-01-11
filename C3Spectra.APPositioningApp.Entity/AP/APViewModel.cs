using C3Spectra.APPositioningApp.Entity.AP;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace C3Spectra.APPositioningApp.Entity
{
    //public class APViewModel : BaseViewModel
    //{
    //    public int APID { get; set; }
    //    public string Name { get; set; }
    //    public double Lat { get; set; }
    //    public double Long { get; set; }
    //    public string Description { get; set; }
    //    public int FloorID { get; set; }
    //    public bool IsSubmitted { get; set; }
    //}
    public partial class APViewModel : BaseViewModel
    {
        public int APID { get; set; }
        public string Name { get; set; }
        public double Lat { get; set; }
        public double Long { get; set; }
        public string Description { get; set; }
        public int FloorID { get; set; }

        public int APTypeID { get; set; }

        public int GroupID { get; set; }

        public string SerialNo { get; set; }
        public bool IsIndoor { get; set; }
        public string ImageSHPPath { get; set; }



        public bool IsSubmitted { get; set; }

        public List<APSectorViewModel> listAPSector = new List<APSectorViewModel>();

    }



    public partial class APViewModel
    {
        public IFormFile Image { get; set; }
    }
}
