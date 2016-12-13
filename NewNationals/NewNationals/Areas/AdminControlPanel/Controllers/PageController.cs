using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ClassLibrary.Commons;
using ClassLibrary.Services;
using NewNationals.Areas.AdminControlPanel.Models;
using ClassLibrary.Models;
using PagedList;

namespace NewNationals.Areas.AdminControlPanel.Controllers
{
    public class PageController : Controller
    {
        CategoriesService catesService=new CategoriesService();
        PagesService pagService=new PagesService();
        UserService userService = new UserService();
        // GET: AdminControlPanel/Page
        public ActionResult Index(int? page, string SearchString, string FromDate, string ToDate)
        {
            int pageNum = page ?? 1;
            var showlist = pagService.ListAllPage();
            var listpage = new List<PageModels>();
            foreach (var item in showlist)
            {
                listpage.Add(new PageModels()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Url = item.Url,
                    Title = item.Title,
                    Keywords = item.Keywords,
                    Description = item.Description,
                    Status = item.Status,
                    CreateDate = item.CreateDate,
                    ModifiedDate = item.ModifiedDate,
                    UserCreate = item.UserCreate,
                    UserModified = item.UserModified,
                    Thumbnail=item.Thumbnail,
                    Content = item.Content,
                    Note = item.Note,
                    Feature = item.Feature,
                    Home = item.Home,
                    CategoriesId = item.CategoriesId,
                    Taxanomy = item.Taxanomy
                });
            }
            if (!string.IsNullOrEmpty(SearchString))
            {
                showlist = showlist.Where(x => x.Name.Contains(SearchString) 
                || x.Title.Contains(SearchString) || x.Keywords.Contains(SearchString) || x.Description.Contains(SearchString) 
                || x.Content.Contains(SearchString) || x.Note.Contains(SearchString));
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
            return View(showlist.ToPagedList(pageNum, 20));
        }
        /// <summary>
        ///  hàm trả về tên của groupcategory
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetNameCategory(int id)
        {
            try
            {
                if (string.IsNullOrEmpty(id.ToString()))
                    id = 0;
                var category = catesService.GetCategoryById(id);
                return category.Name;
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
            ViewBag.CategoriesId = new SelectList(catesService.GetSelectListCategory(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(PageModels entity)
        {
            var getuser = userService.GetUserByUserName(Session[CommonsHelper.SessionAdminCp].ToString());
            ViewBag.CategoriesId = new SelectList(catesService.GetSelectListCategory(), "Id", "Name");
            if (ModelState.IsValid)
            {
                try
                {
                    //long? getidpage = (long?)pagService.GetMaxId() == null ? 1 : pagService.GetMaxId() + 1;
                    var page = new Page();
                    page.Id = 1;
                    page.Name = entity.Name;
                    page.Url = CommonsHelper.FilterCharCommas(entity.Name); // + "-" + getidpage;
                    page.Title = entity.Title;
                    page.Keywords = entity.Keywords;
                    page.Description = entity.Description;
                    page.Status = entity.Status;
                    page.CreateDate = DateTime.Now;
                    page.ModifiedDate = DateTime.Now;
                    page.UserCreate = getuser.Id;
                    page.UserModified = getuser.Id;
                    page.Thumbnail = entity.Thumbnail;
                    page.Content = entity.Content;
                    page.Note = entity.Note;
                    page.Feature = entity.Feature;
                    page.Home = entity.Home;
                    page.CategoriesId = entity.CategoriesId;
                    page.Taxanomy = entity.Taxanomy;
                    pagService.Insert(page);
                    long getid = page.Id;
                    string geturl = page.Url + "-" + getid;
                    //-------------------------------------------------------------------------------
                    // cập nhật lại url
                    pagService.UpdateUrl(getid, geturl);

                    return RedirectToAction("Index", "Page");
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
        public ActionResult Edit(long? id)
        {
            var getuser = userService.GetUserByUserName(Session[CommonsHelper.SessionAdminCp].ToString());
            ViewBag.CategoriesId = new SelectList(catesService.GetSelectListCategory(), "Id", "Name");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var entity = pagService.GetPageById(int.Parse(id.ToString()));
            if (entity == null)
            {
                return HttpNotFound();
            }
            PageModels page = new PageModels();
            page.Id = entity.Id;
            page.Name = entity.Name;
            page.Url = CommonsHelper.FilterCharCommas(entity.Name) + "-" + entity.Id;
            page.Title = entity.Title;
            page.Keywords = entity.Keywords;
            page.Description = entity.Description;
            page.Status = entity.Status;
            page.CreateDate = DateTime.Now;
            page.ModifiedDate = DateTime.Now;
            page.UserCreate = getuser.Id;
            page.UserModified = getuser.Id;
            page.Thumbnail = entity.Thumbnail;
            page.Content = entity.Content;
            page.Note = entity.Note;
            page.Feature = entity.Feature;
            page.Home = entity.Home;
            page.CategoriesId = entity.CategoriesId;
            page.Taxanomy = entity.Taxanomy;
            return View(page);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(PageModels entity)
        {
            var getuser = userService.GetUserByUserName(Session[CommonsHelper.SessionAdminCp].ToString());
            ViewBag.CategoriesId = new SelectList(catesService.GetSelectListCategory(), "Id", "Name");
            if (ModelState.IsValid)
            {
                try
                {
                    var page = new Page();
                    page.Id = entity.Id;
                    page.Name = entity.Name;
                    page.Url = CommonsHelper.FilterCharCommas(entity.Name) + "-" + entity.Id;
                    page.Title = entity.Title;
                    page.Keywords = entity.Keywords;
                    page.Description = entity.Description;
                    page.Status = entity.Status;
                    page.CreateDate = DateTime.Now;
                    page.ModifiedDate = DateTime.Now;
                    page.UserCreate = getuser.Id;
                    page.UserModified = getuser.Id;
                    page.Thumbnail = entity.Thumbnail;
                    page.Content = entity.Content;
                    page.Note = entity.Note;
                    page.Feature = entity.Feature;
                    page.Home = entity.Home;
                    page.CategoriesId = entity.CategoriesId;
                    page.Taxanomy = entity.Taxanomy;
                    pagService.Update(page);
                    return RedirectToAction("Index", "Page");
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
    }
}