using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using C3Spectra.APPositioningApp.BAL;
using C3Spectra.APPositioningApp.Common;
using C3Spectra.APPositioningApp.Entity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace C3Spectra.APPositioningApp.Web.Controllers
{
    public class RoleNRightManagementController : Controller
    {
        #region Variable
        private readonly MainMenuBAL _mainMenuBAL = null;
        private readonly RolesBAL _roleBAL = null;
        private readonly RolesNRightsBAL _rolesNRightsBAL = null;
        private readonly ManageRoleNRightsViewModel _manageRoleNRightsViewModel = null;
        Result result;
        #endregion

        #region Constructor
        public RoleNRightManagementController()
        {
            _mainMenuBAL = new MainMenuBAL();
            _roleBAL = new RolesBAL();
            _rolesNRightsBAL = new RolesNRightsBAL();
            _manageRoleNRightsViewModel = new ManageRoleNRightsViewModel();
            result = new Result();
        }

        #endregion
        // GET: RoleNRightManagement
        public ActionResult Index()
        {
            return View();
        }

        #region Main Menu
        public ActionResult MainMenuList()
        {
            List<MainMenuViewModel> model = _mainMenuBAL.GetMainMenus();
            return View(model);
        }

        public ActionResult ManageMainMenu(int? mainMenuID, string operation)
        {
            MainMenuViewModel model = null;
            if (mainMenuID == null)
            {
                model = new MainMenuViewModel();
            }
            else
            {
                //get Main Menu by ID
                model = _mainMenuBAL.GetMainMenuByID(mainMenuID.Value);
            }
            if (model == null)
            {
                ViewBag.msg = "No record found";
                model = new MainMenuViewModel();
            }
            string message = string.Empty;
            if (operation != null)
            {
                if (operation == AppConstants.ADDED)
                {
                    message = "Sections added successfully.";
                }
                else if (operation == AppConstants.UPDATED)
                {
                    message = "Sections updated successfully.";
                }
                ViewBag.Message = message;
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult ManageMainMenu(MainMenuViewModel model)
        {
            string operation = string.Empty;
            if (model.MainMenuId == 0)//Add Main Menu record
            {
                result = _mainMenuBAL.AddMainMenu(model);
                model.MainMenuId = Convert.ToInt32(result.Values);
                operation = AppConstants.ADDED;
            }
            else
            {
                //Update
                result = _mainMenuBAL.UpdateMainMenu(model);
                operation = AppConstants.UPDATED;
            }

            return RedirectToAction("MainMenuList");
        }

        public ActionResult DeleteMainMenu(int mainmenuID)
        {
            result = _mainMenuBAL.DeleteMainMenu(mainmenuID);
            var sa = new JsonSerializerSettings();
         //   return Json(new { Result = true }, JsonRequestBehavior.AllowGet);
            return Json(new { Result = true }, sa);
        }

        #endregion

        #region Role
        public ActionResult RoleList()
        {
            List<RolesViewModel> model = _roleBAL.GetRoles();
            return View(model);
        }
        public ActionResult ManageRole(int? roleID, string operation)
        {
            RolesViewModel model = null;
            if (roleID == null)
            {
                model = new RolesViewModel();
            }
            else
            {
                //get floor plan by ID
                model = _roleBAL.GetRoleByID(roleID.Value);
            }
            if (model == null)
            {
                ViewBag.msg = "No record found";
                model = new RolesViewModel();
            }
            string message = string.Empty;
            if (operation != null)
            {
                if (operation == AppConstants.ADDED)
                {
                    message = "Sections added successfully.";
                }
                else if (operation == AppConstants.UPDATED)
                {
                    message = "Sections updated successfully.";
                }
                ViewBag.Message = message;
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult ManageRole(RolesViewModel model)
        {
            string operation = string.Empty;
            if (model.RolesID == 0)
            {
                result = _roleBAL.AddRole(model);
                model.RolesID = Convert.ToInt32(result.Values);
                operation = AppConstants.ADDED;
            }
            else
            {
                //Update
                result = _roleBAL.UpdateRole(model);
                operation = AppConstants.UPDATED;
            }
            return RedirectToAction("RoleList");
        }

        public ActionResult DeleteRole(int roleID)
        {
            result = _roleBAL.DeleteRole(roleID);
            var sa = new JsonSerializerSettings();
            //   return Json(new { Result = true }, JsonRequestBehavior.AllowGet);
            return Json(new { Result = true },sa);
        }
        #endregion

        #region Role And Rights
        public ActionResult ManageRoleNRights(int? RoleID)
        {
            _manageRoleNRightsViewModel.RolesNRightsViewMdoels = _rolesNRightsBAL.GetRolesNRights(RoleID);
            _manageRoleNRightsViewModel.RolesViewModels = DropDownBinding.GetDropDown(AppConstants.USP_GETROLESFORDROPDOWN, 0, "SELECT Role");
            return View(_manageRoleNRightsViewModel);
        }
        [HttpPost]
        public ActionResult SaveRoleNRights(ManageRoleNRightsViewModel manageRoleNRightsViewModel)
        {
            result = _rolesNRightsBAL.SaveRoleNRights(manageRoleNRightsViewModel);
            return RedirectToAction("ManageRoleNRights", new { RoleID = manageRoleNRightsViewModel.RoleID });
        }
        #endregion

    }
}