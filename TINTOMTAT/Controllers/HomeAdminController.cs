using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TINTOMTAT.Controllers
{
    public class HomeAdminController : Controller
    {
        // GET: HomeAdmin
        public ActionResult Index()
        {
            var userName = Session["TaiKhoan"];
            ViewBag.userName = userName;

            return View();
        }
    }
}