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
using System.IO;
using System.Data.Entity.Validation;

namespace Detai.Areas.admin.Controllers
{
    public class singlesController : BaseController
    {
        private DCEntities db = new DCEntities();

        // GET: admin/singles
        public ActionResult Index(long? id = null)
        {
            getCategory(id);
            //var singles = db.singles.Include(s => s.category);
            //return View(singles.ToList());
            return View();
        }
        public void getCategory(long ? selectedId = null)
        {
            ViewBag.Category = new SelectList(db.categories.Where(x => x.hide == true).OrderBy(x => x.order), "id", "name", selectedId);
        }
        public ActionResult getSingle(long? id)
        {
            if(id == null)
            {
                var v = db.singles.OrderBy(x => x.order).ToList();
                return PartialView(v);
            }
            var m = db.singles.Where(x => x.categoryid == id).OrderBy(x => x.order).ToList();
            return PartialView(m);
        }
        // GET: admin/singles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            single single = db.singles.Find(id);
            if (single == null)
            {
                return HttpNotFound();
            }
            return View(single);
        }

        // GET: admin/singles/Create
        public ActionResult Create()
        {
            //ViewBag.categoryid = new SelectList(db.categories, "id", "name");
            getCategory();
            return View();
        }

        // POST: admin/singles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "id,name,img,description,detail,meta,hide,order,datebegin,categoryid")] single single, HttpPostedFileBase img)
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
                        path = Path.Combine(Server.MapPath("~/Content/images"), filename);
                        img.SaveAs(path);
                        single.img = filename;
                    }
                    else
                    {
                        single.img = "single_1.jpg";
                    }
                    single.datebegin = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                    single.meta = Functions.ConvertToUnSign(single.name);
                    //single.order = getMaxOrder(single.categoryid);
                    db.singles.Add(single);
                    db.SaveChanges();
                    //return RedirectToAction("Index");
                    return RedirectToAction("Index", "singles", new { id = single.categoryid });
                }
            }
            catch (DbEntityValidationException e)
            {
                throw e;
            }
            catch(Exception ex)
            {
                throw ex;
            }

            //ViewBag.categoryid = new SelectList(db.categories, "id", "name", single.categoryid);
            return View(single);
        }
        
        // GET: admin/singles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            single single = db.singles.Find(id);
            if (single == null)
            {
                return HttpNotFound();
            }
            //ViewBag.categoryid = new SelectList(db.categories, "id", "name", single.categoryid);
            getCategory(single.categoryid);
            return View(single);
        }

        // POST: admin/singles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput (false)]
        public ActionResult Edit([Bind(Include = "id,name,img,description,detail,meta,hide,order,datebegin,categoryid")] single single, HttpPostedFileBase img)
        {
            try
            {
                var path = "";
                var filename = "";
                single tmp = db.singles.Find(single.id);
                if (ModelState.IsValid)
                {
                    if(img != null)
                    {
                        filename = DateTime.Now.ToString("dd-MM-yy-hh-mm-ss-") + img.FileName;
                        path = Path.Combine(Server.MapPath("~/Content/images"), filename);
                        img.SaveAs(path);
                        tmp.img = filename;
                    }
                    tmp.name = single.name;
                    tmp.meta = Functions.ConvertToUnSign(single.meta);
                    tmp.description = single.description;
                    tmp.detail = single.detail;
                    tmp.hide = single.hide;
                    tmp.order = single.order;
                    tmp.datebegin = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                    db.Entry(tmp).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                    //return RedirectToAction("Index", "single", new { id = single.categoryid });
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
            //ViewBag.categoryid = new SelectList(db.categories, "id", "name", single.categoryid);
                return View(single);
        }

        // GET: admin/singles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            single single = db.singles.Find(id);
            if (single == null)
            {
                return HttpNotFound();
            }
            return View(single);
        }

        // POST: admin/singles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            single single = db.singles.Find(id);
            db.singles.Remove(single);
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

        public int getMaxOrder(long? CategoryId)
        {
            if (CategoryId == null)
                return 1;
            return db.singles.Where(x => x.categoryid == CategoryId).Count();
        }
    }
}
