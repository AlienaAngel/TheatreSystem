using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TheatreSystem.Controllers;

namespace TheatreSystem.Filters
{
    public class AuthAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            AuthorizationHelper.InitializationAuthVar(filterContext.Controller.ViewBag, filterContext.HttpContext.Session);
        }

    }
}