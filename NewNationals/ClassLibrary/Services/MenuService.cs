using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ClassLibrary.Commons;
using ClassLibrary.Models;

namespace ClassLibrary.Services
{
    public interface IMenuService
    {
        bool Insert(Menu entity);
        bool Update(Menu entity);
        bool Delete(int menuId);
    }

    public class MenuService: IMenuService
    {
        private readonly DataContext _db = null;

        public MenuService()
        {
            _db = new DataContext();
        }

        /// <summary>
        /// Hàm thực hiện lệnh insert dữ liệu
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Insert(Menu entity)
        {
            try
            {
                _db.Menus.Add(entity);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                LogSystemService logService = new LogSystemService();
                var logs = new LogSystem();
                logs.IPAddress = CommonsHelper.GetIpAddress;
                logs.CreateDate = DateTime.Now;
                logs.Messenger = "Tài khoản: " + HttpContext.Current.Session[CommonsHelper.SessionAdminCp] + " [Lỗi Insert Menu]" +
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
        public bool Update(Menu entity)
        {
            try
            {
                var getmenu = _db.Menus.FirstOrDefault(x => x.Id == entity.Id);
                if (getmenu != null)
                {
                    _db.Entry(getmenu).State = EntityState.Detached;
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
                logs.Messenger = "Tài khoản: " + HttpContext.Current.Session[CommonsHelper.SessionAdminCp] + " [Lỗi Update Menu]" +
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
        public bool Delete(int menuId)
        {
            try
            {
                var menu = _db.Menus.Find(menuId);
                if (menu != null)
                {
                    _db.Menus.Remove(menu);
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
                logs.Messenger = "Tài khoản: " + HttpContext.Current.Session[CommonsHelper.SessionAdminCp] + " [Lỗi Delete Menu]" +
                                 ex.ToString();
                logs.Status = false;
                logService.Insert(logs);
                return false;
            }
        }
        /// <summary>
        /// Hàm trả về danh sách tất cả bản ghi
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Menu> ListAllMenu()
        {
            return _db.Menus.OrderBy(x => x.Order).ToList();
        }
        /// <summary>
        /// tạo cấu trúc Menu
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SelectListItem> GetMenuSelectList()
        {
            var menu = _db.Menus.ToList();
            List<SelectListItem> options = new List<SelectListItem>();
            var parents = menu.Where(x => x.ParentId == null);
            foreach (var parent in parents)
            {
                options.Add(new SelectListItem()
                {
                    Value = parent.Id.ToString(),
                    Text = parent.Name
                });
                var children = menu.Where(x => x.ParentId == parent.Id);
                foreach (var child in children)
                {
                    options.Add(new SelectListItem()
                    {
                        Value = child.Id.ToString(),
                        Text = string.Format("::..{0}", child.Name)
                    });
                }
            }
            return options;
        }
        /// <summary>
        /// hàm trả về danh sách Menu có ParentID=NULL
        /// </summary>
        /// <returns></returns>
        public List<Menu> GetMenuByParent()
        {
            return _db.Menus.Where(x => x.ParentId == null).ToList();
        }
        /// <summary>
        /// Hàm trả về 1 bản ghi trong Menu với ĐK là id truyền vào
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public Menu GetMenuById(int id)
        {
            return _db.Menus.SingleOrDefault(x => x.Id == id);
        }
    }
}
