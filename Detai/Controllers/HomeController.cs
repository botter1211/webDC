using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Detai.Models;

namespace Detai.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        DCEntities db = new DCEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Detail()
        {
            return View();
        }

        public ActionResult getCategory()
        {
            ViewBag.meta = "category";
            var v = from t in db.categories
                    where t.hide == true
                    orderby t.order ascending
                    select t;
            return PartialView(v.ToList());
        }

        public ActionResult getSingle(long id)
        {
            ViewBag.meta = "category";
            var v = from t in db.singles
                    where t.categoryid == id && t.hide == true
                    orderby t.order ascending
                    select t;
            return PartialView(v.ToList());
        }

        public ActionResult getAbout()
        {
            var v = from t in db.abouts
                    select t;
            return PartialView(v.ToList());
        }
    }
}