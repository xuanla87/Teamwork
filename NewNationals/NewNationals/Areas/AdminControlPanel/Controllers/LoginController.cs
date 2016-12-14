using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ClassLibrary.Commons;
using ClassLibrary.Models;
using ClassLibrary.Services;
using NewNationals.Areas.AdminControlPanel.Models;

namespace NewNationals.Areas.AdminControlPanel.Controllers
{
    public class LoginController : Controller
    {
        LogSystemService logService = new LogSystemService();
        UserService userService = new UserService();
        // GET: AdminControlPanel/Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult UserLogin()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserLogin(LoginModels entity)
        {
            if (ModelState.IsValid)
            {
                
                int checklogin = userService.CheckLogin(entity.Username, CommonsHelper.EncrytPassword(entity.Password));
                if (checklogin > 0)
                {
                    var getuser = userService.GetUserByUserName(entity.Username);
                    Session["userid"] = getuser.Id;
                    Session["username"] = getuser.UserName;
                    Session["fullname"] = getuser.FullName;
                    Session["avatar"] = getuser.Avatar;
                    Session["email"] = getuser.UserEmail;
                    Session.Add(CommonsHelper.SessionAdminCp, entity.Username);
                    FormsAuthentication.SetAuthCookie(entity.Username, false);
                    var logs = new LogSystem();
                    logs.IPAddress = CommonsHelper.GetIpAddress;
                    logs.CreateDate = DateTime.Now;
                    logs.Messenger = "Đăng Nhập hệ thống với tài khoản: " + entity.Username;
                    logs.Status = false;
                    logService.Insert(logs);
                    return RedirectToAction("Index", "Default", new { area = "AdminControlPanel" });
                }
                else
                {
                    ModelState.AddModelError("", "Lỗi: Tài khoản hoặc Mật khẩu không hợp lệ!");
                    return View();
                }
            }
            else
            {
                ModelState.AddModelError("", "Lỗi: Tài khoản hoặc Mật khẩu không hợp lệ!");
                return View();
            }
        }
        [HttpPost]
        public JsonResult LogOut()
        {
            //Đăng xuất khỏi ứng dụng
            FormsAuthentication.SignOut();
            Session["userid"] = null;
            Session["username"] = null;
            Session["fullname"] = null;
            Session["avatar"] = null;
            Session["email"] = null;
            Session.Add(CommonsHelper.SessionAdminCp, null);
            //Về trang chủ
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}