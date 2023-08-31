using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Detai.Models;
using System.IO;
using System.Data.Entity.Validation;

namespace Detai.Areas.admin.Controllers
{
    public class logoesController : BaseController
    {
        private DCEntities db = new DCEntities();

        // GET: admin/logoes
        public ActionResult Index()
        {
            return View(db.logoes.ToList());
        }

        // GET: admin/logoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            logo logo = db.logoes.Find(id);
            if (logo == null)
            {
                return HttpNotFound();
            }
            return View(logo);
        }

        // GET: admin/logoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: admin/logoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput (false)]
        public ActionResult Create([Bind(Include = "id,img,order,hide,datebegin")] logo logo, HttpPostedFileBase img)
        {
            try
            { 
                var path = "";
                var filename = "";
                if (ModelState.IsValid)
                {
                    if (img != null)
                    {
                        filename = img.FileName;
                        path = Path.Combine(Server.MapPath("~/Content/images/logo"), filename);
                        img.SaveAs(path);
                        logo.img = filename;
                    }
                    logo.datebegin = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                    db.logoes.Add(logo);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DbEntityValidationException e)
            {
                throw e;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View(logo);
        }

        // GET: admin/logoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            logo logo = db.logoes.Find(id);
            if (logo == null)
            {
                return HttpNotFound();
            }
            return View(logo);
        }

        // POST: admin/logoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "id,img,order,hide,datebegin")] logo logo, HttpPostedFileBase img)
        {
            var path = "";
            var filename = "";
            logo tmp = getById(logo.id);
            if (ModelState.IsValid)
            {
                if(img != null)
                {
                    filename = img.FileName;
                    path = Path.Combine(Server.MapPath("~/Content/images/logo"), filename);
                    img.SaveAs(path);
                    tmp.img = filename;
                }
                tmp.hide = logo.hide;
                tmp.order = logo.order;
                tmp.datebegin = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                db.Entry(tmp).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(logo);
        }
        public logo getById(long id)
        {
            return db.logoes.AsNoTracking().Where(x => x.id == id).FirstOrDefault();
        }


        // GET: admin/logoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            logo logo = db.logoes.Find(id);
            if (logo == null)
            {
                return HttpNotFound();
            }
            return View(logo);
        }

        // POST: admin/logoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            logo logo = db.logoes.Find(id);
            db.logoes.Remove(logo);
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
