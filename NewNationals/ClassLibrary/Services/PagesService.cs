using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI.WebControls;
using ClassLibrary.Commons;
using ClassLibrary.Models;

namespace ClassLibrary.Services
{
    public interface IPagesService
    {
        bool Insert(Page entity);
        bool Update(Page entity);
        bool Delete(Page entity);
    }
    public class PagesService: IPagesService
    {
        private readonly DataContext _db = null;

        public PagesService()
        {
            _db = new DataContext();
        }

        /// <summary>
        /// Hàm thực hiện lệnh insert dữ liệu
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Insert(Page entity)
        {
            try
            {
                _db.Pages.Add(entity);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                LogSystemService logService = new LogSystemService();
                var logs = new LogSystem();
                logs.IPAddress = CommonsHelper.GetIpAddress;
                logs.CreateDate = DateTime.Now;
                logs.Messenger = "Tài khoản: " + HttpContext.Current.Session[CommonsHelper.SessionAdminCp] + " [Lỗi Insert Bài viết]" +
                                 ex.ToString();
                logs.Status = false;
                logService.Insert(logs);
                return false;
            }
        }

        /// <summary>
        /// Hàm thực hiện lệnh Update dữ liệu
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Update(Page entity)
        {
            try
            {
                var getpage = _db.Pages.FirstOrDefault(x => x.Id == entity.Id);
                if (getpage != null)
                {
                    _db.Entry(getpage).State = EntityState.Detached;
                }
                _db.Entry(entity).State = EntityState.Modified;
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                LogSystemService logService = new LogSystemService();
                var logs = new LogSystem();
                logs.IPAddress = CommonsHelper.GetIpAddress;
                logs.CreateDate = DateTime.Now;
                logs.Messenger = "Tài khoản: " + HttpContext.Current.Session[CommonsHelper.SessionAdminCp] + " [Lỗi Cập nhật Page]" +
                                 ex.ToString();
                logs.Status = false;
                logService.Insert(logs);
                return false;
            }
        }

        /// <summary>
        /// Hàm thực hiện lệnh Delete
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Delete(Page entity)
        {
            try
            {
                var page = _db.Pages.Find(entity.Id);
                if (page != null)
                {
                    page.Status = -1;
                    _db.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                LogSystemService logService = new LogSystemService();
                var logs = new LogSystem();
                logs.IPAddress = CommonsHelper.GetIpAddress;
                logs.CreateDate = DateTime.Now;
                logs.Messenger = "Tài khoản: " + HttpContext.Current.Session[CommonsHelper.SessionAdminCp] + " [Lỗi Delete Page]" +
                                 ex.ToString();
                logs.Status = false;
                logService.Insert(logs);
                return false;
            }
        }
        /// <summary>
        /// Hàm thực hiện lệnh Delete
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool UpdateUrl(long id,string url)
        {
            try
            {
                var page = _db.Pages.Find(id);
                if (page != null)
                {
                    page.Url = url;
                    _db.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                LogSystemService logService = new LogSystemService();
                var logs = new LogSystem();
                logs.IPAddress = CommonsHelper.GetIpAddress;
                logs.CreateDate = DateTime.Now;
                logs.Messenger = "Tài khoản: " + HttpContext.Current.Session[CommonsHelper.SessionAdminCp] + " [Lỗi Update lại Url Page]" +
                                 ex.ToString();
                logs.Status = false;
                logService.Insert(logs);
                return false;
            }
        }
        /// <summary>
        /// thuc thi update bản ghi
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool UpdateStatus(int pagid, int status)
        {
            try
            {
                var entity = _db.Pages.Find(pagid);
                if (entity != null)
                {
                    if (status == 0)
                        entity.Status = 1;
                    if (status == 1)
                        entity.Status = 0;
                    _db.SaveChanges();
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                LogSystemService logService = new LogSystemService();
                var logs = new LogSystem();
                logs.IPAddress = CommonsHelper.GetIpAddress;
                logs.CreateDate = DateTime.Now;
                logs.Messenger = "Tài khoản: " + HttpContext.Current.Session[CommonsHelper.SessionAdminCp] + " [Lỗi Cập nhật Status Page]" +
                                 ex.ToString();
                logs.Status = false;
                logService.Insert(logs);
                return false;
            }
        }
        /// <summary>
        /// hàm trả về danh sách tất cả bản ghi
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Page> ListAllPage()
        {
            return _db.Pages.Where(x => x.Status != -1).OrderByDescending(x => x.CreateDate).ToList();
        }

        /// <summary>
        /// hàm trả về 1 bản ghi với id truyền vào
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Page GetPageById(long id)
        {
            return _db.Pages.SingleOrDefault(x => x.Id == id);
        }
        public List<Page> GetPageAutoComplete(string input)
        {
            return _db.Pages.Where(x => (x.Url.Contains(input) || x.Name.Contains(input))).ToList();
        }
    }
}
