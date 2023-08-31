using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Detai.Models;
using System.Security.Cryptography;
using System.Text;

namespace Detai.Areas.admin.Controllers
{
    public class DefaultController : BaseController
    {
        
        // GET: admin/Default
        public ActionResult Index()
        {
            return View();
        }

        
    }
}