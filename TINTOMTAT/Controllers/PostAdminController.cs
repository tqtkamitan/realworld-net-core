using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using TINTOMTAT.Data;
using TINTOMTAT.Data.Entites;
using TINTOMTAT.Infrastructure;
using TINTOMTAT.Models;

namespace TINTOMTAT.Controllers
{
    [CustomAuthenticationFilter]
    public class PostAdminController : Controller
    {
        QuangTanDbContext _connect = new QuangTanDbContext();

        public ActionResult Index()
        {
            var result = _connect.Posts.Where(x => x.IsDeleted != true).Select(p => new PostViewModel
            {
                Id = p.Id,
                PostName = p.PostName,
                Alias = p.Alias,
                PostImage = p.PostImage,
                Order = p.Order,
                HomePageOrder = p.HomePageOrder,
                View = (p.View == null ? 0 : p.View) + (p.FView == null ? 0 : p.FView),
                CreatedDate = p.CreatedDate,
                ShortContent = p.ShortContent,
                Content = p.Content,
                UpdatedDate = p.UpdatedDate,
                IsDeleted = p.IsDeleted,
                PostTypeId = p.PostTypeId,
                PostTypeName = p.PostType.PostTypeName
            });

            return View(result);
        }

        [HttpGet]
        public ActionResult ThemBaiViet()
        {
            var danhMuc = _connect.PostTypes.Where(x=>x.IsDeleted != true).ToList();
            ViewBag.DanhMuc = danhMuc;
            ViewBagBaiViet();

            return View();
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult ThemBaiViet(PostViewModel model)
        {
            var baiViet = new Post()
            {
                PostName = model.PostName,
                Alias = LoaiDau(model.PostName),
                HomePageOrder = model.HomePageOrder,
                Order = model.Order,
                FView =  model.FView,
                ShortContent = model.ShortContent,
                Content = model.Content,
                PostTypeId = model.PostTypeId,
                CreatedDate = DateTime.Now,
                IsDeleted = false
            };

            //To Get File Extension  
            string FileExtension = Path.GetExtension(model.ImageFile.FileName);

            string FileName = baiViet.Alias + DateTime.Now.ToString("yyyyMMdd") + FileExtension;

            //Get Upload path from Web.Config file AppSettings.  
            string UploadPath = ConfigurationManager.AppSettings["UserImagePath"].ToString();

            //Its Create complete path to store in server.  
            model.PostImage = UploadPath + FileName;

            //To copy and save file into server.  
            model.ImageFile.SaveAs(Server.MapPath(model.PostImage));

            baiViet.PostImage = model.PostImage;
            _connect.Posts.Add(baiViet);
            _connect.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult SuaBaiViet(long id)
        {
            var baiViet = _connect.Posts.Where(x => x.Id == id).Select(p => new PostViewModel
            {
                Id = p.Id,
                PostName = p.PostName,
                HomePageOrder = p.HomePageOrder,
                Order = p.Order,
                View = p.View == null ? 0 : p.View,
                FView = p.FView == null ? 0 : p.FView,
                ShortContent = p.ShortContent,
                Content = p.Content,
                PostTypeId = p.PostTypeId,
                PostImage = p.PostImage
            }).FirstOrDefault();
            if (baiViet == null)
            {
                return HttpNotFound();
            }

            ViewBagBaiViet();

            var danhMuc = _connect.PostTypes.Where(x => x.IsDeleted != true).ToList();
            ViewBag.DanhMuc = danhMuc;
            return View(baiViet);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult SuaBaiViet(PostViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return null;
            }
            Post baiViet = _connect.Posts.FirstOrDefault(x => x.Id == model.Id);
            if (baiViet == null)
            {
                return HttpNotFound();
            }

            baiViet.PostName = model.PostName;
            baiViet.Alias = LoaiDau(model.PostName);
            baiViet.HomePageOrder = model.HomePageOrder;
            baiViet.Order = model.Order;
            baiViet.ShortContent = model.ShortContent;
            baiViet.Content = model.Content;
            baiViet.FView = model.FView == null ? 0 : model.FView;
            baiViet.UpdatedDate = DateTime.Now;
            baiViet.PostTypeId = model.PostTypeId;

            if (model.ImageFile != null)
            {
                //dell file olf
                System.IO.File.Delete(Server.MapPath(model.PostImage));

                //save file new
                //To Get File Extension  
                string FileExtension = Path.GetExtension(model.ImageFile.FileName);

                string FileName = baiViet.Alias + DateTime.Now.ToString("yyyyMMdd") + FileExtension;

                //Get Upload path from Web.Config file AppSettings.  
                string UploadPath = ConfigurationManager.AppSettings["UserImagePath"].ToString();

                //Its Create complete path to store in server.  
                model.PostImage = UploadPath + FileName;
                baiViet.PostImage = model.PostImage;

                //To copy and save file into server.  
                model.ImageFile.SaveAs(Server.MapPath(model.PostImage));
            }
            _connect.Entry(baiViet).State = EntityState.Modified;
            _connect.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult XoaBaiViet(long id)
        {
            var baiViet = _connect.Posts.Where(x => x.Id == id).FirstOrDefault();
            if (baiViet == null)
            {
                return HttpNotFound();
            }

            baiViet.IsDeleted = true;

            _connect.Entry(baiViet).State = EntityState.Modified;
            _connect.SaveChanges();

            return Redirect(Request.UrlReferrer.ToString());
        }

        public string LoaiDau(string str)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = str.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty)
                        .Replace('đ', 'd').Replace('Đ', 'D').Replace(' ', '-');
        }

        public void ViewBagBaiViet()
        {
            var thuTuTrangChu = _connect.Posts.Where(x => x.HomePageOrder.HasValue && x.IsDeleted != true)
                .Select(x => x.HomePageOrder).OrderBy(x=>x.Value).ToList();

            ViewBag.ThuTuTrangChu = thuTuTrangChu;
        }
    }
}