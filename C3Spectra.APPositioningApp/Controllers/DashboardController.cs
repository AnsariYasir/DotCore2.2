using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using C3Spectra.APPositioningApp.Common;
using C3Spectra.APPositioningApp.Entity;
using C3Spectra.APPositioningApp.Entity.ManageDashboard;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace C3Spectra.APPositioningApp.Web.Controllers
{
    [Authorize(Roles = "Admin, User")]
    public class DashboardController : Controller
    {
        #region Constructor
        public DashboardController()
        {
        }
        #endregion

      
        public IActionResult Dashboard(string user)
        {


            ManageDashboardViewModel model = new ManageDashboardViewModel();
            model.Sas = new SASViewModel();
            model.CBSDVM = new CBSDVendorModel();

            model.Sas.SASNames = DropDownBinding.GetDropDown(AppConstants.USP_GETSAS, 0, "SELECT SAS");
            model.CBSDVM.CBSDVendorModels = DropDownBinding.GetDropDown(AppConstants.USP_GETCBSDVENDORMODELS, 0, "SELECT CBSD Vendor / Model");
            ViewBag.User = user;
            return View(model);

        }

        public IActionResult TestMap()
        {
          
            return View();
        }
        public void Dashboard1(int id)
        {

            HttpContext.Session.SetComplexData("sas", id);
            // return Json("");
        }

        public void Dashboard2(int id)
        {
            HttpContext.Session.SetComplexData("sas", id);

        }
    }

   
}