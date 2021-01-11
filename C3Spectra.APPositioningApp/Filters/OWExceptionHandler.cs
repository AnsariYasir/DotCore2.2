using C3Spectra.APPositioningApp.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace C3Spectra.APPositioningApp.Web.Filters
{
    public class OWExceptionHandler : ActionFilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            Exception e = filterContext.Exception;

            string ControllerName = filterContext.RouteData.Values["controller"].ToString();//.Controller.ToString();
            string ActionName = filterContext.RouteData.Values["action"].ToString();


            //filterContext.Result = new ViewResult()
            //{
            //    ViewName = "SomeException"
            //};


            var isAjax = filterContext.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest";
            
                //filterContext.Result = new RedirectResult("~/Home/HandleError");
            if (isAjax)
            {
               // var result = new JsonResult();
                JsonResult result = new JsonResult(new { });
              

                result.Value = JsonConvert.SerializeObject(new Result { Status = Status.Failure, Message = "An Error Occured. Please try after some time." });
            }
            else
            {
                 var result = new ViewResult();
          
               result.ViewData["ErrorMessage"] = JsonConvert.SerializeObject(new Result { Status = Status.Failure, Message = "An Error Occured. Please try after some time." });
                filterContext.Result = result;
                filterContext.ExceptionHandled = true;
            }

            //TO DO: Error Logging Code

        }


    }
}
