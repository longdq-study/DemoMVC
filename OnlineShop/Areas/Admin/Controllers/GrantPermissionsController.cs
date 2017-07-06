using Model.EF;
using OnlineShop.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class GrantPermissionsController : BaseController
    {
        // GET: Admin/GrantPermissions

        private OnlineShopDbContext db = new OnlineShopDbContext();
        public ActionResult Index(string id)
        {
            //Lấy ra tất cả danh sách nghiệp vụ
            var listController = db.Businesses.AsEnumerable();

            List<SelectListItem> items = new List<SelectListItem>();

            foreach (var item in listController)
            {
                items.Add(new SelectListItem() { Text = item.Name, Value = item.Code });
            }
            //tra ve du lieu cho dropdowlist
            ViewBag.BusinessList = items;

            Session[Common.CommonConstants.USER_GRANT_ROLE] = id;


            return View();
        }
        public JsonResult GetPermission(string businessId, int userId)
        {
            //lay  ra tat ca permision cua business hien tai bigger
            var listPermission = (from p in db.Permissions
                                  where p.BusinessCode == businessId
                                  select new PermissionAction { PermissionId = p.ID, PermissionName = p.ActionName, Description = p.Description, isGrant = false }).ToList();

            //lay ra tat ca list permission cua user va business hien tai smaller

            var listGrant = (from g in db.GrantPermissions
                             join p in db.Permissions on g.PermissionID equals p.ID
                             where g.UserID == userId && p.BusinessCode == businessId
                             select new PermissionAction { PermissionId = p.ID, PermissionName = p.ActionName, Description = p.Description, isGrant = true }).ToList();

            var listPermissionId = listGrant.Select(p => p.PermissionId);

            foreach (var i in listPermission)
            {
                if (!listPermissionId.Contains(i.PermissionId))
                {
                    listGrant.Add(i);
                }
            }

            return Json(listGrant.OrderBy(x => x.PermissionName), JsonRequestBehavior.AllowGet);
        }
    }
}