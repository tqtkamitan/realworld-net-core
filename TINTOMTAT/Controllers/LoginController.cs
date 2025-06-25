using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TINTOMTAT.Data;
using TINTOMTAT.Data.Entites;
using TINTOMTAT.Models.Login;

namespace TINTOMTAT.Controllers
{
    public class LoginController : Controller
    {
        QuangTanDbContext _connect = new QuangTanDbContext();

        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(LoginViewModel model)
        {
            if (string.IsNullOrEmpty(model.UserName))
            {
                return null;
            }

            var user = _connect.Members.Where(s => s.Email.Equals(model.UserName) && (string.IsNullOrEmpty(s.Password) || s.Password.Equals(model.Password)) && s.AccountType == "Local").ToList();
            if (user.Count() > 0)
            {
                //add session
                Session["ThanhVienId"] = user.FirstOrDefault().Id;
                Session["TenThanhVien"] = user.FirstOrDefault().DisplayName;
                Session["TaiKhoanThanhVien"] = user.FirstOrDefault().Email;
                Session["IdThanhVien"] = user.FirstOrDefault().Id;
                if (String.IsNullOrEmpty(model.RedirecUrl))
                {
                    return RedirectToAction("Index", "Home");
                }

                return Redirect(model.RedirecUrl);
            }
            else
            {
                ViewBag.error = "Login failed";
                return Content("Không tìm thấy tài khoản này");
            }


            return Redirect(model.RedirecUrl);
        }

        public ActionResult Register(LoginViewModel model)
        {
            if(string.IsNullOrEmpty(model.UserName) || string.IsNullOrEmpty(model.Password))
            {
                return Content("Không được để trống Username hoặc Password");
            }

            var checkUser = _connect.Members.Where(x => x.Email == model.UserName).FirstOrDefault();
            if(checkUser != null){
                return Content("User (email) đã tồn tại trong hệ thống");
            }

            var endName = model.UserName.IndexOf("@");
            var tenHienThi = model.UserName.Substring(0, endName);
            var thanhVien = new Member();
            thanhVien.DisplayName = tenHienThi;
            thanhVien.Email = model.UserName;
            thanhVien.Password = model.Password;
            thanhVien.CreateDate = DateTime.Now;
            thanhVien.AccountTypeId = 1;
            thanhVien.AccountType = "Local";

            _connect.Members.Add(thanhVien);
            _connect.SaveChanges();

            var user = _connect.Members.Where(s => s.Email.Equals(model.UserName) && (string.IsNullOrEmpty(s.Password) || s.Password.Equals(model.Password))).ToList();
            if (user == null)
            {
                ViewBag.error = "Login failed";
                return RedirectToAction("Index", "Home");
            }
            //add session
            Session["ThanhVienId"] = user.FirstOrDefault().Id;
            Session["TenThanhVien"] = user.FirstOrDefault().DisplayName;
            Session["TaiKhoanThanhVien"] = user.FirstOrDefault().Email;
            Session["IdThanhVien"] = user.FirstOrDefault().Id;

            return Redirect(model.RedirecUrl);

        }
        public ActionResult Logout(string redirecUrl)
        {
            FormsAuthentication.SignOut();
            Session["ThanhVienId"] = "";
            Session["TenThanhVien"] = "";
            Session["TaiKhoanThanhVien"] = "";
            Session["IdThanhVien"] = "";
            //Session.Clear();
            //Session.RemoveAll();
            //Session.Abandon();
            return Redirect(redirecUrl);
        }

        [HttpPost]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl, int ScrollTop = 0)
        {
            // Request a redirect to the external login provider
            TempData["ScrollTop"] = ScrollTop;
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Login", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string ReturnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            //get user
            var user = _connect.Members.Where(x => x.Email == loginInfo.Email).ToList();
            if(user.Count() <=0 )
            {
                //register
                var thanhVien = new Member();
                thanhVien.DisplayName = loginInfo.DefaultUserName;
                thanhVien.Email = loginInfo.Email;
                thanhVien.Password = "123";
                thanhVien.CreateDate = DateTime.Now;
                thanhVien.AccountTypeId = 2;
                thanhVien.AccountType = "Google";

                _connect.Members.Add(thanhVien);
                _connect.SaveChanges();

                var userAfter = _connect.Members.Where(s => s.Email.Equals(loginInfo.Email)).ToList();

                //login; 
                Session["ThanhVienId"] = userAfter.FirstOrDefault().Id;
                Session["TenThanhVien"] = userAfter.FirstOrDefault().DisplayName;
                Session["TaiKhoanThanhVien"] = userAfter.FirstOrDefault().Email;
                Session["IdThanhVien"] = userAfter.FirstOrDefault().Id;
            }
            else
            {
                if (user.FirstOrDefault().IsDeleted == true)
                {
                    return Content("User has been locked");
                }
                //login; 
                Session["ThanhVienId"] = user.FirstOrDefault().Id;
                Session["TenThanhVien"] = user.FirstOrDefault().DisplayName;
                Session["TaiKhoanThanhVien"] = user.FirstOrDefault().Email;
                Session["IdThanhVien"] = user.FirstOrDefault().Id;
            }



            if (string.IsNullOrEmpty(ReturnUrl))
            {
                return RedirectToAction("Index", "Home");
            }
            return Redirect(ReturnUrl);
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

    }


    internal class ChallengeResult : HttpUnauthorizedResult
    {
        public ChallengeResult(string provider, string redirectUri)
            : this(provider, redirectUri, null)
        {
        }

        public ChallengeResult(string provider, string redirectUri, string userId)
        {
            LoginProvider = provider;
            RedirectUri = redirectUri;
            UserId = userId;
        }
        private const string XsrfKey = "XsrfId";
        public string LoginProvider { get; set; }
        public string RedirectUri { get; set; }
        public string UserId { get; set; }
       

        public override void ExecuteResult(ControllerContext context)
        {
            var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
            if (UserId != null)
            {
                properties.Dictionary[XsrfKey] = UserId;
            }
            context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
        }
    }
}