using OnlineShop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var sess = (UserLogin) Session[CommonConstants.USER_SESSION];
            if(sess  == null)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { area = "Admin", controller = "Login", action = "Index" }));
            }

            base.OnActionExecuted(filterContext);
        }

        protected void SetAlert(string msg, string type)
        {
            TempData["AlertMessage"] = msg;
            if (type == "success")
            {
                TempData["AlertType"] = "alert-success";
            }
            else if (type == "warning")
            {
                TempData["AlertType"] = "alert-warning";
            }
            else if (type == "error")
            {
                TempData["AlertType"] = "alert-danger";
            }
           
        }
    }
}