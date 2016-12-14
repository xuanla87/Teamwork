using ClassLibrary.Models;
using ClassLibrary.Services;
using NewNationals.Models;
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
        public ActionResult Index()
        {
            var listCate = CATEGORIES.getTopCategory(6).ToList();
            return PartialView(listCate);
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
                        if (!string.IsNullOrEmpty(content.Title))
                            entity.Title = content.Title;
                        else
                            entity.Title = content.Name;
                        entity.Keywords = content.Keywords;
                        entity.Descriptions = content.Description;
                        entity.ActionLink = content.Taxanomy;
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
                stLink += "<a class=\"page-home\" href=\"/\">Trang chủ</a>";
                stLink += " | <a href=\"" + cate.Url + "\">" + cate.Name + "</a> | ";
                stLink += "" + entity.Name + "";
            }
            else
            {
                stLink += "<a class=\"page-home\" href=\"/\">Trang chủ</a> | ";
                stLink += "" + entity.Name + "";
            }
            ViewBag.Breadcrumb = stLink;
            return PartialView(entity);
        }

        public ActionResult Details(string slug)
        {
            return PartialView();
        }

        public ActionResult Categories(string slug, int? page)
        {
            return PartialView();
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
    }
}