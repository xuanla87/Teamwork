using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ClassLibrary.Services;
using ClassLibrary.Commons;
using ClassLibrary.Models;
using NewNationals.Areas.AdminControlPanel.Models;

namespace NewNationals.Areas.AdminControlPanel.Controllers
{
    public class UserController : BaseController
    {
        UserService userService=new UserService();
        RoleService rolService=new RoleService();
        // GET: AdminControlPanel/User
        public ActionResult Index()
        {
            var list = userService.ListAllUser();
            return View(list);
        }
        public string GetNameRole(int? id)
        {
            try
            {
                if (string.IsNullOrEmpty(id.ToString()))
                    id = 0;
                var role = rolService.GetRoleById(id);
                return role.RoleName;
            }
            catch
            {
                return null;
            }
        }
        #region [Insert]
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.RoleId = new SelectList(rolService.ListAllRole(), "Id", "RoleName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserModels entity)
        {
            ViewBag.RoleId = new SelectList(rolService.ListAllRole(), "Id", "RoleName");
            if (ModelState.IsValid)
            {
                try
                {
                    if (!userService.UserNameExits(entity.UserName))
                    {
                        var ad = new User();
                        ad.Id = 1;
                        ad.UserName = entity.UserName;
                        ad.UserEmail = entity.UserEmail;
                        ad.UserPassword = CommonsHelper.EncrytPassword(entity.UserPassword);
                        ad.FullName = entity.FullName;
                        ad.Avatar = entity.Avatar;
                        ad.Status = entity.Status;
                        ad.Avatar = entity.Avatar;
                        ad.CreateDate = DateTime.Now;
                        ad.RoleId = entity.RoleId;
                        userService.Insert(ad);
                        return RedirectToAction("Index", "User");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Lỗi: Tài khoản đã tồn tại!");
                        return View();
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Lỗi!");
                    return View();
                }
            }
            else
            {
                ModelState.AddModelError("", "Lỗi!");
                return View();
            }
        }
        #endregion
        #region [Update]
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            ViewBag.RoleId = new SelectList(rolService.ListAllRole(), "Id", "RoleName");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var entity = userService.GetUserById(int.Parse(id.ToString()));
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserModels entity)
        {
            ViewBag.RoleId = new SelectList(rolService.ListAllRole(), "Id", "RoleName");
            if (ModelState.IsValid)
            {
                try
                {
                    var user = new User();
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
                    userService.Update(user);
                    return RedirectToAction("Index", "User");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Lỗi!");
                    return View();
                }
            }
            else
            {
                ModelState.AddModelError("", "Lỗi!");
                return View();
            }
        }
        #endregion

        #region [Delete]
        [HttpPost]
        public JsonResult Delete(int id)
        {
            var data = true;
            try
            {
                var ad = userService.GetUserById(id);
                if (ad != null)
                {
                    userService.Delete(id);
                }
            }
            catch (Exception)
            {
                data = false;
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region [UpdateStatus]
        [HttpPost]
        public JsonResult UpdateStatus(int id, int status)
        {
            var data = true;
            try
            {
                var ad = userService.GetUserById(id);
                if (ad != null)
                {
                    userService.UpdateStatus(id, status);
                }
            }
            catch (Exception)
            {
                data = false;
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}