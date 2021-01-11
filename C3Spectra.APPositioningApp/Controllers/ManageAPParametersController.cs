using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using C3Spectra.APPositioningApp.BAL;
using C3Spectra.APPositioningApp.Common;
using C3Spectra.APPositioningApp.Entity;
using C3Spectra.APPositioningApp.Entity.AP;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace C3Spectra.APPositioningApp.Web.Controllers
{
    public class ManageAPParametersController : Controller
    {
        #region Variable

        private readonly InstallationParametersBAL _installationParametersBAL = null;
        private readonly OptionalParametersBAL _optionalParametersBAL = null;
        private readonly CBSDBAL _CBSDBAL = null;
        private readonly APBAL _APBAL = null;


        Result result;

        #endregion

        #region Constructor
        public ManageAPParametersController()

        {

            _installationParametersBAL = new InstallationParametersBAL();
            _optionalParametersBAL = new OptionalParametersBAL();

            _APBAL = new APBAL();
            _CBSDBAL = new CBSDBAL();
            result = new Result();

        }
        #endregion
        // GET: ManageAPParameters
        public IActionResult ManageAPParameters(int? apid, double? lat, double? lng, int? floorID, string section, string operation)
        {
            ManageAPParametersViewModal model = new ManageAPParametersViewModal();
            if (floorID != 0 && floorID != null)
            {
                ViewBag.floorID = floorID;
            }
            else
            {
                ViewBag.floorID = 0;
            }

            if (apid != null)
            {
                var installationParameters = _installationParametersBAL.GetInstallationParemetersByAPID(apid.Value);
                if (installationParameters != null)
                    model.InstallationParameters = installationParameters;
                else
                {
                    model.InstallationParameters = new InstallationParametersViewModel();
                    if (lat != null)
                        model.InstallationParameters.Latitude = lat.Value;
                    if (lng != null)
                        model.InstallationParameters.Longitude = lng.Value;
                }

                model.InstallationParameters.APID = apid.Value;


                model.InstallationParameters.HeightTypes = DropDownBinding.GetDropDown(AppConstants.USP_GETHEIGHTTYPES, 0, "SELECT Height Type");
                model.InstallationParameters.AntennaModels = DropDownBinding.GetDropDown(AppConstants.USP_GETANTENNAMODELS, 0, "SELECT Antenna Model");

                var optionalParameters = _optionalParametersBAL.GetOptionalParemetersByAPID(apid.Value);
                if (optionalParameters != null)
                    model.OptionalParameters = optionalParameters;
                else
                {
                    model.OptionalParameters = new OptionalParametersViewModel();
                }
                model.OptionalParameters.APID = apid.Value;

                model.OptionalParameters.CallSigns = DropDownBinding.GetDropDown(AppConstants.USP_GETCALLSIGNS, 0, "SELECT Call Sign");
                model.OptionalParameters.CbsdInfos = DropDownBinding.GetDropDown(AppConstants.USP_GETCBSDINFOS, 0, "SELECT Cbsd Info");
                model.OptionalParameters.GroupingParams = DropDownBinding.GetDropDown(AppConstants.USP_GETGROUPINGPARAMS, 0, "SELECT GroupingParam");


                var cbsd = _CBSDBAL.GetCBSDByAPID(apid.Value);
                if (cbsd != null)
                    model.CBSD = cbsd;
                else
                {
                    model.CBSD = new CBSDViewModel();
                }
                model.CBSD.APID = apid.Value;


                model.CBSD.CBSDVendorModels = DropDownBinding.GetDropDown(AppConstants.USP_GETCBSDVENDORMODELS, 0, "SELECT CBSDVendorModels");

            }

            string message = string.Empty;

            if (section != null && operation != null)
            {
                if (section == AppConstants.InstallationParameters)
                {
                    message = "Installation Parameters ";
                }
                else if (section == AppConstants.OptionalParameters)
                {
                    message = "Optional Parameters ";
                }
                else if (section == AppConstants.CBSD)
                {
                    message = "CBSD ";
                }

                if (operation == AppConstants.SaveAsDraft)
                {
                    message += "Saved as Draft successfully";
                }
                else if (operation == AppConstants.Submit)
                {
                    message += "Submitted successfully";
                }

                ViewBag.Message = message;
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult ManageAPParameters(ManageAPParametersViewModal model, FormCollection frm)
        {
            bool isSubmitted = false;
            string section = string.Empty;
            string operation = string.Empty;
            int id = 0;

            try
            {

                //if (model.InstallationParameters != null)
                //{
                if (frm["btnIPSaveAsDraft"].ToString() != null)
                {
                    isSubmitted = false;
                    model.InstallationParameters.IsSubmitted = isSubmitted;
                    if (model.InstallationParameters.InstallationParamterID == 0)
                    {
                        //insert
                        result = _installationParametersBAL.AddInstallationParameters(model.InstallationParameters);

                        section = AppConstants.InstallationParameters;
                        operation = AppConstants.SaveAsDraft;
                        id = model.InstallationParameters.APID;
                    }
                    else
                    {
                        //update
                        result = _installationParametersBAL.UpdateInstallationParameters(model.InstallationParameters);
                        section = AppConstants.InstallationParameters;
                        operation = AppConstants.SaveAsDraft;
                        id = model.InstallationParameters.APID;
                    }
                }
                else if (frm["btnIPSubmit"].ToString() != null)
                {
                    isSubmitted = true;
                    model.InstallationParameters.IsSubmitted = isSubmitted;
                    if (model.InstallationParameters.InstallationParamterID == 0)
                    {
                        //insert
                        result = _installationParametersBAL.AddInstallationParameters(model.InstallationParameters);
                        section = AppConstants.InstallationParameters;
                        operation = AppConstants.Submit;
                        id = model.InstallationParameters.APID;
                    }
                    else
                    {
                        //update
                        result = _installationParametersBAL.UpdateInstallationParameters(model.InstallationParameters);
                        section = AppConstants.InstallationParameters;
                        operation = AppConstants.Submit;
                        id = model.InstallationParameters.APID;
                    }
                }
                //}
                //    if (model.OptionalParameters != null)
                //{
                else if (frm["btnOPSaveAsDraft"].ToString() != null)
                {
                    isSubmitted = false;
                    model.OptionalParameters.IsSubmitted = isSubmitted;

                    if (model.OptionalParameters.OptionalParameterID == 0)
                    {
                        result = _optionalParametersBAL.AddOptionalParameters(model.OptionalParameters);
                        section = AppConstants.OptionalParameters;
                        operation = AppConstants.SaveAsDraft;
                        id = model.OptionalParameters.APID;
                    }
                    else
                    {
                        result = _optionalParametersBAL.UpdateOptionalParameters(model.OptionalParameters);
                        section = AppConstants.OptionalParameters;
                        operation = AppConstants.SaveAsDraft;
                        id = model.OptionalParameters.APID;
                    }
                }
                else if (frm["btnOPSubmit"].ToString() != null)
                {
                    isSubmitted = true;
                    if (model.OptionalParameters.OptionalParameterID == 0)
                    {
                        result = _optionalParametersBAL.AddOptionalParameters(model.OptionalParameters);
                        section = AppConstants.OptionalParameters;
                        operation = AppConstants.Submit;
                        id = model.OptionalParameters.APID;
                    }
                    else
                    {
                        result = _optionalParametersBAL.UpdateOptionalParameters(model.OptionalParameters);
                        section = AppConstants.OptionalParameters;
                        operation = AppConstants.Submit;
                        id = model.OptionalParameters.APID;
                    }
                }
                //}
                //if (model.CBSD != null)
                //{

                else if (frm["btnCBSDSaveAsDraft"].ToString() != null)
                {
                    isSubmitted = false;
                    model.CBSD.IsSubmitted = isSubmitted;

                    if (model.CBSD.CBSDID == 0)
                    {
                        //insert
                        result = _CBSDBAL.AddCBSD(model.CBSD);
                        section = AppConstants.CBSD;
                        operation = AppConstants.SaveAsDraft;
                        id = model.CBSD.APID;
                    }
                    else
                    {
                        //update
                        result = _CBSDBAL.UpdateCBSD(model.CBSD);
                        section = AppConstants.CBSD;
                        operation = AppConstants.SaveAsDraft;
                        id = model.CBSD.APID;
                    }
                }
                else if (frm["btnCBSDSubmit"].ToString() != null)
                {
                    isSubmitted = true;
                    model.CBSD.IsSubmitted = isSubmitted;

                    if (model.CBSD.CBSDID == 0)
                    {
                        //insert
                        result = _CBSDBAL.AddCBSD(model.CBSD);
                        section = AppConstants.CBSD;
                        operation = AppConstants.Submit;
                        id = model.CBSD.APID;
                    }
                    else
                    {
                        //update
                        result = _CBSDBAL.UpdateCBSD(model.CBSD);
                        section = AppConstants.CBSD;
                        operation = AppConstants.Submit;
                        id = model.CBSD.APID;
                    }
                }
                //}


                return RedirectToAction("ManageAPParameters", new { apid = id, section = section, operation = operation });
            }
            catch (Exception)
            {

                throw;

            }

        }

        public ActionResult ManageAPNew(
          int apid,
          double newlat,
          double newlong,
          string name,
          string desc,
          int floorID
          )
        {

            APViewModel model = new APViewModel();
            model.APID = apid;
            model.Lat = newlat;
            model.Long = newlong;
            model.Name = name;
            model.Description = desc;
            model.FloorID = floorID;
            if (model.APID == 0)
            {
                //insert
                result = _APBAL.AddAP(model);
                model.APID = Convert.ToInt32(result.Values);
            }
            else
            {
                //update
                result = _APBAL.UpdateAP(model);
            }
            var sa = new JsonSerializerSettings();
            //   return Json(new { Result = true, APID = model.APID }, JsonRequestBehavior.AllowGet);
            return Json(new { Result = true, APID = model.APID }, sa);

        }

        public ActionResult ManageAPForOutdoor(
               )
        {

            string ImageSHPPath = "";
            APViewModel model = new APViewModel();

            var files = Request.Form.Files;
            if (files.Count > 0)
            {
                model.Image = files[0];
            }
            
            model.APID = Convert.ToInt32(Request.Form.Where(x => x.Key == "apid").FirstOrDefault().Value);
            model.Lat = Convert.ToDouble(Request.Form.Where(x => x.Key == "newlat").FirstOrDefault().Value);
            model.Long = Convert.ToDouble(Request.Form.Where(x => x.Key == "newlong").FirstOrDefault().Value);
            model.Name = Request.Form.Where(x => x.Key == "name").FirstOrDefault().Value;
            model.Description = Request.Form.Where(x => x.Key == "desc").FirstOrDefault().Value;

            bool res = true;

            if (files.Count > 0)
            {
                res = UploadFile(model.Image, out ImageSHPPath);
                model.ImageSHPPath = ImageSHPPath;
            }
            else
            {
                model.ImageSHPPath = Request.Form.Where(x => x.Key == "ImageSHPPath").FirstOrDefault().Value; ;
                //if (HttpContext.Session.GetString("ImagePath") != null)
                //{
                //    model.ImageSHPPath = HttpContext.Session.GetString("ImagePath");
                //}
                //else
                //{
                //    return Json(new { Result = false, APID = model.APID }, new JsonSerializerSettings());
                //}
            }


            if (model.APID == 0)
            {
                //insert
                result = _APBAL.AddAP(model, true);
                model.APID = Convert.ToInt32(result.Values);
            }
            else
            {
                //update    
                result = _APBAL.UpdateAP(model, true);
            }





            //  return Json(new { Result = true, APID = model.APID }, JsonRequestBehavior.AllowGet);
            var sa = new JsonSerializerSettings();
            //   return Json(new { Result = true, APID = model.APID }, JsonRequestBehavior.AllowGet);
            return Json(new { Result = true, APID = model.APID }, sa);

        }

        public ActionResult DeleteManageAPNew(int apID)
        {
            result = _APBAL.DeleteAPSector(apID);
            result = _APBAL.DeleteAP(apID);

            var sa = new JsonSerializerSettings();
            // return Json(new { Result = true }, JsonRequestBehavior.AllowGet);
            return Json(new { Result = true }, sa);
        }

        public ActionResult ReduceManageAPNew(int apID,string apSectorId)
        {
            result = _APBAL.ReduceAPSector(apID,apSectorId);
            var sa = new JsonSerializerSettings();
            return Json(new { Result = true }, sa);
        }

        [HttpPost]
        public ActionResult ManageAPSectorNew(
          int apsectorid,
          int apid,
          int sectornumber,
          string sectorvalue,
          string serialnumber
          )
        {

            APSectorViewModel model = new APSectorViewModel();
            model.APSectorId = apsectorid;
            model.APId = apid;
            model.SectorNumber = sectornumber;
            model.SectorValue = sectorvalue;
            model.SerialNumber = serialnumber;
            if (model.APSectorId == 0)
            {
                //insert
                result = _APBAL.AddAPSector(model, true);
                model.APSectorId = Convert.ToInt32(result.Values);
            }
            else
            {
                //update
                result = _APBAL.UpdateAPSector(model, true);
            }
            var sa = new JsonSerializerSettings();
            //   return Json(new { Result = true, APID = model.APID }, JsonRequestBehavior.AllowGet);
            return Json(new { Result = true, APSectorId = model.APSectorId }, sa);

        }


        //private bool UploadFile(IFormFile file, out string ImageSHPPath)
        //{
        //    var fileName = Path.GetFileName(file.FileName);
        //    bool res = false;
        //    var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\APImages", fileName);
        //    try
        //    {
        //        if (file != null)
        //        {




        //            string generatedFileName = string.Concat(Path.GetFileNameWithoutExtension(file.FileName),
        //                                                                     DateTime.Now.Ticks,
        //                                                                      Path.GetExtension(file.FileName));






        //            using (var fileStream = new FileStream(filePath, FileMode.Create))
        //            {



        //                file.CopyToAsync(fileStream);



        //            }

        //            res = true;
        //        }
        //        ImageSHPPath = string.Concat("~/",
        //                                                "APImages/",//TO DO : Get SHP storage filepath from Web.config file
        //                                                  fileName);
        //    }
        //    catch (Exception ex)
        //    {
        //        res = false;
        //        ImageSHPPath = "";
        //    }



        //    return res;
        //}

        private bool UploadFile(IFormFile file, out string ImageSHPPath)
        {
            //var fileName = Path.GetFileName(file.FileName);
            bool res = false;
            string generatedFileName = string.Empty;
            //var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\APImages", fileName);
            try
            {
                if (file != null)
                {
                    generatedFileName = string.Concat(Path.GetFileNameWithoutExtension(file.FileName),
                                                                             DateTime.Now.Ticks,
                                                                              Path.GetExtension(file.FileName));
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\APImages", generatedFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyToAsync(fileStream);
                    }
                    res = true;
                }
                //ImageSHPPath = string.Concat("~/","APImages/",fileName); //TO DO : Get SHP storage filepath from Web.config file
                ImageSHPPath = string.Concat("~/", "APImages/", generatedFileName);
            }
            catch (Exception ex)
            {
                res = false;
                ImageSHPPath = "";
            }

            return res;
        }
    }
}