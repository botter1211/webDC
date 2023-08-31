using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Detai.Models;

namespace Detai.Controllers
{
    public class FilterController : Controller
    {
        DCEntities db = new DCEntities();
        // GET: Filter
        public ActionResult Index(string meta)
        {
            var v = from t in db.categories
                    where t.meta == meta
                    select t;
            return View(v.FirstOrDefault());
        }

        public ActionResult Detail(long id)
        {
            var v = from t in db.singles
                    where t.id == id
                    select t;
            return View(v.FirstOrDefault());
        }
    }
}