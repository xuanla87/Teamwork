using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                    _db.Entry(entity).State = EntityState.Modified;
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
