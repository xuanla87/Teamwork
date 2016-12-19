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
    public class SystemSettingController : BaseController
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
            entity.FooterInfo1 = SETTING.getValue("Footer_Info1");
            entity.FooterInfo2 = SETTING.getValue("Footer_Info2");
            entity.FooterInfo3 = SETTING.getValue("Footer_Info3");
            entity.THONGTIN = SETTING.getValue("THONGTIN");
            entity.RIENGTU = SETTING.getValue("RIENGTU");
            entity.DIEUKHOAN = SETTING.getValue("DIEUKHOAN");
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
                SETTING.saveValue("Footer_Info1", entity.FooterInfo1);
                SETTING.saveValue("Footer_Info2", entity.FooterInfo2);
                SETTING.saveValue("Footer_Info3", entity.FooterInfo3);
                SETTING.saveValue("THONGTIN", entity.THONGTIN);
                SETTING.saveValue("RIENGTU", entity.RIENGTU);
                SETTING.saveValue("DIEUKHOAN", entity.DIEUKHOAN);
            }
            return View(entity);
        }
    }
}