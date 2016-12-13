using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using ClassLibrary.Commons;
using ClassLibrary.Models;

namespace ClassLibrary.Services
{
    public interface ITagService
    {
        bool Insert(Tag entity);
        bool Update(Tag entity);
        bool Delete(int id);

    }
    public class TagService: ITagService
    {
        private readonly DataContext _db = null;

        public TagService()
        {
            _db = new DataContext();
        }

        /// <summary>
        /// Hàm thực hiện lệnh insert dữ liệu
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Insert(Tag entity)
        {
            try
            {
                _db.Tags.Add(entity);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                LogSystemService logService = new LogSystemService();
                var logs = new LogSystem();
                logs.IPAddress = CommonsHelper.GetIpAddress;
                logs.CreateDate = DateTime.Now;
                logs.Messenger = "Tài khoản: " + HttpContext.Current.Session[CommonsHelper.SessionAdminCp] + " [Lỗi Insert Tag]" +
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
        public bool Update(Tag entity)
        {
            try
            {
                var tag = _db.Tags.FirstOrDefault(x => x.PageId == entity.PageId);
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
                logs.Messenger = "Tài khoản: " + HttpContext.Current.Session[CommonsHelper.SessionAdminCp] + " [Lỗi Update Tag]" +
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
        public bool Delete(int id)
        {
            try
            {
                var tag = _db.Tags.Find(id);
                if (tag != null)
                {
                    _db.Tags.Remove(tag);
                    _db.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
