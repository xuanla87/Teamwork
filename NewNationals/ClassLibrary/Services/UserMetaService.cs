using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary.Models;

namespace ClassLibrary.Services
{
    public interface IUserMetaService
    {
        bool Insert(UserMeta entity);
        bool Update(UserMeta entity);
        bool Delete(int id);

    }
    public class UserMetaService: IUserMetaService
    {
        private readonly DataContext _db = null;

        public UserMetaService()
        {
            _db = new DataContext();
        }

        /// <summary>
        /// Hàm thực hiện lệnh insert dữ liệu
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Insert(UserMeta entity)
        {
            try
            {
                _db.UserMetas.Add(entity);
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
        public bool Update(UserMeta entity)
        {
            try
            {
                var usermeta = _db.UserMetas.FirstOrDefault(x => x.Id == entity.Id);
                if (usermeta != null)
                {
                    _db.Entry(usermeta).State = EntityState.Detached;
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
                var usermeta = _db.UserMetas.Find(id);
                if (usermeta != null)
                {
                    _db.UserMetas.Remove(usermeta);
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
