using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TINTOMTAT.Data;
using TINTOMTAT.Infrastructure;
using TINTOMTAT.Models.Members;

namespace TINTOMTAT.Controllers
{
    [CustomAuthenticationFilter]
    public class UserAdminController : Controller
    {
        QuangTanDbContext _connect = new QuangTanDbContext();
        // GET: ThanhVienAdmin
        public ActionResult Index()
        {
            var result = _connect.Members.Where(x => x.IsDeleted.Value != true).Select(p => new MemberViewModel
            {
                Id = p.Id,
                DisplayName = p.DisplayName,
                Email = p.Email,
                IsDeleted = p.IsDeleted.Value,
                CreateDate = p.CreateDate.Value
            });

            return View(result);
        }
    }
}