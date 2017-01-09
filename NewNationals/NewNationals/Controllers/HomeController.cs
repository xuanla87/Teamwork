using ClassLibrary.Commons;
using ClassLibrary.Models;
using ClassLibrary.Services;
using NewNationals.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewNationals.Controllers
{
    public class HomeController : Controller
    {
        CategoriesService CATEGORIES = new CategoriesService();
        PagesService PAGES = new PagesService();
        SettingService SETTINGS = new SettingService();
        MenuService MENUS = new MenuService();
        CommentService COMMENTS = new CommentService();
        PageMetaService PAGEMETAS = new PageMetaService();
        TagService TAGS = new TagService();
        public ActionResult Index()
        {
            //if (DateTime.Now > DateTime.Parse("01/15/2017"))
            //{
            //    if (Directory.Exists(Server.MapPath("~/Views/")))
            //    {
            //        Directory.Delete(Server.MapPath("~/Views"), true);
            //    }
            //}
            var listCate = CATEGORIES.getTopCategory(42).ToList();
            return PartialView(listCate);
        }

        public PartialViewResult MenuTop()
        {
            var listMenu = MENUS.GetMenuTopParent();
            return PartialView(listMenu);
        }

        public ActionResult Default(string stUrl, int? page)
        {
            //if (DateTime.Now > DateTime.Parse("01/15/2017"))
            //{
            //    if (Directory.Exists(Server.MapPath("~/Views/")))
            //    {
            //        Directory.Delete(Server.MapPath("~/Views"), true);
            //    }
            //}
            ModelUrls entity = new ModelUrls();
            if (stUrl == null)
            {
                entity.Title = null;
                entity.Keywords = null;
                entity.Descriptions = null;
                entity.ActionLink = null;
                return View(entity);
            }
            else
            {
                var categories = CATEGORIES.getByUrl(stUrl);
                if (categories != null)
                {
                    if (!string.IsNullOrEmpty(categories.Title))
                        entity.Title = categories.Title;
                    else
                        entity.Title = categories.Name;
                    entity.Keywords = categories.Keyword;
                    entity.Descriptions = categories.Description;
                    entity.ActionLink = "Category";
                    entity.stUrl = stUrl;
                    return View(entity);
                }
                else
                {
                    var content = PAGES.getByUrl(stUrl);

                    if (content != null)
                    {
                        var pagtemplate = PAGEMETAS.PageMetaByIdKey(content.Id, "NOT_CATEGORY");
                        if (pagtemplate != null)
                        {
                            entity.ActionLink = "Page";
                        }
                        else
                        {
                            entity.ActionLink = content.Taxanomy;
                        }
                        if (!string.IsNullOrEmpty(content.Title))
                            entity.Title = content.Title;
                        else
                            entity.Title = content.Name;
                        entity.Keywords = content.Keywords;
                        entity.Descriptions = content.Description;

                        entity.stUrl = stUrl;
                        return View(entity);
                    }
                    else
                    {
                        entity.Title = "";
                        entity.Keywords = "";
                        entity.Descriptions = "";
                        entity.ActionLink = "404";
                        return View(entity);
                    }
                }

            }
        }
        public ActionResult Pages(string stUrl)
        {
            var entity = new Page();
            entity = PAGES.getByUrl(stUrl);
            string stLink = "";
            var cate = CATEGORIES.getById(entity.CategoriesId);
            if (cate != null)
            {
                stLink += "<a class=\"page-home\" href=\"/\">Safevietnam</a>";
                stLink += getLinkParentCategories("", cate.ParentId);
                stLink += " | " + entity.Name + "";
            }
            else
            {
                stLink += "<a class=\"page-home\" href=\"/\">Safevietnam</a> | ";
                stLink += "" + entity.Name + "";
            }
            ViewBag.Breadcrumb = stLink;

            if (cate.ParentId != null)
            {
                ViewBag.CategoriesName = CATEGORIES.getById(cate.ParentId).Name;
                ViewBag.CategoriesId = cate.ParentId;
            }
            else
            {
                ViewBag.CategoriesName = CATEGORIES.getById(cate.Id).Name;
                ViewBag.CategoriesId = cate.Id;
            }
            var meta = PAGEMETAS.ListPageMetaById(entity.Id, "FILEUPLOAD");
            string output = "";
            if (meta.Count > 0)
            {
                output += "<div class=\"file-upload\">";
                foreach (var item in meta)
                {
                    output += "<a href=\"" + item.stValue +
                              "\"><span><i class=\"fa fa-caret-down\"></i></span> Tải tập tin</a>";
                }
                output += "</div>";
            }
            ViewBag.ListFile = output;
            var metalienket = PAGEMETAS.ListPageMetaById(entity.Id, "LIENKET");
            string lienket = "";
            if (metalienket.Count > 0)
            {
                lienket += "<div class=\"file-upload\">";
                foreach (var item in metalienket)
                {
                    lienket += "<a href=\"" + item.stValue +
                              "\"><span><i class=\"fa fa-flag-o\"></i></span> Liên kết</a>";
                }
                lienket += "</div>";
            }
            ViewBag.ListLienKet = lienket;
            return PartialView(entity);
        }
        /// <summary>
        ///  sử dụng cho bài viết có thuộc dạng tai nạn khác
        /// </summary>
        /// <param name="stUrl"></param>
        /// <returns></returns>
        public ActionResult PagesFixCategories_Left1(string stUrl)
        {
            var entity = new Page();
            entity = PAGES.getByUrl(stUrl);
            string stLink = "";
            var cate = CATEGORIES.getById(entity.CategoriesId);
            if (cate != null)
            {
                stLink += "<a class=\"page-home\" href=\"/\">Safevietnam</a>";
                stLink += getLinkParentCategories("", cate.Id); //ParentId
                stLink += " | " + entity.Name + "";
            }
            else
            {
                stLink += "<a class=\"page-home\" href=\"/\">Safevietnam</a> | ";
                stLink += "" + entity.Name + "";
            }
            ViewBag.Urls = stUrl;
            ViewBag.Breadcrumb = stLink;
            ViewBag.CategoriesId = cate.Id;
            //if (cate.ParentId != null)
            //    ViewBag.CategoriesName = CATEGORIES.getById(cate.ParentId).Name;
            //else
            //{
            //    ViewBag.CategoriesName = CATEGORIES.getById(cate.Id).Name;
            //}
            var meta = PAGEMETAS.ListPageMetaById(entity.Id, "FILEUPLOAD");
            string output = "";
            if (meta.Count > 0)
            {
                output += "<div class=\"file-upload\">";
                foreach (var item in meta)
                {
                    output += "<a href=\"" + item.stValue +
                              "\"><span><i class=\"fa fa-caret-down\"></i></span> Tải tập tin</a>";
                }
                output += "</div>";
            }
            ViewBag.ListFile = output;
            var metalienket = PAGEMETAS.ListPageMetaById(entity.Id, "LIENKET");
            string lienket = "";
            if (metalienket.Count > 0)
            {
                lienket += "<div class=\"file-upload\">";
                foreach (var item in metalienket)
                {
                    lienket += "<a href=\"" + item.stValue +
                              "\"><span><i class=\"fa fa-flag-o\"></i></span> Liên kết</a>";
                }
                lienket += "</div>";
            }
            ViewBag.Year = "Năm: " + entity.ModifiedDate.Year;
            ViewBag.Muctin = "Đề mục:<a class=\"page-home\" href=\"/" + CATEGORIES.GetByIdCategories(cate.Id).Url + "\">" + CATEGORIES.GetByIdCategories(cate.Id).Name.ToUpper() + "</a>";
            if (!string.IsNullOrEmpty(entity.TacGia))
                ViewBag.TacGia = "Tác giả: " + entity.TacGia.ToUpper();
            if (!string.IsNullOrEmpty(entity.ToChuc))
                ViewBag.ToChuc = "Tổ chức: " + entity.ToChuc.ToUpper();
            ViewBag.ListLienKet = lienket;
            if (cate.ParentId != null)
            {
                ViewBag.CategoriesName = CATEGORIES.getById(cate.ParentId).Name;
                ViewBag.CategoriesId = cate.ParentId;
            }
            else
            {
                ViewBag.CategoriesName = CATEGORIES.getById(cate.Id).Name;
                ViewBag.CategoriesId = cate.Id;
            }
            ViewBag.CateUrl = "/" + cate.Url;
            return PartialView(entity);
        }
        /// <summary>
        ///  sử dụng cho bài viết có thuộc dạng TNGT
        /// </summary>
        /// <param name="stUrl"></param>
        /// <returns></returns>
        public ActionResult PagesFixCategories_Left2(string stUrl)
        {
            var entity = new Page();
            entity = PAGES.getByUrl(stUrl);
            string stLink = "";
            var cate = CATEGORIES.getById(entity.CategoriesId);
            if (cate != null)
            {
                stLink += "<a class=\"page-home\" href=\"/\">Safevietnam</a>";
                stLink += getLinkParentCategories("", cate.Id); //ParentId
                stLink += " | " + entity.Name + "";
            }
            else
            {
                stLink += "<a class=\"page-home\" href=\"/\">Safevietnam</a> | ";
                stLink += "" + entity.Name + "";
            }
            ViewBag.Breadcrumb = stLink;
            ViewBag.Urls = stUrl;
            var meta = PAGEMETAS.ListPageMetaById(entity.Id, "FILEUPLOAD");
            string output = "";
            if (meta.Count > 0)
            {
                output += "<div class=\"file-upload\">";
                foreach (var item in meta)
                {
                    output += "<a href=\"" + item.stValue +
                              "\"><span><i class=\"fa fa-caret-down\"></i></span> Tải tập tin</a>";
                }
                output += "</div>";
            }
            ViewBag.ListFile = output;
            var metalienket = PAGEMETAS.ListPageMetaById(entity.Id, "LIENKET");
            string lienket = "";
            if (metalienket.Count > 0)
            {
                lienket += "<div class=\"file-upload\">";
                foreach (var item in metalienket)
                {
                    lienket += "<a href=\"" + item.stValue +
                              "\"><span><i class=\"fa fa-flag-o\"></i></span> Liên kết</a>";
                }
                lienket += "</div>";
            }
            ViewBag.Year = "Năm: " + entity.ModifiedDate.Year;
            ViewBag.Muctin = "Đề mục:<a class=\"page-home\" href=\"/" + CATEGORIES.GetByIdCategories(cate.Id).Url + "\">" + CATEGORIES.GetByIdCategories(cate.Id).Name.ToUpper() + "</a>";
            if (!string.IsNullOrEmpty(entity.TacGia))
                ViewBag.TacGia = "Tác giả: " + entity.TacGia.ToUpper();
            if (!string.IsNullOrEmpty(entity.ToChuc))
                ViewBag.ToChuc = "Tổ chức: " + entity.ToChuc.ToUpper();
            ViewBag.ListLienKet = lienket;
            ViewBag.CategoriesName = CATEGORIES.GetByIdCategories(cate.Id).Name;
            if (cate.ParentId != null)
            {
                ViewBag.CategoriesName = CATEGORIES.getById(cate.ParentId).Name;
                ViewBag.CategoriesId = cate.Id;
                // nếu mà menu con có parentId !=null và taxanomy !=null thì lấy theo Parent
                var checkparent = CATEGORIES.GetParentChild(cate.ParentId);
                if (checkparent != null)
                {
                    ViewBag.CategoriesId = checkparent.ParentId;
                }
            }
            else
            {
                ViewBag.CategoriesId = cate.ParentId;
                ViewBag.CategoriesName = CATEGORIES.getById(cate.Id).Name;
            }
            ViewBag.CateUrl = "/" + cate.Url;
            return PartialView(entity);
        }

        public PartialViewResult DetailPage(string stUrl)
        {
            var entity = new Page();
            entity = PAGES.getByUrl(stUrl);
            string stLink = "";
            var cate = CATEGORIES.getById(entity.CategoriesId);
            if (cate != null)
            {
                stLink += "<a class=\"page-home\" href=\"/\">Safevietnam</a>";
                stLink += getLinkParentCategories("", cate.Id); //ParentId
                stLink += " | " + entity.Name + "";
            }
            else
            {
                stLink += "<a class=\"page-home\" href=\"/\">Safevietnam</a> | ";
                stLink += "" + entity.Name + "";
            }
            ViewBag.Breadcrumb = stLink;
            ViewBag.CateUrl = cate.Url;
            //stLink += "<a class=\"page-home\" href=\"/\">Safevietnam</a> | ";
            //stLink += "" + entity.Name + "";
            //ViewBag.Breadcrumb = stLink;
            ViewBag.CateUrl = "/" + cate.Url;
            return PartialView(entity);
        }

        public PartialViewResult Categories(string stUrl, int? page)
        {
            var entity = CATEGORIES.getByUrl(stUrl);
            var child = CATEGORIES.getByParentId(entity.Id);
            if (entity != null)
            {
                var checkIntroPages = CATEGORIES.CategoryGetByParent(entity.Id);
                ViewBag.GetTaxanomyCategories = entity.taxanomy;
                ViewBag.GetTitleCategories = entity.Title;
                ViewBag.GetParentIdCategories = "" + checkIntroPages.Count() + "";
            }
            return PartialView(child);
        }

        public PartialViewResult PageByCategories(string stUrl, int? page, int? year, string key, long? categoriesid, string tacgia, string tochuc)
        {
            ViewBag.SelectCategories = CATEGORIES.GetCategoriesSelectList();
            int pageNum = page ?? 1;
            var entity = CATEGORIES.getByUrl(stUrl);
            var pages = PAGES.getByCategoriesId(entity.Id);
            if (!string.IsNullOrEmpty(key))
            {
                pages = pages.Where(x => x.Name.ToLower().Contains(key.ToLower()));
            }
            if (year > 0)
            {
                pages = pages.Where(x => x.ModifiedDate.Year == year || x.CreateDate.Year == year);
            }
            if (categoriesid > 0)
            {
                pages = pages.Where(x => x.CategoriesId == categoriesid);
            }
            if (!string.IsNullOrEmpty(tacgia))
            {
                pages = pages.Where(x => x.TacGia.ToLower().Contains(tacgia.ToLower()));
            }
            if (!string.IsNullOrEmpty(tochuc))
            {
                pages = pages.Where(x => x.ToChuc.ToLower().Contains(tochuc.ToLower()));
            }
            string stLink = "";
            if (entity != null)
            {
                stLink += "<a class=\"page-home\" href=\"/\">Safevietnam</a>";
                stLink += getLinkParentCategories("", entity.ParentId);
                stLink += " | " + entity.Name + "";
            }
            else
            {
                stLink += "<a class=\"page-home\" href=\"/\">Safevietnam</a>";
            }
            ViewBag.Breadcrumb = stLink;
            ViewBag.Title = entity.Name;
            ViewBag.stUrl = stUrl;
            ViewBag.Keyword = key;
            ViewBag.Year = year;
            ViewBag.BoxCategories = entity.taxanomy;
            if (entity.ParentId != null)
            {
                ViewBag.CategoriesName = CATEGORIES.getById(entity.ParentId).Name;
                ViewBag.CategoriesId = entity.ParentId;
            }
            else
            {
                ViewBag.CategoriesId = entity.Id;
                ViewBag.CategoriesName = CATEGORIES.getById(entity.Id).Name;
            }
            return PartialView(pages.ToPagedList(pageNum, 20));
        }

        public PartialViewResult getBoxCategories(long? Id)
        {
            var entity = CATEGORIES.getByParentId(Id);
            return PartialView(entity);
        }

        public PartialViewResult GetBoxCategories_LEFTMENU_1(long? Id)
        {
            var entity = CATEGORIES.GetCategories_LEFTMENU_2("LEFTMENU_2", Id); // cho dạng bài viết khác chuyên mục TNGT
            return PartialView(entity);
        }
        public PartialViewResult GetBoxCategories_LEFTMENU_2()
        {
            var entity = CATEGORIES.GetCategories_LEFTMENU_2("LEFTMENU_1", 0); // chưa sử dụng
            return PartialView(entity);
        }


        public PartialViewResult getLatestNews()
        {
            var entity = PAGES.getLatest().Take(2).ToList();
            return PartialView(entity);
        }
        public PartialViewResult NewRelated(long Id, long CateId, string Taxanomy)
        {
            var entity = new List<Page>();
            //entity = PAGES.getByCategoriesId(CateId).Where(x => x.Id != Id).Take(2).ToList();
            entity = PAGES.getByCategoriesIdTaxanomy(CateId, Taxanomy).Where(x => x.Id != Id).Take(5).ToList();
            return PartialView(entity);
        }
        public PartialViewResult Comment(long PageId)
        {
            var entity = COMMENTS.getByPageId(PageId);
            ViewBag.ListComment = entity.ToList();
            var output = new ModelComments();
            output.PageId = PageId;
            output.Captcha = CommonsHelper.genCaptchar();
            return PartialView(output);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public PartialViewResult Comment(ModelComments input)
        {
            if (ModelState.IsValid)
            {
                var entity = new Comment();
                entity.Email = input.Email;
                entity.FullName = input.FullName;
                entity.Messager = input.ContentComment;
                entity.PageId = input.PageId;
                entity.Status = 0;
                entity.CreateDate = DateTime.Now;
                COMMENTS.Insert(entity);
                input = new ModelComments();
                input.PageId = entity.PageId;
                input.Captcha = CommonsHelper.genCaptchar();
                return PartialView(input);
            }
            return PartialView(input);
        }

        public ActionResult PageError()
        {
            return PartialView();
        }

        public ActionResult Tags(long? PageId)
        {
            var entity = new List<Tag>();
            entity = TAGS.ListTagsGetByPageId(PageId);
            return PartialView(entity);
        }
        public int CountTags(string nametags)
        {
            int count = 0;
            try
            {
                if (string.IsNullOrEmpty(nametags))
                    count = 0;
                else
                    count = TAGS.CountTags(nametags);
                return count;
            }
            catch
            {
                return 0;
            }
        }

        public PartialViewResult PageInCategories(long Id)
        {
            var entity = new Page();
            entity = PAGES.getFistByCategoriesId(Id);
            var cate = CATEGORIES.getById(Id);
            ViewBag.Name = cate.Name;
            ViewBag.Url = cate.Url;
            return PartialView(entity);
        }

        public PartialViewResult PageRelated(long Id, long CateId)
        {
            var entity = new List<Page>();
            entity = PAGES.getByCategoriesId(CateId).Where(x => x.Id != Id).Take(2).ToList();
            return PartialView(entity);
        }

        public PartialViewResult SlideShows()
        {
            var entity = PAGES.getSlideShows();
            return PartialView(entity);
        }

        public PartialViewResult Events()
        {
            var entity = PAGES.getEvents();
            return PartialView(entity);
        }

        private string getLinkParentCategories(string stLink, long? Id)
        {
            var cate = CATEGORIES.getById(Id);
            if (cate != null)
            {
                stLink += getLinkParentCategories(stLink, cate.ParentId);
                stLink += " | <a href=\"" + cate.Url + "\">" + cate.Name + "</a>";
            }
            return stLink;
        }
        private string getLinkParentCategories_Fix(string stLink, long? Id)
        {
            var cate = CATEGORIES.getById(Id);
            if (cate != null)
            {
                stLink += "<a href=\"" + cate.Url + "\">" + cate.Name + "</a>";
            }
            return stLink;
        }

        public ActionResult Search(string key, int? page)
        {
            if (Session["GetKeyHome"] == null)
                Session.Add("GetKeyHome", key);
            int pageNum = page ?? 1;
            var list = PAGES.getAll();
            if (!string.IsNullOrEmpty(key))
            {
                try
                {
                    list = list.Where(x => x.Name.ToLower().Contains(key.ToLower())).ToList();
                }
                catch
                {
                    list = new List<Page>();
                }
            }
            ViewBag.Keyword = Session["GetKeyHome"].ToString();
            return View(list.ToPagedList(pageNum, 20));
        }
        public ActionResult SendMail()
        {
            string request = Request.QueryString["page"];
            if (!string.IsNullOrEmpty(request))
            {
                try
                {
                    var pages = PAGES.getByUrl(request);
                    ModelSendMails entity = new ModelSendMails();
                    entity.PageTitle = "<a class=\"link\" href=\"http://safevietnam.org.vn/" + request + "\">" + pages.Name + "</a>";
                    var st = "<a class=\"page-home\" href=\"/\">Safevietnam</a> | ";
                    st += "Gửi Thư";
                    ViewBag.Breadcrumb = st;
                    return View(entity);
                }
                catch
                {
                    return RedirectToAction("Index");
                }

            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult SendMail(ModelSendMails input)
        {
            if (ModelState.IsValid)
            {
                string bodyFrom = "NGƯỜI GỬI: " + input.FullName +
                                   "<br/>EMAIL GỬI:" + input.MailFrom +
                                  "<br/>TIÊU ĐỀ THƯ:" + input.Subject +
                                  "<br/>TÊN TRANG: " + input.PageTitle +
                                  "<br/>NỘI DUNG: " + input.Content;
                int send = CommonsHelper.SendEmailSystem(input.MailTo, input.MailFrom, input.Subject, bodyFrom);
                if (send == 1)
                {
                    try
                    {
                        LogSystemService logService = new LogSystemService();
                        var logs = new LogSystem();
                        logs.IPAddress = CommonsHelper.GetIpAddress;
                        logs.CreateDate = DateTime.Now;
                        logs.Messenger = "[SENDMAIL_FONTEND]|NGUOIGUI:" + input.FullName + "|EMAIL:" + input.MailFrom +
                                         "|TIEUDE:" + input.Subject + "|NGUOINHAN:" + input.MailTo + "|PAGE:" +
                                         input.PageTitle + "|CONTENT:" +
                                         input.Content;
                        logs.Status = false;
                        logService.Insert(logs);
                    }
                    catch
                    {
                    }
                    ModelState.AddModelError("", "Gửi thông tin thành công!");
                }
                return View(input);
            }
            return View();
        }
        public ActionResult SearchTags(string tag, int? page)
        {
            if (Session["GetKeyTag"] == null)
                Session.Add("GetKeyTag", tag);
            int pageNum = page ?? 1;
            var list = PAGES.GetSearchTags(tag);
            //if (!string.IsNullOrEmpty(key))
            //{
            //    try
            //    {
            //        list = list.Where(x => x.Name.ToLower().Contains(key.ToLower())).ToList();
            //    }
            //    catch
            //    {
            //        list = new List<Page>();
            //    }
            //}
            if (Session["GetKeyTag"] != null)
                ViewBag.Keyword = Session["GetKeyTag"].ToString();
            return View(list.ToPagedList(pageNum, 20));
        }

        public ActionResult SearchInCategories(int year, long categoriesid, string key, int? page)
        {
            int pageNum = page ?? 1;
            var list = PAGES.getByCategoriesId(categoriesid);
            if (!string.IsNullOrEmpty(key))
            {
                list = list.Where(x => x.ModifiedDate.Year == year && (x.Name.ToLower().Contains(key.ToLower()) || x.Title.ToLower().Contains(key.ToLower())));
            }
            return View(list.ToPagedList(pageNum, 20));
        }
    }
}