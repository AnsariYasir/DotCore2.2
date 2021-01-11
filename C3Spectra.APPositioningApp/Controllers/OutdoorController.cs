using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using C3Spectra.APPositioningApp.BAL;
using C3Spectra.APPositioningApp.Entity;
using C3Spectra.APPositioningApp.Entity.AP;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace C3Spectra.APPositioningApp.Web.Controllers
{
    public class OutdoorController : Controller
    {
        #region Variable
        private readonly APBAL _APBAL = null;
        #endregion
        #region Constructor
        public OutdoorController()

        {
            _APBAL = new APBAL();
        }
        #endregion

        // GET: Outdoor
        public ActionResult OutdoorStep1()
        {
            List<APViewModel> aps = _APBAL.GetAPByOutdoorMasterID(0);
            //if (aps[0].ImageSHPPath.ToString().Length > 0)
            //{
            //    HttpContext.Session.SetString("ImagePath", aps[0].ImageSHPPath.ToString());
            //}
            ViewBag.APs = aps;
            return View();
        }

        //public ActionResult GetAPSector(int? apId)
        //{
        //    List<APSectorViewModel> apsector = _APBAL.GetAPSectorByOutdoorAPID(apId);
        //    ViewBag.APSector = apsector;
        //    return View();
        //}
    }
}