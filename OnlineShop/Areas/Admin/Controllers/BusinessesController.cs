using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Model.EF;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class BusinessesController : BaseController
    {
        private OnlineShopDbContext db = new OnlineShopDbContext();

        // GET: Admin/Businesses
        public ActionResult Index()
        {
            return View(db.Businesses.ToList());
        }

        // GET: Admin/Businesses/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Business business = db.Businesses.Find(id);
            if (business == null)
            {
                return HttpNotFound();
            }
            return View(business);
        }

        // GET: Admin/Businesses/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Businesses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Code,Name,Description")] Business business)
        {
            if (ModelState.IsValid)
            {
                db.Businesses.Add(business);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(business);
        }

        // GET: Admin/Businesses/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Business business = db.Businesses.Find(id);
            if (business == null)
            {
                return HttpNotFound();
            }
            return View(business);
        }

        // POST: Admin/Businesses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Code,Name,Description")] Business business)
        {
            if (ModelState.IsValid)
            {
                db.Entry(business).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(business);
        }

        // GET: Admin/Businesses/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Business business = db.Businesses.Find(id);
            if (business == null)
            {
                return HttpNotFound();
            }
            return View(business);
        }

        // POST: Admin/Businesses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Business business = db.Businesses.Find(id);
            db.Businesses.Remove(business);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult GetAllBusiness()
        {
            ReflectionController rfc = new ReflectionController();
            List<Type> businessList = new List<Type>();
            businessList = rfc.GetController("OnlineShop.Areas.Admin.Controllers");

            List<String> listControllerOld = db.Businesses.Select(c => c.Code).ToList();
            List<String> listActionOld = db.Permissions.Select(c => c.ActionName).ToList();

            foreach (var c in businessList)
            {
                if (!listControllerOld.Contains(c.Name))
                {
                    Business mc = new Business() { Code = c.Name, Name = c.Name, Description = "Chưa có mô tả" };
                    db.Businesses.Add(mc);
                    //db.SaveChanges();
                }
                List<string> actionList = rfc.GetActions(c);
                foreach (var al in actionList)
                {
                    if (!listActionOld.Contains(c.Name + "-" + al))
                    {
                        Permission p = new Permission();
                        p.ActionName = c.Name + "-" + al;
                        p.Description = "Chưa có mô tả";
                        p.BusinessCode = c.Name ;
                      //  p.Business = db.Businesses.Where(x=>x.Code == c.Name).FirstOrDefault();

                        db.Permissions.Add(p);
                       
                    }
                }
                db.SaveChanges();

            }


            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
