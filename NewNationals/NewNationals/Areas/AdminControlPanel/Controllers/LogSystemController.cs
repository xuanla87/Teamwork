using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClassLibrary.Services;
using NewNationals.Areas.AdminControlPanel.Models;
using PagedList;

namespace NewNationals.Areas.AdminControlPanel.Controllers
{
    public class LogSystemController : BaseController
    {
        // GET: AdminControlPanel/LogSystem
        public ActionResult Index()
        {
            return View();
        }
        LogSystemService logService = new LogSystemService();

        public ActionResult ListLogSystem(int? page, string SearchString, string FromDate, string ToDate)
        {
            int pageNum = page ?? 1;
            var showlist = logService.GetByListAllOrderDesc();
            var listlog = new List<LogSystems>();
            foreach (var item in showlist)
            {
                listlog.Add(new LogSystems()
                {
                    LogId = item.LogId,
                    CreateDate = item.CreateDate,
                    IPAddress = item.IPAddress,
                    Messenger = item.Messenger,
                    Status = item.Status
                });
            }
            if (!string.IsNullOrEmpty(SearchString))
            {
                showlist = showlist.Where(x => x.Messenger.Contains(SearchString) || x.IPAddress.Contains(SearchString));
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
    }
}