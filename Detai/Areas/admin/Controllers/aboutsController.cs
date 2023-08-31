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

namespace Detai.Areas.admin.Controllers
{
    public class aboutsController : BaseController
    {
        private DCEntities db = new DCEntities();

        // GET: admin/abouts
        public ActionResult Index()
        {
            return View(db.abouts.ToList());
        }

        // GET: admin/abouts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            about about = db.abouts.Find(id);
            if (about == null)
            {
                return HttpNotFound();
            }
            return View(about);
        }

        // GET: admin/abouts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: admin/abouts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "id,img,title,name,detail,linkfb,linkig,linktwitter")] about about, HttpPostedFileBase img)
        {
            var path = "";
            var filename = "";
            if (ModelState.IsValid)
            {
                if (img != null)
                {
                    filename = img.FileName;
                    path = Path.Combine(Server.MapPath("~/Content/images"), filename);
                    img.SaveAs(path);
                    about.img = filename;
                }
                else
                {
                    about.img = "single_1.jpg";
                }
                db.abouts.Add(about);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(about);
        }

        // GET: admin/abouts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            about about = db.abouts.Find(id);
            if (about == null)
            {
                return HttpNotFound();
            }
            return View(about);
        }

        // POST: admin/abouts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "id,img,title,name,detail,linkfb,linkig,linktwitter")] about about, HttpPostedFileBase img)
        {
            var path = "";
                var filename = "";
                about tmp = db.abouts.Find(about.id);
                if (ModelState.IsValid)
                {
                    if (img != null)
                    {
                        filename = DateTime.Now.ToString("dd-MM-yy-hh-mm-ss-") + img.FileName;
                        path = Path.Combine(Server.MapPath("~/Content/images"), filename);
                        img.SaveAs(path);
                        tmp.img = filename;
                    }
                    tmp.name = about.name;
                    tmp.title = about.title;
                    tmp.detail = about.detail;
                    tmp.linkfb = about.linkfb;
                    tmp.linkig = about.linkig;
                    tmp.linktwitter = about.linktwitter;
                    db.Entry(tmp).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            return View(about);
        }

        // GET: admin/abouts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            about about = db.abouts.Find(id);
            if (about == null)
            {
                return HttpNotFound();
            }
            return View(about);
        }

        // POST: admin/abouts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            about about = db.abouts.Find(id);
            db.abouts.Remove(about);
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
