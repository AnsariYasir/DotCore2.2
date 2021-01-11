using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace C3Spectra.APPositioningApp.Web.Controllers
{
    public class ErrorsController : Controller
    {
        [Route("Error/500")]
        public void Error500(ExceptionHandlerFeature filter)
        {
            var exceptionFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            if (exceptionFeature != null)
            {
             
                
                ViewBag.ErrorMessage = exceptionFeature.Error.Message;
                ViewBag.RouteOfException = exceptionFeature.Path;
              
                
            }

            
        }
    }
}