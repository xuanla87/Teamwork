using System;
using System.Collections.Generic;
using System.IO;
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
    public class PageController : BaseController
    {
        CategoriesService catesService=new CategoriesService();
        PagesService pagService=new PagesService();
        UserService userService = new UserService();
        TagService tagService=new TagService();
        PageMetaService pageMetaService=new PageMetaService();
        ModelSystems settings=new ModelSystems();
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
        public string GetNameCategory(int? id)
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
            ViewBag.CategoriesId = catesService.GetCategoriesSelectList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(PageModels entity, HttpPostedFileBase[] files)
        {
            var getuser = userService.GetUserByUserName(Session[CommonsHelper.SessionAdminCp].ToString());
            ViewBag.CategoriesId = catesService.GetCategoriesSelectList();
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
                    page.Status = 1;
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
                    page.Taxanomy = "Content";
                    pagService.Insert(page);
                    long getid = page.Id;
                    string geturl = page.Url + "-" + getid;
                    //-------------------------------------------------------------------------------
                    // cập nhật lại url
                    pagService.UpdateUrl(getid, geturl);
                    // lưu nội dung vào bảng tag neu co
                    if (!string.IsNullOrEmpty(entity.Tag))
                    {
                        string gettag = entity.Tag;
                        string[] arr = gettag.Split(',');
                        for (int i = 0; i < arr.Length; i++)
                        {
                            if(!string.IsNullOrEmpty(arr[i]))
                            {
                                Tag tg = new Tag();
                                tg.PageId = getid;
                                tg.stTag = arr[i];
                                tagService.Insert(tg);
                            }
                        }
                    }
                    if (!Directory.Exists(Server.MapPath("~/Images/FileAttach/")))
                    {
                        Directory.CreateDirectory(Server.MapPath("~/Images/FileAttach/"));
                    }
                    try
                    {
                        /*Lopp for multiple files*/
                        foreach (HttpPostedFileBase file in files)
                        {
                            var filename = System.IO.Path.GetFileName(file.FileName);
                            //string extfile = Path.GetExtension(fileName);
                            //string filerename = fileName + "_" + DateTime.Now.Ticks.ToString() + extfile;
                            var path = Path.Combine(Server.MapPath("~/Images/FileAttach/"), filename);
                            file.SaveAs(path);
                            PageMeta pagmeta = new PageMeta();
                            pagmeta.Id = 1;
                            pagmeta.PageId = getid;
                            pagmeta.stKey = "FILEUPLOAD";
                            pagmeta.stValue = "/Images/FileAttach/" + filename;
                            pageMetaService.Insert(pagmeta);
                        }
                    }
                    catch
                    {
                    }
                    if(!string.IsNullOrEmpty(entity.LinkRelated))
                    {
                        PageMeta pgmeta = new PageMeta();
                        pgmeta.Id = 1;
                        pgmeta.PageId = getid;
                        pgmeta.stKey = "LIENKET";
                        pgmeta.stValue = entity.LinkRelated;
                        pageMetaService.Insert(pgmeta);
                    }
                   

                    return RedirectToAction("Index", "Page");
                }
                catch
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
            ViewBag.CategoriesId = catesService.GetCategoriesSelectList();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var entity = pagService.GetPageById(int.Parse(id.ToString()));
            if (entity == null)
            {
                return HttpNotFound();
            }
            //-----------------------------------------------------------------------
            // lấy nội dung tag nếu bài viết đã có tags trước đó
            var tagentity = tagService.ListTagById(entity.Id);
            string gettag = "";
            foreach (var item in tagentity)
            {
                gettag += item.stTag + ",";
            }
            //-----------------------------------------------------------------------
            PageModels page = new PageModels();
            page.Id = entity.Id;
            page.Name = entity.Name;
            page.Url = CommonsHelper.FilterCharCommas(entity.Name) + "-" + entity.Id;
            page.Title = entity.Title;
            page.Keywords = entity.Keywords;
            page.Description = entity.Description;
            page.Status = 1;
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
            page.Tag = gettag; // hiển thị tags ra bên view
            return View(page);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(PageModels entity, HttpPostedFileBase[] files)
        {
            var getuser = userService.GetUserByUserName(Session[CommonsHelper.SessionAdminCp].ToString());
            ViewBag.CategoriesId = catesService.GetCategoriesSelectList();
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
                    page.Status = 1;
                    page.CreateDate = entity.CreateDate;
                    page.ModifiedDate = DateTime.Now;
                    page.UserCreate = entity.UserCreate;
                    page.UserModified = getuser.Id;
                    page.Thumbnail = entity.Thumbnail;
                    page.Content = entity.Content;
                    page.Note = entity.Note;
                    page.Feature = entity.Feature;
                    page.Home = entity.Home;
                    page.CategoriesId = entity.CategoriesId;
                    page.Taxanomy = entity.Taxanomy;
                    pagService.Update(page);
                    //---------------------------------------------------------
                    // xóa toàn bộ tag của bài viết
                    var listltag = tagService.ListTagById(entity.Id);
                    tagService.Delete(listltag);
                    //---------------------------------------------------------
                    // lưu lại nội dung tag
                    if (!string.IsNullOrEmpty(entity.Tag))
                    {
                        string gettag = entity.Tag;
                        string[] arr = gettag.Split(',');
                        for (int i = 0; i < arr.Length; i++)
                        {
                            if (!string.IsNullOrEmpty(arr[i]))
                            {
                                Tag tg = new Tag();
                                tg.PageId = entity.Id;
                                tg.stTag = arr[i];
                                tagService.Insert(tg);
                            }
                        }
                    }
                    //---------------------------------------------------------------------
                    // upload file
                    var listpagemetaFileUpload = pageMetaService.ListPageMetaById(entity.Id,"FILEUPLOAD");
                    pageMetaService.DeletePageId(listpagemetaFileUpload);

                    if (!Directory.Exists(Server.MapPath("~/Images/FileAttach/")))
                    {
                        Directory.CreateDirectory(Server.MapPath("~/Images/FileAttach/"));
                    }
                    try
                    {
                        /*Lopp for multiple files*/
                        foreach (HttpPostedFileBase file in files)
                        {
                            var filename = Path.GetFileName(file.FileName);
                            //string extfile = Path.GetExtension(fileName);
                            //string filerename = fileName + "_" + DateTime.Now.Ticks.ToString() + extfile;
                            var path = Path.Combine(Server.MapPath("~/Images/FileAttach/"), filename);
                            file.SaveAs(path);
                            PageMeta pagmeta = new PageMeta();
                            pagmeta.Id = 1;
                            pagmeta.PageId = entity.Id;
                            pagmeta.stKey = "FILEUPLOAD";
                            pagmeta.stValue = "/Images/FileAttach/" + filename;
                            pageMetaService.Insert(pagmeta);
                        }
                    }
                    catch
                    {
                    }
                    var listpagemetaLienKet = pageMetaService.ListPageMetaById(entity.Id, "LIENKET");
                    pageMetaService.DeletePageId(listpagemetaLienKet);
                    if (!string.IsNullOrEmpty(entity.LinkRelated))
                    {
                        PageMeta pgmeta = new PageMeta();
                        pgmeta.Id = 1;
                        pgmeta.PageId = entity.Id;
                        pgmeta.stKey = "LIENKET";
                        pgmeta.stValue = entity.LinkRelated;
                        pageMetaService.Insert(pgmeta);
                    }
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
        #region [Delete]
        [HttpPost]
        public JsonResult Delete(long id)
        {
            var data = true;
            try
            {
                var page = pagService.GetPageById(id);
                if (page != null)
                {
                    // thay đổi trạng thái về -1 coi như bản ghi đã bị xóa
                    //page.Status = -1;
                    pagService.Delete(page);
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
        public JsonResult SetUpdateStatus(int id, int status)
        {
            var data = true;
            try
            {
                var page = pagService.GetPageById(id);
                if (page != null)
                {
                    pagService.UpdateStatus(id, status);
                }
            }
            catch (Exception)
            {
                data = false;
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region [Update Feature]
        [HttpPost]
        public JsonResult SetUpdateFeature(long id,bool status)
        {
            var data = true;
            try
            {
                var page = pagService.GetPageById(id);
                if (page != null)
                {
                    pagService.UpdateFeature(id, status);
                }
            }
            catch (Exception)
            {
                data = false;
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region [Update Home]
        [HttpPost]
        public JsonResult SetUpdateHome(long id, bool status)
        {
            var data = true;
            try
            {
                var page = pagService.GetPageById(id);
                if (page != null)
                {
                    pagService.UpdateHome(id, status);
                }
            }
            catch (Exception)
            {
                data = false;
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion

        [HttpGet]
        public ActionResult Settings()
        {
            PageModels entity = new PageModels();
            var getentity = pagService.PageGetSettings("Page");
            entity.Content = getentity.Content;
            entity.Name = getentity.Name;
            return View(entity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Settings(PageModels entity)
        {
            if (ModelState.IsValid)
            {
                pagService.PageSettingSaveValue("Page", entity.Content, entity.Name);
            }
            return View(entity);
        }
    }
}