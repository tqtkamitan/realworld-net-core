using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TINTOMTAT.Data;
using TINTOMTAT.Models.PostPortalViewModel;

namespace TINTOMTAT.Controllers
{
    public class CategoryController : Controller
    {
        QuangTanDbContext _connect = new QuangTanDbContext();
        // GET: Category
        public ActionResult Index(string alias)
        {
            var result = _connect.Posts.Where(x => x.PostType.Alias == alias && x.IsDeleted != true).Select(p => new PostViewModel
            {
                Id = p.Id,
                PostName = p.PostName,
                Alias = p.Alias,
                View = p.View,
                ShortContent = p.ShortContent,
                CreatedDate = p.CreatedDate,
                Image = p.PostImage
            }).ToList();

            var postHot = _connect.Posts.Where(x => x.IsDeleted != true).Take(4).Select(p => new PostViewModel
            {
                Id = p.Id,
                PostName = p.PostName,
                Alias = p.Alias,
                View = p.View,
                Image = p.PostImage
            }).ToList();

            ViewBag.Title = alias;
            ViewBag.PostHot = postHot;

            return View(result);
        }
    }
}