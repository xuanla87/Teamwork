using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ClassLibrary.Models;
using ClassLibrary.Services;
using ClassLibrary.Commons;
using NewNationals.Areas.AdminControlPanel.Models;
using PagedList;

namespace NewNationals.Areas.AdminControlPanel.Controllers
{
    public class SlideShowController : Controller
    {
        SlidehowService slideService=new SlidehowService();
        // GET: AdminControlPanel/SlideShow
        public ActionResult Index(int? page, string SearchString, string FromDate, string ToDate)
        {
            int pageNum = page ?? 1;
            var showlist = slideService.ListAllSlide();
            var listslide = new List<SlideShowModels>();
            foreach (var item in showlist)
            {
                listslide.Add(new SlideShowModels()
                {
                    Id = item.Id,
                    Name = item.Name,
                    ImageUrl = item.ImageUrl,
                    Note = item.Note,
                    TargetUrl = item.TargetUrl,
                    Order = item.Order,
                    Tanoxomy = item.Tanoxomy,
                    Status = item.Status,
                    StartDate = item.StartDate,
                    EndDate = item.EndDate
                });
            }
            if (!string.IsNullOrEmpty(SearchString))
            {
                showlist = showlist.Where(x => x.Name.Contains(SearchString) || x.Note.Contains(SearchString));
            }
            try
            {
                if (!string.IsNullOrEmpty(FromDate))
                {
                    if (!FromDate.Trim().Equals(string.Empty))
                    {
                        showlist = showlist.Where(x => x.StartDate.Date >= DateTime.Parse(FromDate).Date);
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
                        showlist = showlist.Where(x => x.EndDate.Date <= DateTime.Parse(ToDate).Date);
                    }
                }
            }
            catch
            {
            }
            return View(showlist.ToPagedList(pageNum, 20));
        }
        #region [Insert]
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SlideShowModels entity)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var slide = new Slidehow();
                    slide.Id = 1;
                    slide.Name = entity.Name;
                    slide.ImageUrl = entity.ImageUrl;
                    slide.Note = entity.Note;
                    slide.TargetUrl = entity.TargetUrl;
                    slide.Order = entity.Order;
                    slide.Tanoxomy = entity.Tanoxomy;
                    slide.Status = entity.Status;
                    slide.StartDate = entity.StartDate;
                    slide.EndDate = entity.EndDate;
                    slideService.Insert(slide);
                    return RedirectToAction("Index", "SlideShow");
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
            var entity = slideService.GetSlideById(int.Parse(id.ToString()));
            if (entity == null)
            {
                return HttpNotFound();
            }
            SlideShowModels slide = new SlideShowModels();
            slide.Id = entity.Id;
            slide.Name = entity.Name;
            slide.ImageUrl = entity.ImageUrl;
            slide.Note = entity.Note;
            slide.TargetUrl = entity.TargetUrl;
            slide.Order = entity.Order;
            slide.Tanoxomy = entity.Tanoxomy;
            slide.Status = entity.Status;
            slide.StartDate = entity.StartDate;
            slide.EndDate = entity.EndDate;
            return View(slide);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SlideShowModels entity)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var slide = new Slidehow();
                    slide.Id = entity.Id;
                    slide.Name = entity.Name;
                    slide.ImageUrl = entity.ImageUrl;
                    slide.Note = entity.Note;
                    slide.TargetUrl = entity.TargetUrl;
                    slide.Order = entity.Order;
                    slide.Tanoxomy = entity.Tanoxomy;
                    slide.Status = entity.Status;
                    slide.StartDate = entity.StartDate;
                    slide.EndDate = entity.EndDate;
                    slideService.Update(slide);
                    return RedirectToAction("Index", "SlideShow");
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
                var category = slideService.GetSlideById(id);
                if (category != null)
                {
                    // thay đổi trạng thái về -1 coi như bản ghi đã bị xóa
                    slideService.Delete(id);
                }
            }
            catch (Exception)
            {
                data = false;
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region [Update Status]
        [HttpPost]
        public JsonResult UpdateStatus(int id, int status)
        {
            var data = true;
            try
            {
                var category = slideService.GetSlideById(id);
                if (category != null)
                {
                    slideService.UpdateStatus(id, status);
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