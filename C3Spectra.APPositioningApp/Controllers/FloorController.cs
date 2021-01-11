using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using C3Spectra.APPositioningApp.BAL;
using C3Spectra.APPositioningApp.Common;
using C3Spectra.APPositioningApp.Entity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace C3Spectra.APPositioningApp.Web.Controllers
{
    public class FloorController : Controller
    {

        #region Variable

        private IHostingEnvironment _env;
        private readonly FloorBAL _FLOORBAL = null;
            private readonly BuildingBAL _buildingBAL = null;
            Result result;

            #endregion

            #region Constructor
            public FloorController(IHostingEnvironment env)

            {
                _FLOORBAL = new FloorBAL();
                _buildingBAL = new BuildingBAL();
                result = new Result();
            _env = env;
        }
            #endregion

            // GET: Floor
            public ActionResult FloorList(int? buildingID)
            {
                //List<FloorViewModel> model = _FLOORBAL.GetFloorsForIndoor();

                IndoorStep1ViewModel model = new IndoorStep1ViewModel();

                model.Floors = _FLOORBAL.GetFloorsByBuildingID(buildingID.Value);
                ViewBag.BuildingID = buildingID.Value;




                //TO DO: get building record by building ID and set building name in model
                model.BuildingName = _buildingBAL.GetBuildingByID(buildingID.Value).Name;

                return View(model);
            }


            public ActionResult ManageFloor(int? floorID, int? buildingID, string Name, string operation)
            {
                FloorViewModel model = null;


                if (floorID == null)
                {
                    model = new FloorViewModel();
                }
                else
                {
                    //get floor plan by ID

                    model = _FLOORBAL.GetFloorByID(floorID.Value);
                }
                if (model == null)
                {

                    ViewBag.msg = "No record found";
                    model = new FloorViewModel();
                }

                string message = string.Empty;
                if (operation != null)
                {
                    if (operation == AppConstants.ADDED)
                    {
                        message = "Floor added successfully.";
                    }
                    else if (operation == AppConstants.UPDATED)
                    {
                        message = "Floor updated successfully.";
                    }
                    ViewBag.Message = message;
                }

                model.Buildings = DropDownBinding.GetDropDown(AppConstants.USP_GETBUILDINGSFORDROPDOWN, 0, "SELECT Building");
                ViewBag.BuildingID = buildingID;

                return View(model);
            }

        //[HttpPost]
        //[AcceptVerbs(HttpVerbs.Post)]
        //public ActionResult ManageFloor(FloorViewModel model, FormCollection frm)
        //{
        //    string floorPlanSHPPath = "";
        //    string operation = string.Empty;
        //    bool fileUploadNeeded = false;
        //    bool isValid = true;
        //    if (string.IsNullOrEmpty(model.FloorPlanSHPPath))
        //    {
        //        fileUploadNeeded = true;


        //    }
        //    if (model.FloorPlan != null)
        //    {
        //        if (fileUploadNeeded)
        //        {
        //            if (model.FloorPlan.ContentLength == 0)
        //            {
        //                isValid = false;
        //                ViewBag.FileUploadValidation = "Please upload floor plan";
        //            }
        //            else
        //            {
        //                isValid = true;
        //                ViewBag.FileUploadValidation = "";
        //            }
        //        }
        //    }
        //    // ViewBag.FileUploadValidation

        //    if (ModelState.IsValid && isValid)
        //    {
        //        //1) Upload File
        //        bool res = true;
        //        if (model.FloorPlan != null)// model.FloorPlan.ContentLength > 0
        //        {
        //            //res = UploadFile(model.FloorPlan, out floorPlanSHPPath);
        //            model.FloorPlanSHPPath = floorPlanSHPPath;
        //            model.FloorPlanOrginalFileName = Path.GetFileNameWithoutExtension(model.FloorPlan.FileName);
        //        }

        //        if (model.FloorID == 0)//Add Floor record
        //        {
        //            //   if (res)
        //            //  {
        //            result = _FLOORBAL.AddFloor(model);
        //            model.FloorID = Convert.ToInt32(result.Values);
        //            operation = AppConstants.ADDED;
        //            //  }
        //            //else
        //            //{
        //            //    //Show user message about file upload failure
        //            //}
        //        }
        //        else
        //        {
        //            //Update
        //            //     if (res)
        //            //  {
        //            if (model.RemoveFloorPlan == true && model.FloorPlan == null)
        //            {
        //                model.FloorPlanSHPPath = "";
        //                model.FloorPlanOrginalFileName = "";
        //            }

        //            result = _FLOORBAL.UpdateFloor(model);
        //            operation = AppConstants.UPDATED;
        //            //      }
        //            // else
        //            // {
        //            //Show user message about file upload failure
        //            // }

        //        }
        //        //  model.Buildings = DropDownBinding.GetDropDown(AppConstants.USP_GETBUILDINGSFORDROPDOWN, 0, "SELECT Building");
        //        //  return View(model);
        //        return RedirectToAction("ManageFloor", new { floorID = model.FloorID, buildingID = Convert.ToInt32(frm["hdnBuildingID"]), operation = operation });
        //    }

        //    else
        //    {
        //        ViewBag.BuildingID = Convert.ToInt32(frm["hdnBuildingID"]);
        //        model.Buildings = DropDownBinding.GetDropDown(AppConstants.USP_GETBUILDINGSFORDROPDOWN, 0, "SELECT Building");
        //        return View(model);
        //    }
        //}

        public ActionResult DeleteFloor(int floorID)
            {
                result = _FLOORBAL.DeleteFloor(floorID);
            var sa = new JsonSerializerSettings();
            
            return Json(new { Result = true },sa);
            }


        #region Private Helper functions
        //private bool UploadFile(IFormFile file, out string floorPlanSHPPath)
        //{
        //    var webRoot = _env.WebRootPath;
        //    string uploads = System.IO.Path.Combine(webRoot, "ShapeFiles");
        //   // string uploads = Server.MapPath("~/ShapeFiles");
        //    //string uploads = 
        //    bool res;
            
        //    try
        //    {
        //        string generatedFileName = string.Concat(Path.GetFileNameWithoutExtension(file.FileName),
        //                                                               DateTime.Now.Ticks,
        //                                                               Path.GetExtension(file.FileName));

        //        file.SaveAs(
        //                                string.Concat(
        //                                                uploads,
        //                                                "//",
        //                                                generatedFileName
        //                                                )
        //                                );

        //        floorPlanSHPPath = string.Concat("~/",
        //                                                "ShapeFiles/",//TO DO : Get SHP storage filepath from Web.config file
        //                                                generatedFileName);

        //        res = true;

        //    }
        //    catch (Exception ex)
        //    {
        //        res = false;
        //        floorPlanSHPPath = "";
        //    }
        //    return res;
        //}
        #endregion


    }
}