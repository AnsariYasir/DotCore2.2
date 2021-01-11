using C3Spectra.APPositioningApp.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace C3Spectra.APPositioningApp.Web.Filters
{
    public class ErrorHandlingFilter: ExceptionFilterAttribute
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public override void OnException(ExceptionContext filterContext)
        {
            var exception = filterContext.Exception;
            //log your exception here
            Exception e = filterContext.Exception;

            string ControllerName = filterContext.RouteData.Values["controller"].ToString();//.Controller.ToString();
            string ActionName = filterContext.RouteData.Values["action"].ToString();


            var isAjax = filterContext.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest";

            //filterContext.Result = new RedirectResult("~/Home/HandleError");
            if (isAjax)
            {
                // var result = new JsonResult();
                JsonResult result = new JsonResult(new { });
              

                result.Value = JsonConvert.SerializeObject(new Result { Status = Status.Failure, Message = "An Error Occured. Please try after some time." });
                filterContext.ExceptionHandled = true;
               
            }
            else
            {
                // ViewResult result = new ViewResult();

                var result = new ViewResult();

                string json = JsonConvert.SerializeObject(new Result { Status = Status.Failure, Message = "An Error Occured. Please try after some time.", Values="" });
                //var result = new JsonResult(json);
                //object o = JsonConvert.DeserializeObject(json);
                string json2 = JsonConvert.SerializeObject(json, Formatting.Indented);
              //  result.ViewData["ErrorMessage"] = "Test";

                filterContext.Result = result;
                filterContext.ExceptionHandled = true;
                logger.Debug("ERROR :" + ControllerName + ActionName);

                //var result = new ViewResult();

                //result.ViewBag.ErrorMessage = JsonConvert.SerializeObject(new Result { Status = Status.Failure, Message = "An Error Occured. Please try after some time." });
                //filterContext.Result = result;
                //filterContext.ExceptionHandled = true;

            }

        }
    }
}
