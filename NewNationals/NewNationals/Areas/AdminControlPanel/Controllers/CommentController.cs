using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClassLibrary.Commons;
using ClassLibrary.Services;
using ClassLibrary.Models;
using NewNationals.Areas.AdminControlPanel.Models;
using PagedList;

namespace NewNationals.Areas.AdminControlPanel.Controllers
{
    public class CommentController : Controller
    {
        CommentService comService=new CommentService();
        // GET: AdminControlPanel/Comment
        public ActionResult Index(int? page, string SearchString, string FromDate, string ToDate)
        {
            int pageNum = page ?? 1;
            var showlist = comService.ListAllComment();
            var listslide = new List<Comment>();
            foreach (var item in showlist)
            {
                listslide.Add(new Comment()
                {
                    Id = item.Id,
                    FullName = item.FullName,
                    Email = item.Email,
                    Messager = item.Messager,
                    CreateDate = item.CreateDate,
                    Status = item.Status
                });
            }
            if (!string.IsNullOrEmpty(SearchString))
            {
                showlist = showlist.Where(x => x.FullName.Contains(SearchString) || x.Email.Contains(SearchString) || x.Messager.Contains(SearchString));
            }
            try
            {
                if (!string.IsNullOrEmpty(FromDate))
                {
                    if (!FromDate.Trim().Equals(string.Empty))
                    {
                        showlist = showlist.Where(x => x.CreateDate.Date >= DateTime.Parse(FromDate).Date);
                    }
                }
            }
            catch
            {
            }
            try
            {
                if (!string.IsNullOrEmpty(ToDate))
                {
                    if (!ToDate.Trim().Equals(string.Empty))
                    {
                        showlist = showlist.Where(x => x.CreateDate.Date <= DateTime.Parse(ToDate).Date);
                    }
                }
            }
            catch
            {
            }
            return View(showlist.ToPagedList(pageNum, 30));
        }
        #region [Delete]
        [HttpPost]
        public JsonResult Delete(long id)
        {
            var data = true;
            try
            {
                var comment = comService.GetCommentById(id);
                if (comment != null)
                {
                    // thay đổi trạng thái về -1 coi như bản ghi đã bị xóa
                    comService.Delete(id);
                }
            }
            catch (Exception)
            {
                data = false;
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region [Accept]
        [HttpPost]
        public JsonResult Accept(long id)
        {
            var data = true;
            try
            {
                var comment = comService.GetCommentById(id);
                if (comment != null)
                {
                    comService.UpdateStatus(id, 1);
                }
            }
            catch (Exception)
            {
                data = false;
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region [Desagree]
        [HttpPost]
        public JsonResult Desagree(long id)
        {
            var data = true;
            try
            {
                var comment = comService.GetCommentById(id);
                if (comment != null)
                {
                    comService.UpdateStatus(id, 0);
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