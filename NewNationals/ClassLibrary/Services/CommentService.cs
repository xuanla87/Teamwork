using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using ClassLibrary.Commons;
using ClassLibrary.Models;

namespace ClassLibrary.Services
{
    public interface ICommentService
    {
        bool Insert(Comment entity);
        bool Update(Comment entity);
        bool Delete(long id);

    }
    public class CommentService : ICommentService
    {
        private readonly DataContext _db = null;

        public CommentService()
        {
            _db = new DataContext();
        }

        /// <summary>
        /// Hàm thực hiện lệnh insert dữ liệu
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Insert(Comment entity)
        {
            try
            {
                _db.Comments.Add(entity);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                LogSystemService logService = new LogSystemService();
                var logs = new LogSystem();
                logs.IPAddress = CommonsHelper.GetIpAddress;
                logs.CreateDate = DateTime.Now;
                logs.Messenger = "Tài khoản: " + HttpContext.Current.Session[CommonsHelper.SessionAdminCp] + " [Lỗi Insert Comment]" +
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
        public bool Update(Comment entity)
        {
            try
            {
                var tag = _db.Comments.FirstOrDefault(x => x.Id == entity.Id);
                if (tag != null)
                {
                    _db.Entry(tag).State = EntityState.Detached;
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
                logs.Messenger = "Tài khoản: " + HttpContext.Current.Session[CommonsHelper.SessionAdminCp] + " [Lỗi Update Comment]" +
                                 ex.ToString();
                logs.Status = false;
                logService.Insert(logs);
                return false;
            }
        }

        /// <summary>
        /// Hàm thực hiện lệnh Delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(long id)
        {
            try
            {
                var comment = _db.Comments.Find(id);
                if (comment != null)
                {
                    comment.Status = -1;
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
                logs.Messenger = "Tài khoản: " + HttpContext.Current.Session[CommonsHelper.SessionAdminCp] + " [Lỗi Delete Comment]" +
                                 ex.ToString();
                logs.Status = false;
                logService.Insert(logs);
                return false;
            }
        }
        /// <summary>
        /// Hàm thực hiện lệnh Delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool UpdateStatus(long id, int status)
        {
            try
            {
                var comment = _db.Comments.Find(id);
                if (comment != null)
                {
                    comment.Status = status;
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
                logs.Messenger = "Tài khoản: " + HttpContext.Current.Session[CommonsHelper.SessionAdminCp] + " [Lỗi Delete Comment]" +
                                 ex.ToString();
                logs.Status = false;
                logService.Insert(logs);
                return false;
            }
        }
        /// <summary>
        /// Hàm trả về danh sách tất cả bản ghi với Id truyền vào
        /// </summary>
        /// <returns></returns>
        public List<Comment> ListComemnt(long id)
        {
            return _db.Comments.Where(x => x.Id == id).ToList();
        }
        /// <summary>
        /// Hàm trả về danh sách tất cả bản ghi
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Comment> ListAllComment()
        {
            return _db.Comments.Where(x => x.Status != -1).OrderByDescending(x => x.CreateDate).ToList();
        }

        /// <summary>
        /// Hàm trả về 1 bản ghi trong Comment với ĐK là id truyền vào
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public Comment GetCommentById(long id)
        {
            return _db.Comments.SingleOrDefault(x => x.Id == id);
        }
        public int CountComment()
        {
            return _db.Comments.Where(x => x.Status == 0).Count();
        }
        public int CountPage()
        {
            return _db.Pages.Where(x => x.Status != -1).Count();
        }
        public int CountUser()
        {
            return _db.Users.Where(x => x.Status != -1).Count();
        }
        public int CountCategory()
        {
            return _db.Categories.Where(x => x.Status != -1).Count();
        }
        public long CountLog()
        {
            return _db.LogSystem.Count();
        }
        public int CountSetting()
        {
            return _db.Settings.Where(x => x.stValue != null).Count();
        }
        public int CountMenu()
        {
            return _db.Menus.Count();
        }

        public IEnumerable<Comment> getByPageId(long PageId)
        {
            return _db.Comments.Where(x => x.PageId == PageId && x.Status == 1).OrderByDescending(x => x.CreateDate);
        }
    }
}
