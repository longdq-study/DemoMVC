using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Model.EF;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class MyControllersController : Controller
    {
        private OnlineShopDbContext db = new OnlineShopDbContext();

        // GET: Admin/MyControllers
        public ActionResult Index()
        {
            return View(db.MyControllers.ToList());
        }

        // GET: Admin/MyControllers/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MyController myController = db.MyControllers.Find(id);
            if (myController == null)
            {
                return HttpNotFound();
            }
            return View(myController);
        }

        // GET: Admin/MyControllers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/MyControllers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Code,Description")] MyController myController)
        {
            if (ModelState.IsValid)
            {
                db.MyControllers.Add(myController);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(myController);
        }

        // GET: Admin/MyControllers/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MyController myController = db.MyControllers.Find(id);
            if (myController == null)
            {
                return HttpNotFound();
            }
            return View(myController);
        }

        // POST: Admin/MyControllers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Code,Description")] MyController myController)
        {
            if (ModelState.IsValid)
            {
                db.Entry(myController).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(myController);
        }

        // GET: Admin/MyControllers/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MyController myController = db.MyControllers.Find(id);
            if (myController == null)
            {
                return HttpNotFound();
            }
            return View(myController);
        }

        // POST: Admin/MyControllers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            MyController myController = db.MyControllers.Find(id);
            db.MyControllers.Remove(myController);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult GetAll()
        {
            ReflectionController rfc = new ReflectionController();
            List<Type> myControllerList = new List<Type>();
            myControllerList = rfc.GetController("OnlineShop.Areas.Admin.Controllers");

            List<String> listControllerOld = db.MyControllers.Select(c => c.Code).ToList();
            List<String> listActionOld = db.Actions.Select(c => c.ActionName).ToList();

            foreach (var c in myControllerList)
            {
                if (!listControllerOld.Contains(c.Name))
                {
                    MyController mc = new MyController() { Code = c.Name, Name = c.Name, Description = "Chưa có mô tả" };
                    db.MyControllers.Add(mc);
                    db.SaveChanges();
                }
                List<string> actionList = rfc.GetActions(c);
                foreach (var al in actionList)
                {
                    if (listActionOld.Contains(c.Name + "-" + al))
                    {
                        Model.EF.Action action = new Model.EF.Action() { ActionName = c.Name + "-" + al, Description = "Chưa có mô tả", ControllerID = c.Name };

                        db.Actions.Add(action);
                        db.SaveChanges();
                    }
                }

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
