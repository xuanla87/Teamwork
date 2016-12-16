using ClassLibrary.Services;
using NewNationals.Areas.AdminControlPanel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace NewNationals.Areas.AdminControlPanel.Controllers
{
    public class SystemSettingController : Controller
    {
        SettingService SETTING = new SettingService();
        [HttpGet]
        public ActionResult Index()
        {
            ModelSystems entity = new ModelSystems();
            entity.HomeBoxFirst = SETTING.getValue("HomeBoxFirst");
            entity.HomeBoxSecond = SETTING.getValue("HomeBoxSecond");
            entity.HomeBoxThird = SETTING.getValue("HomeBoxThird");
            entity.HomeBoxFourth = SETTING.getValue("HomeBoxFourth");
            entity.HomeBoxFifth = SETTING.getValue("HomeBoxFifth");
            entity.HomeBoxSixth = SETTING.getValue("HomeBoxSixth");
            entity.FooterInfo = SETTING.getValue("Footer_Info");
            return View(entity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Index(ModelSystems entity)
        {
            if (ModelState.IsValid)
            {
                SETTING.saveValue("HomeBoxFirst", entity.HomeBoxFirst);
                SETTING.saveValue("HomeBoxSecond", entity.HomeBoxSecond);
                SETTING.saveValue("HomeBoxThird", entity.HomeBoxThird);
                SETTING.saveValue("HomeBoxFourth", entity.HomeBoxFourth);
                SETTING.saveValue("HomeBoxFifth", entity.HomeBoxFifth);
                SETTING.saveValue("HomeBoxSixth", entity.HomeBoxSixth);
                SETTING.saveValue("Footer_Info", entity.FooterInfo); 
            }
            return View(entity);
        }
    }
}