using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClassLibrary.Commons;
using ClassLibrary.Services;
using NewNationals.Areas.AdminControlPanel.Models;

namespace NewNationals.Areas.AdminControlPanel.Controllers
{
    public class DefaultController : BaseController
    {
        CommentService comService=new CommentService();
        // GET: AdminControlPanel/Home
        public ActionResult Index()
        {
            ViewBag.CountComment = comService.CountComment();
            ViewBag.CountPage = comService.CountPage(); 
            ViewBag.CountUser = comService.CountUser();
            ViewBag.CountCategory = comService.CountCategory();
            ViewBag.CountLog = comService.CountLog();
            ViewBag.CountSetting = comService.CountSetting();
            ViewBag.CountMenu = comService.CountMenu();
            ViewBag.CountContact = comService.CountContact();
            return View();
        }

        UserService userService = new UserService();
        public ActionResult UserInfo()
        {
            var entity = userService.GetUserByUserName(Session[CommonsHelper.SessionAdminCp].ToString());
            if (entity == null)
            {
                return HttpNotFound();
            }
            UserModels user = new UserModels();
            user.Id = entity.Id;
            user.UserName = entity.UserName;
            user.UserEmail = entity.UserEmail;
            user.UserPassword = CommonsHelper.EncrytPassword(entity.UserPassword);
            user.FullName = entity.FullName;
            user.Avatar = entity.Avatar;
            user.Status = entity.Status;
            user.Avatar = entity.Avatar;
            user.CreateDate = DateTime.Now;
            user.RoleId = entity.RoleId;
            return View(user);
        }
        [HttpGet]
        public ActionResult ChangePass()
        {
            var entity = new ChangePassModels();
            entity.Username = Session[CommonsHelper.SessionAdminCp].ToString();
            return View(entity);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePass(ChangePassModels entity)
        {
            if (ModelState.IsValid)
            {
                int checklogin = userService.CheckLogin(entity.Username, CommonsHelper.EncrytPassword(entity.PassOld));
                if (checklogin > 0)
                {
                    var getuser = userService.GetUserByUserName(entity.Username);
                    bool checkchange = userService.ChangePass(getuser.Id, CommonsHelper.EncrytPassword(entity.PasswordNew));
                    if (checkchange)
                    {
                        ModelState.AddModelError("", "Đổi mật khẩu thành công!");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Lỗi: không đổi được mật khẩu!");
                    }
                    return View();
                }
                else
                {
                    ModelState.AddModelError("", "Lỗi: mật khẩu cũ không chính xác!");
                    return View();
                }
            }
            else
            {
                ModelState.AddModelError("", "Lỗi!");
                return View();
            }
        }
    }
}