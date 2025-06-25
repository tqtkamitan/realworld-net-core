using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TINTOMTAT.Data;
using TINTOMTAT.Models.PostPortalViewModel;
using TINTOMTAT.Models.PostType;

namespace TINTOMTAT.Controllers
{
    public class HomeController : Controller
    {
        QuangTanDbContext _connect = new QuangTanDbContext();
        public ActionResult Index()
        {

            //get danh sách trang chủ
            var result = _connect.Posts.Where(x => x.Order.HasValue && x.IsDeleted != true).Select(p => new PostViewModel
            {
            Id = p.Id,
            PostName = p.PostName,
            Alias = p.Alias,
            View = p.View ??0 +p.FView??0,
            ShortContent = p.ShortContent,
            CreatedDate = p.CreatedDate,
                Image = p.PostImage
            }).ToList();

            var postHot = _connect.Posts.Where(x=>x.IsDeleted != true).Take(4).Select(p => new PostViewModel
            {
                Id = p.Id,
                PostName = p.PostName,
                Alias = p.Alias,
                View = p.View ??0 + p.FView??0,
                Image = p.PostImage
            }).ToList();

            ViewBag.PostHot = postHot;

            return View(result);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {   
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult MenuPartial()
        {
            var danhMucs = _connect.PostTypes.Where(x=>x.IsDeleted != true)
                .Select(s=> new PostTypeViewModel { 
                Id = s.Id,
                PostTypeName = s.PostTypeName,
                Alias = s.Alias,
                Order = s.Order
            }).ToList().OrderBy(x => x.Order);
            return PartialView(danhMucs);
        }

        public ActionResult LeftMenuPartial()
        {
            var danhMucs = _connect.PostTypes.Where(x => x.IsDeleted != true)
                .Select(s => new PostTypeViewModel
                {
                    Id = s.Id,
                    PostTypeName = s.PostTypeName,
                    Alias = s.Alias,
                    Order = s.Order
                }).ToList().OrderBy(x => x.Order);
            return PartialView(danhMucs);
        }

        public ActionResult MyCV()
        {
            return View();
        }
    }
}