﻿using ClassLibrary.Commons;
using ClassLibrary.Models;
using ClassLibrary.Services;
using NewNationals.Models;
using PagedList;
using System;
using System.Collections.Generic;
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
        PageMetaService PAGEMETAS=new PageMetaService();
        public ActionResult Index()
        {
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
            ViewBag.CategoriesId = cate.ParentId;
            if (cate.ParentId != null)
                ViewBag.CategoriesName = CATEGORIES.getById(cate.ParentId).Name;
            else
            {
                ViewBag.CategoriesName = CATEGORIES.getById(cate.Id).Name;
            }
            return PartialView(entity);
        }

        public PartialViewResult DetailPage(string stUrl)
        {
            var entity = new Page();
            entity = PAGES.getByUrl(stUrl);
            string stLink = "";
            stLink += "<a class=\"page-home\" href=\"/\">Safevietnam</a> | ";
            stLink += "" + entity.Name + "";
            ViewBag.Breadcrumb = stLink;
            return PartialView(entity);
        }

        public PartialViewResult Categories(string stUrl, int? page)
        {
            var entity = CATEGORIES.getByUrl(stUrl);
            var child = CATEGORIES.getByParentId(entity.Id);
            return PartialView(child);
        }

        public PartialViewResult PageByCategories(string stUrl, int? page, int? year, string key)
        {
            int pageNum = page ?? 1;
            var entity = CATEGORIES.getByUrl(stUrl);
            var pages = PAGES.getByCategoriesId(entity.Id);
            if (!string.IsNullOrEmpty(key))
            {
                pages = pages.Where(x => x.Name.ToLower().Contains(key.ToLower()) || x.Title.ToLower().Contains(key.ToLower()));
            }
            if (year > 0)
            {
                pages = pages.Where(x => x.ModifiedDate.Year == year || x.CreateDate.Year == year);
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
            ViewBag.CategoriesId = entity.ParentId;
            if (entity.ParentId != null)
                ViewBag.CategoriesName = CATEGORIES.getById(entity.ParentId).Name;
            else
            {
                ViewBag.CategoriesName = CATEGORIES.getById(entity.Id).Name;
            }
            return PartialView(pages.ToPagedList(pageNum, 20));
        }

        public PartialViewResult getBoxCategories(long? Id)
        {
            var entity = CATEGORIES.getByParentId(Id);
            return PartialView(entity);
        }

        public PartialViewResult getLatestNews()
        {
            var entity = PAGES.getLatest().Take(2).ToList();
            return PartialView(entity);
        }
        public PartialViewResult NewRelated(long Id, long CateId)
        {
            var entity = new List<Page>();
            entity = PAGES.getByCategoriesId(CateId).Where(x => x.Id != Id).Take(5).ToList();
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

        public ActionResult SendMail()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SendMail(ModelSendMails input)
        {
            if (ModelState.IsValid)
            {
                return View(input);
            }
            return View(input);
        }
        public ActionResult PageError()
        {
            return PartialView();
        }

        public ActionResult Tags(string slug)
        {
            return PartialView();
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

        public ActionResult Search(string key, int? page)
        {
            int pageNum = page ?? 1;
            var list = PAGES.getAll();
            if (!string.IsNullOrEmpty(key))
            {
                list = list.Where(x => x.Name.ToLower().Contains(key.ToLower()) || x.Title.ToLower().Contains(key.ToLower()));
            }
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