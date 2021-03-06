﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Model.EF;
using Model.Dao;
using OnlineShop.Common;
using PagedList;
using PagedList.Mvc;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class UsersController : BaseController
    {
        private OnlineShopDbContext db = new OnlineShopDbContext();

        // GET: Admin/Users
        public ActionResult Index(int page = 1, int pageSize = 10)
        {
            var dao = new UserDao();
            var model = dao.ListAllPaging(page, pageSize);
            return View(model);
        }

        // GET: Admin/Users/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Admin/Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,UserName,Password,Name,Address,Email,Phone,CreatedDate,CreateBy,ModifiedDate,ModifiedBy,Status")] User user)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                var md5Pass = Encryptor.MD5Hash(user.Password);

                user.Password = md5Pass;

                user.CreateBy = ((UserLogin)Session[CommonConstants.USER_SESSION]).UserName;
                user.CreatedDate = DateTime.Now;

                user.ModifiedBy = ((UserLogin)Session[CommonConstants.USER_SESSION]).UserName;
                user.ModifiedDate = DateTime.Now;

                long id = dao.Insert(user);
                if (id > 0)
                {
                    return RedirectToAction("Index", "Users");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm người dùng thành công");
                }
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: Admin/Users/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Admin/Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,UserName,Password,Name,Address,Email,Phone,CreatedDate,CreateBy,ModifiedDate,ModifiedBy,Status")] User user)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                if (user.Password != null)
                {
                    user.Password = Encryptor.MD5Hash(user.Password);
                }
                else
                {
                    User u = dao.GetById(user.UserName);
                    user.Password = u.Password;

                }
                user.ModifiedBy = ((UserLogin)Session[CommonConstants.USER_SESSION]).UserName;
                user.ModifiedDate = DateTime.Now;

                dao.Update(user);
                return RedirectToAction("Index");
            }
            return View(user);
        }

        //// GET: Admin/Users/Delete/5
        //public ActionResult Delete(long? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    User user = db.Users.Find(id);
        //    if (user == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(user);
        //}

        //public ActionResult Delete(long? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }

        //    return View();
        //}

        //// POST: Admin/Users/Delete/5
        //[HttpPost, ActionName("Delete1")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(long id)
        //{
        //    User user = db.Users.Find(id);
        //    db.Users.Remove(user);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        [HttpPost, ActionName("Delete")]
        public ActionResult Delete(long id)
        {
            try
            {
                User user = db.Users.Find(id);
                db.Users.Remove(user);
                db.SaveChanges();
                return Json(new { errorCode = "0", errorMsg = "Xóa thành công.", id = id });
                //return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return Json(new { errorCode = "1", errorMsg = "Xóa thất bại." });
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult Logout()
        {
            Session[CommonConstants.USER_SESSION] = null;
            return Redirect("/");
        }

        [HttpPost, ActionName("ChangeStatus")]
        public ActionResult ChangeStatus(long id)
        {
            User user = db.Users.Find(id);
            try
            {
                user.Status = !user.Status;
                db.SaveChanges();
                return Json(new { errorCode = "0", errorMsg = "Đổi trạng thái thành công.", status = user.Status });
                //return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return Json(new { errorCode = "1", errorMsg = "Đổi trạng thái thất bại.", status = user.Status });
            }
        }
    }
}
