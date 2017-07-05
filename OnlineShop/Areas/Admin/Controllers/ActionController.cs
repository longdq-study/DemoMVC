using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class ActionController : BaseController
    {
        // GET: Admin/Action
        public ActionResult Index()
        {
            return View();
        }
    }
}