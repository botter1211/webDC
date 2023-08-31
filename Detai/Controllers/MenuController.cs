using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Detai.Models;

namespace Detai.Controllers
{
    public class MenuController : Controller
    {
        DCEntities db = new DCEntities();
        // GET: Menu
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult getMenu()
        {
            ViewBag.meta = "category";
            var v = from t in db.categories
                    where t.hide == true
                    orderby t.order ascending
                    select t;
            return PartialView(v.ToList());
        }

        public ActionResult getLogo()
        {
            var v = from t in db.logoes
                    where t.hide == true
                    orderby t.order ascending
                    select t;
            return PartialView(v.ToList());
        }
    }
}