using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TINTOMTAT.Data;
using TINTOMTAT.Data.Entites;
using TINTOMTAT.Models.Comments;
using TINTOMTAT.Models.PostPortalViewModel;

namespace TINTOMTAT.Controllers
{
    public class PostController : Controller
    {
        QuangTanDbContext _connect = new QuangTanDbContext();

        public ActionResult Index(string alias = "")
        {
            var post = _connect.Posts.FirstOrDefault(x => x.Alias.Contains(alias));
            var postViewModel = new PostDetailViewModel
            {
                Id = post.Id,
                PostName = post.PostName,
                Alias = post.Alias,
                Content = post.Content,
                PostImage = post.PostImage,
                View = post.View ?? 0 + post.FView ?? 0,
                CreatedDate = post.CreatedDate

            };

            var postHot = _connect.Posts.Where(x => x.IsDeleted != true).Take(4).Select(p => new Models.PostViewModel
            {
                Id = p.Id,
                PostName = p.PostName,
                Alias = p.Alias,
                View = p.View ?? 0 + p.FView ?? 0,
                PostImage = p.PostImage
            }).ToList();

            ViewBag.PostHot = postHot;
            //update lượt xem

            post.View = post.View != null ? post.View += 1 : 0;
            _connect.Entry(post).State = EntityState.Modified;
            _connect.SaveChanges();

            return View(postViewModel);
        }

        [ChildActionOnly]
        public ActionResult GetCommentForPost(long postId = 0)
        {
            var lisCommnets = _connect.Comments.Where(x => x.PostId == postId && x.IsDeleted.Value != true).ToList().OrderByDescending(x => x.CreatedDate);
            ViewBag.BaiVietId = postId;

            if (TempData["ScrollTop"] == null)
            {
                TempData["ScrollTop"] = 0;
            }


            return PartialView(lisCommnets);
        }

        public ActionResult BinhLuan(CommentViewModel model)
        {
            var binhluan = new Comment();
            binhluan.PostId = model.PostId;
            binhluan.MemberId = model.MemberId;
            binhluan.CommentData = model.Comment;
            binhluan.CreatedDate = DateTime.Now;

            _connect.Comments.Add(binhluan);
            _connect.SaveChanges();

            //ViewBag.ScrollTop = model.ScrollTop;
            TempData["ScrollTop"] = model.ScrollTop;
            return Redirect(model.RedirecUrlComt);
        }
    }
}