using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary.Models;

namespace ClassLibrary.Services
{
    public interface IPageMetaService
    {
        bool Insert(PageMeta entity);
        bool Update(PageMeta entity);
        bool Delete(int id);

    }
    public class PageMetaService:IPageMetaService
    {
        private readonly DataContext _db = null;

        public PageMetaService()
        {
            _db = new DataContext();
        }

        /// <summary>
        /// Hàm thực hiện lệnh insert dữ liệu
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Insert(PageMeta entity)
        {
            try
            {
                _db.PageMetas.Add(entity);
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
        public bool Update(PageMeta entity)
        {
            try
            {
                var getpagemeta = _db.PageMetas.FirstOrDefault(x => x.Id == entity.Id);
                if (getpagemeta != null)
                {
                    _db.Entry(getpagemeta).State = EntityState.Detached;
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
                var pagemeta = _db.PageMetas.Find(id);
                if (pagemeta != null)
                {
                    _db.PageMetas.Remove(pagemeta);
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
