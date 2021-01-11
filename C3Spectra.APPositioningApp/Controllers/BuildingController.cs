using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using C3Spectra.APPositioningApp.BAL;
using C3Spectra.APPositioningApp.Common;
using C3Spectra.APPositioningApp.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace C3Spectra.APPositioningApp.Web.Controllers
{
    public class BuildingController : Controller
    {
        #region Variable
        private readonly BuildingBAL _buildingBAL = null;
        private readonly UserBAL _userBAL = null;
        Result result;
        #endregion

        #region Constructor
        public BuildingController()

        {
            _buildingBAL = new BuildingBAL();
            _userBAL = new UserBAL();
            result = new Result();
        }
        #endregion

        public IActionResult ViewBuilding()
        {
            List<BuildingViewModel> model = _buildingBAL.GetBuildings();
            return View(model);
        }

        public ActionResult ManageBuilding(int? buildingID, string operation)
        {
            BuildingViewModel model = null;
            if (buildingID == null)
            {
                model = new BuildingViewModel();
            }
            else
            {
                //get floor plan by ID
                model = _buildingBAL.GetBuildingByID(buildingID.Value);
            }
            if (model == null)
            {
                ViewBag.msg = "No record found";
                model = new BuildingViewModel();
            }

            string message = string.Empty;
            if (operation != null)
            {
                if (operation == AppConstants.ADDED)
                {
                    message = "Building added successfully.";
                }
                else if (operation == AppConstants.UPDATED)
                {
                    message = "Building udpated successfully.";
                }
                ViewBag.Message = message;
            }

            return View(model);
        }
        [HttpPost]
        public ActionResult ManageBuilding(BuildingViewModel model, FormCollection frm)
        {
            string operation = string.Empty;
            if (model.BuildingId == 0)//Add Building record
            {
                result = _buildingBAL.AddBuilding(model);
                model.BuildingId = Convert.ToInt32(result.Values);
                operation = AppConstants.ADDED;
            }
            else
            {
                //Update
                result = _buildingBAL.UpdateBuilding(model);
                operation = AppConstants.UPDATED;
            }
            //return View(model);
            return RedirectToAction("ManageBuilding", new { buildingID = model.BuildingId, operation = operation });
        }



        public ActionResult DeleteBuilding(int buildingID)
        {
            result = _buildingBAL.DeleteBuilding(buildingID);
            var sa = new JsonSerializerSettings();
            return Json(new { Result = true },sa);
        }
    }
}