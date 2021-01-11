using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using C3Spectra.APPositioningApp.BAL;
using C3Spectra.APPositioningApp.Common;
using C3Spectra.APPositioningApp.Entity;
using C3Spectra.APPositioningApp.Entity.APTypes;
using C3Spectra.APPositioningApp.Entity.Groups;
using C3Spectra.APPositioningApp.Entity.IndoorStep1;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace C3Spectra.APPositioningApp.Web.Controllers
{
    public class IndoorController : Controller
    {
        #region Variable

        private readonly InstallationParametersBAL _installationParametersBAL = null;
        private readonly OptionalParametersBAL _optionalParametersBAL = null;
        private readonly CBSDBAL _CBSDBAL = null;
        private readonly FloorBAL _FLOORBAL = null;
        private readonly APBAL _APBAL = null;
        private readonly BuildingBAL _buildingBAL = null;
        private readonly UserBAL _userBAL = null;
        Result result;

        #endregion

        #region Constructor
        public IndoorController()

        {

            _installationParametersBAL = new InstallationParametersBAL();
            _optionalParametersBAL = new OptionalParametersBAL();
            _CBSDBAL = new CBSDBAL();
            _FLOORBAL = new FloorBAL();
            _APBAL = new APBAL();
            _buildingBAL = new BuildingBAL();
            _userBAL = new UserBAL();
            result = new Result();

        }
        #endregion
        // GET: Indoor

        #region Events
        public ActionResult IndoorStep2(int floorID, string Name)
        {
            //1)Get Floor record by Floor ID
            FloorViewModel floor = _FLOORBAL.GetFloorByID(floorID);
            List<APViewModel> aps = _APBAL.GetAPByFloorID(floorID);
            ViewBag.BuildName = Name;

            if (!string.IsNullOrEmpty(floor.FloorPlanSHPPath))
            {
                ViewBag.FileName = string.Concat("..", Url.Content(floor.FloorPlanSHPPath));

                //      will be used in Indoor/ReadJsonFile
                ViewBag.FloorPlanSHPPath = floor.FloorPlanSHPPath;
            }
            else
            {
                ViewBag.FileName = "";
            }

            IndoorStep2ViewModel model = new IndoorStep2ViewModel();
            ViewBag.APs = aps;

            model.InstallationParameters = new InstallationParametersViewModel();
            model.OptionalParameters = new OptionalParametersViewModel();
            model.CBSD = new CBSDViewModel();

            ViewBag.BuildingID = floor.BuildingID;

            return View(model);
        }



        public ActionResult IndoorStep(int floorID)
        {

         

            FloorViewModel floor = _FLOORBAL.GetFloorByID(23);
            List<APViewModel> aps = _APBAL.GetAPByFloorID(23);
          //  ViewBag.BuildName = Name;

            if (!string.IsNullOrEmpty(floor.FloorPlanSHPPath))
            {
                ViewBag.FileName = string.Concat("..", Url.Content("~/ShapeFiles/Columbus Circle - First Floor Lines636984586978583967637001940100847321.json"));

                //      will be used in Indoor/ReadJsonFile
                ViewBag.FloorPlanSHPPath = "~/ShapeFiles/Columbus Circle - First Floor Lines636984586978583967637001940100847321.json";
            }
            else
            {
                ViewBag.FileName = "";
            }

            IndoorStep2ViewModel model = new IndoorStep2ViewModel();
            ViewBag.APs = aps;

            model.InstallationParameters = new InstallationParametersViewModel();
            model.OptionalParameters = new OptionalParametersViewModel();
            model.CBSD = new CBSDViewModel();
            model.APType = new APTypeViewModel();
            model.Group = new GroupViewModel();
            model.APType.APTypeNames = DropDownBinding.GetDropDown(AppConstants.USP_GETAPTypes, 0, "AP Type");
            model.Group.GroupNames = DropDownBinding.GetDropDown(AppConstants.USP_GETGROUPS, 0, "Group");

            ViewBag.BuildingID = floor.BuildingID;
            ViewBag.FloorID = 23;

            return View(model);
        }
        //Commented to solve the code

        public JsonResult ReadJsonFile(string path)
        {
            if (path == null)
            {
                throw new ArgumentNullException("path");
            }
            else
            {
                string body = string.Empty;
               string absolutePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\ShapeFiles", "Columbus Circle - First Floor Lines636984586978583967637001940100847321.json");
               // string absolutePath = @"E:\Workspace-GIT\CPI-Pro\C3Spectra.APPositioningApp\C3Spectra.APPositioningApp\ShapeFiles";

                using (StreamReader reader = new StreamReader(absolutePath))
                {
                    body = reader.ReadToEnd();
                }
                var sa = new JsonSerializerSettings();
                return Json(new { Result = true, json = body }, sa);
            }

        }
        #endregion
    }
}