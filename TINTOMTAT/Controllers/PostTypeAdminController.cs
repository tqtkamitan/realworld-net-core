using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using TINTOMTAT.Data;
using TINTOMTAT.Data.Entites;
using TINTOMTAT.Infrastructure;
using TINTOMTAT.Models.PostType;

namespace TINTOMTAT.Controllers
{
    [CustomAuthenticationFilter]
    public class PostTypeAdminController : Controller
    {
        QuangTanDbContext _connect = new QuangTanDbContext();

        //[Authorize]
        public ActionResult Index()
        {
            var result = _connect.PostTypes.Where(x => x.IsDeleted != true).ToList();
            return View(result);
        }

        [HttpGet]
        public ActionResult ThemDanhMuc()
        {
            ViewBagDanhMucBaiViet();

            return View();
        }

        [HttpPost]
        public ActionResult ThemDanhMuc(PostTypeViewModel model)
        {
            var danhMuc = new PostType()
            {
                PostTypeName = model.PostTypeName,
                Alias = LoaiDau(model.PostTypeName),
                Order = model.Order,
                CreatedDate = DateTime.Now,
                IsDeleted = false
            };

            _connect.PostTypes.Add(danhMuc);
            _connect.SaveChanges();


            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult SuaDanhMuc(long id)
        {
            var danhMuc = _connect.PostTypes.Where(x => x.Id == id).Select(p => new PostTypeViewModel
            {
                Id = p.Id,
                PostTypeName = p.PostTypeName,
                Order = p.Order
            }).FirstOrDefault();
            if (danhMuc == null)
            {
                return HttpNotFound();
            }

            ViewBagDanhMucBaiViet();

            return View(danhMuc);
        }

        [HttpPost]
        public ActionResult SuaDanhMuc(PostTypeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return null;
            }
            PostType danhMuc = _connect.PostTypes.FirstOrDefault(x => x.Id == model.Id);
            if (danhMuc == null)
            {
                return HttpNotFound();
            }

            danhMuc.PostTypeName = model.PostTypeName;
            danhMuc.Alias = LoaiDau(model.PostTypeName);
            danhMuc.Order = model.Order;
            danhMuc.UpdatedDate = DateTime.Now;

            var danhMucUpdate = danhMuc;

            _connect.Entry(danhMuc).State = EntityState.Modified;
            _connect.SaveChanges();


            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult XoaDanhMuc(long id)
        {
            var danhMuc = _connect.PostTypes.Where(x => x.Id == id).FirstOrDefault();
            if (danhMuc == null)
            {
                return HttpNotFound();
            }

            //_connect.DanhMucBaiViets.Remove(danhMuc);
            danhMuc.IsDeleted = true;
            _connect.Entry(danhMuc).State = EntityState.Modified;
            _connect.SaveChanges();

            return Redirect(Request.UrlReferrer.ToString());
        }

        public string LoaiDau(string str)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = str.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty)
                        .Replace('đ', 'd').Replace('Đ', 'D').Replace(' ', '-').Replace('@', '-').Replace('&', '-').Replace('$', '-');
        }



        public void ViewBagDanhMucBaiViet()
        {
            var thuTuHienThi = _connect.PostTypes.Where(x => x.Order.HasValue && x.IsDeleted != true)
                .Select(x => x.Order).OrderBy(x => x.Value).ToList();

            ViewBag.ThuTuHienThi = thuTuHienThi;
        }
    }
}