using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using C3Spectra.APPositioningApp.BAL;
using C3Spectra.APPositioningApp.Common;
using C3Spectra.APPositioningApp.Entity.User;
using C3Spectra.APPositioningApp.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace C3Spectra.APPositioningApp.Web.Controllers
{
    public class AccountController : Controller
    {

        CookieOptions option = new CookieOptions();
        #region Variable

        private readonly UserBAL _userBAL = null;
        Result result;
        private readonly EmailHelper _emailHepler = null;
        private readonly IHttpContextAccessor _httpContextAccessor;

        #endregion

        #region Constructor
        public AccountController(IHttpContextAccessor httpContextAccessor)

        {
            _userBAL = new UserBAL();
            result = new Result();
            _emailHepler = new EmailHelper();
            this._httpContextAccessor = httpContextAccessor;

        }


        #endregion


        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            //if(HttpContext.Session.GetString(Common.AppConstants.CURRENTNAME) != null)
            // {
            //     ViewBag.Username = HttpContext.Session.GetString(Common.AppConstants.CURRENTNAME);
            //     HttpContext.Session.SetString(Common.AppConstants.CURRENTNAME, string.Empty);
            // }
            //int value = 0;
            //value /= value;


            if (Request.Cookies["UserName"] != null)
            {
                ViewBag.Username = Request.Cookies["UserName"];
                HttpContext.Session.SetString(Common.AppConstants.CURRENTNAME, string.Empty);

            }
            else
            {
                HttpContext.Session.SetString(Common.AppConstants.CURRENTNAME, string.Empty);
            }

            return View();
        }





        [HttpPost]

        public IActionResult Login(UserViewModel model)
        {
            UserViewModel user = _userBAL.GetValidUser(model);
            ClaimsIdentity identity = null;
            bool isAuthenticated = false;
            if (user.UserID != 0)
            {
                if (model.Remember)
                {
                    Set("UserName", user.EmailAddress, 30);


                    // HttpContext.Session.SetComplexData(Common.AppConstants.CURRENTNAME, Username);
                }
                else
                {
                    Response.Cookies.Delete("UserName");
                }
                string Username = user.EmailAddress;
                HttpContext.Session.SetString(Common.AppConstants.CURRENTNAME, Username);
                HttpContext.Session.SetComplexData(Common.AppConstants.CURRENTUSER, user);
                //   Session[Common.AppConstants.CURRENTUSER] = user;

                identity = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name, user.UserID.ToString()),
                    new Claim(ClaimTypes.Role, "Admin")
                }, CookieAuthenticationDefaults.AuthenticationScheme);

                isAuthenticated = true;
                if (isAuthenticated)
                {
                    var principal = new ClaimsPrincipal(identity);

                    var login = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    //return RedirectToAction("Index", "Home");
                }
                return RedirectToAction("Dashboard", "Dashboard", new { user = model.EmailAddress });
            }
            else
            {
                //  ViewBag.msg = "Invalid email or passsword";
                ViewBag.Result = JsonConvert.SerializeObject(new Result { Status = Status.Success, Message = "Invalid email or passsword" });
                return View(model);
            }
            //  return RedirectToAction("Dashboard", new { user = model.EmailAddress });
        }

        public void Set(string key, string value, int? expireTime)
        {
            CookieOptions option = new CookieOptions();
            if (expireTime.HasValue)
                option.Expires = DateTime.Now.AddDays(Convert.ToDouble(expireTime));
            else
                option.Expires = DateTime.Now.AddDays(Convert.ToDouble(expireTime));
            Response.Cookies.Append(key, value, option);
        }
        public void Remove(string key)
        {
            Response.Cookies.Delete(key);
        }


        public IActionResult Home()
        {
            if (HttpContext.Session.GetComplexData<UserViewModel>(Common.AppConstants.CURRENTUSER) != null)
            {
                return RedirectToAction("Dashboard", "Home");
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ChangePassword(UserViewModel model)
        {
            try
            {
                UserViewModel currentUser = null;

                if (HttpContext.Session.GetComplexData<UserViewModel>(Common.AppConstants.CURRENTUSER) != null)
                {
                    // currentUser = (UserViewModel)Session[AppConstants.CURRENTUSER];
                    currentUser = HttpContext.Session.GetComplexData<UserViewModel>(Common.AppConstants.CURRENTUSER);
                    model.UserID = currentUser.UserID;
                    model.EmailAddress = currentUser.EmailAddress;
                }

                UserViewModel dbUser = _userBAL.GetValidUser(model);
                if (dbUser.UserID != 0)
                {
                    result = _userBAL.UpdatePassword(model);
                    //    ViewBag.msg = "Password updated successfully";
                    ViewBag.Result = JsonConvert.SerializeObject(new Result { Status = Status.Success, Message = "Password updated successfully" });
                    return View(model);
                }
                else
                {
                    //   ViewBag.msg = "Invalid User";
                    ViewBag.Result = JsonConvert.SerializeObject(new Result { Status = Status.Success, Message = "Invalid User" });
                    return View(model);
                }
            }
            catch (Exception e)
            {
                throw;
            }

        }

        public IActionResult Logout()
        {

            // HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ForgotPassword(UserViewModel model)
        {

            try
            {
                UserViewModel user = _userBAL.GetValidUserByEmail(model);
                if (user.UserID != 0)
                {
                    string autoPassword = autoGeneratePassword(user.password);
                    model.newpassword = autoPassword;
                    model.UserID = user.UserID;
                    result = _userBAL.UpdatePassword(model);
                    Helper.SendEmail(model.EmailAddress, model.newpassword);

                    //ViewBag.Result = JsonConvert.SerializeObject(new Result { Status = Status.Success, Message = "Password sent to your email address" });
                    //return View(model);
                    return RedirectToAction("ForgetPasswordSuccess");
                }
                else
                {
                    ViewBag.Result = JsonConvert.SerializeObject(new Result { Status = Status.Failure, Message = "Invalid email" });
                    return View(model);
                }
            }
            catch (Exception e)
            {
                throw;
            }


        }

        public IActionResult ForgetPasswordSuccess()
        {
            return View();
        }

        public string autoGeneratePassword(string password)
        {
            string allowedChars = "";

            allowedChars = "a,b,c,d,e,f,g,h,j,k,m,n,p,q,r,s,t,u,v,w,x,y,z,";

            allowedChars += "A,B,C,D,E,F,G,H,J,K,M,N,P,Q,R,S,T,U,V,W,X,Y,Z,";

            allowedChars += "1,2,3,4,5,6,7,8,9,@,#,$,%,&,?";

            char[] sep = { ',' };
            string[] arr = allowedChars.Split(sep);
            string passwordString = "";
            string temp = "";
            Random rand = new Random();
            for (int i = 0; i < password.Length; i++)
            {
                temp = arr[rand.Next(0, arr.Length)];
                passwordString += temp;
            }
            return passwordString;
        }

        public JsonResult GenerateExp()
        {
            int value = 0;
            value /= value;
            var sa = new JsonSerializerSettings();
            //TODO: currently replaced code for JSON Allow get. Need to be verified

            return Json(new { res = "test" }, sa);
        }



    }


}