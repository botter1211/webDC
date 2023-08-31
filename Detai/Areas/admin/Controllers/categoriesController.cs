using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Detai.Models;
using Detai.Help;


namespace Detai.Areas.admin.Controllers
{
    public class categoriesController : BaseController
    {
        private DCEntities db = new DCEntities();

        // GET: admin/categories
        public ActionResult Index()
        {
            return View(db.categories.ToList());
        }

        // GET: admin/categories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            category category = db.categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // GET: admin/categories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: admin/categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "id,name,link,meta,hide,order,datebegin")] category category)
        {
            
            if (ModelState.IsValid)
            {
                category.datebegin = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                category.meta = Functions.ConvertToUnSign(category.name);
                db.categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(category);
        }

        // GET: admin/categories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            category category = db.categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: admin/categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "id,name,link,meta,hide,order,datebegin")] category category)
        {
            category tmp = getById(category.id);
            if (ModelState.IsValid)
            {
                tmp.name = category.name;
                tmp.meta = Functions.ConvertToUnSign(category.meta);
                tmp.hide = category.hide;
                tmp.order = category.order;
                tmp.datebegin = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                db.Entry(tmp).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }
        public category getById(long id)
        {
            return db.categories.AsNoTracking().Where(x => x.id == id).FirstOrDefault();
        }

        // GET: admin/categories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            category category = db.categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: admin/categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            category category = db.categories.Find(id);
            db.categories.Remove(category);
            db.SaveChanges();
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
