using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                return false;
            }
        }

        public IEnumerable<Role> ListAllRole()
        {
            return _db.Roles.ToList();
        }
    }
}
