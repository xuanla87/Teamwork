using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ClassLibrary.Commons;
using ClassLibrary.Models;
using ClassLibrary.Services;
using NewNationals.Areas.AdminControlPanel.Models;


namespace NewNationals.Areas.AdminControlPanel.Controllers
{
    public class RoleController : Controller
    {
        RoleService rolService=new RoleService();
        // GET: AdminControlPanel/Role
        public ActionResult Index()
        {
            string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            var list = rolService.ListAllRole();
            return View(list);
        }
        #region [Insert]
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RoleModels entity)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var role = new Role();
                    role.Id = 1;
                    role.RoleName = entity.RoleName;
                    role.Note = entity.Note;
                    rolService.Insert(role);
                    return RedirectToAction("Index", "Role");
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
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var entity = rolService.GetRoleById(int.Parse(id.ToString()));
            if (entity == null)
            {
                return HttpNotFound();
            }
            RoleModels role = new RoleModels();
            role.Id = entity.Id;
            role.RoleName = entity.RoleName;
            role.Note = entity.Note;
            return View(role);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(RoleModels entity)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var role = new Role();
                    role.Id = entity.Id;
                    role.RoleName = entity.RoleName;
                    role.Note = entity.Note;
                    rolService.Update(role);
                    return RedirectToAction("Index", "Role");
                }
                catch (Exception)
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
                var rol = rolService.GetRoleById(id);
                if (rol != null)
                {
                    // thay đổi trạng thái về -1 coi như bản ghi đã bị xóa
                    rolService.Delete(id);
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