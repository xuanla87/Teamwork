using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using ClassLibrary.Commons;
using ClassLibrary.Models;

namespace ClassLibrary.Services
{
    public interface IRoleService
    {
        bool Insert(Role entity);
        bool Update(Role entity);
        bool Delete(int id);

    }
    public class RoleService:IRoleService
    {
        private readonly DataContext _db = null;

        public RoleService()
        {
            _db = new DataContext();
        }

        /// <summary>
        /// Hàm thực hiện lệnh insert dữ liệu
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Insert(Role entity)
        {
            try
            {
                _db.Roles.Add(entity);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                LogSystemService logService = new LogSystemService();
                var logs = new LogSystem();
                logs.IPAddress = CommonsHelper.GetIpAddress;
                logs.CreateDate = DateTime.Now;
                logs.Messenger = "Tài khoản: " + HttpContext.Current.Session[CommonsHelper.SessionAdminCp] + " [Lỗi Thêm mới Role]" +
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
        public bool Update(Role entity)
        {
            try
            {
                var role = _db.Roles.FirstOrDefault(x => x.Id == entity.Id);
                if (role != null)
                {
                    _db.Entry(role).State = EntityState.Detached;
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
                logs.Messenger = "Tài khoản: " + HttpContext.Current.Session[CommonsHelper.SessionAdminCp] + " [Lỗi Update Role]" +
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
                var role = _db.Roles.Find(id);
                if (role != null)
                {
                    _db.Roles.Remove(role);
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
                logs.Messenger = "Tài khoản: " + HttpContext.Current.Session[CommonsHelper.SessionAdminCp] + " [Lỗi Delete Role]" +
                                 ex.ToString();
                logs.Status = false;
                logService.Insert(logs);
                return false;
            }
        }

        public IEnumerable<Role> ListAllRole()
        {
            return _db.Roles.ToList();
        }
        /// <summary>
        ///  Kiểm tra quyền có tồn tại không
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public bool RoleNameExits(string input)
        {
            return _db.Roles.Any(x => x.RoleName == input);
        }

        /// <summary>
        /// Hàm trả về 1 bản ghi trong Role với ID truyền vào
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Role GetRoleById(int? id)
        {
            return _db.Roles.SingleOrDefault(x => x.Id == id);
        }
        /// <summary>
        /// hàm trả về danh sách Category có ParentID=NULL và trạng thái != -1
        /// trạng thái =-1 có nghĩa là bản ghi đã bị xóa không hiển thị trên he thống
        /// </summary>
        /// <returns></returns>
        public List<Role> GetRoleList()
        {
            return _db.Roles.ToList();
        }
    }
}
