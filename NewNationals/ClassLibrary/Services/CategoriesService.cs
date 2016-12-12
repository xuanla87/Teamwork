using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary.Models;

namespace ClassLibrary.Services
{
    public interface ICategoriesService
    {
        bool Insert(Category entity);
        bool Update(Category entity);
        bool Delete(int cateId);
    }
    public class CategoriesService:ICategoriesService
    {
        private readonly DataContext _db = null;

        public CategoriesService()
        {
            _db = new DataContext();
        }

        /// <summary>
        /// Hàm thực hiện lệnh insert dữ liệu
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Insert(Category entity)
        {
            try
            {
                _db.Categories.Add(entity);
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
        public bool Update(Category entity)
        {
            try
            {
                var getcate = _db.Categories.FirstOrDefault(x => x.Id == entity.Id);
                if (getcate != null)
                {
                    _db.Entry(getcate).State = EntityState.Detached;
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
        public bool Delete(int cateId)
        {
            try
            {
                var category = _db.Categories.Find(cateId);
                if (category != null)
                {
                    _db.Categories.Remove(category);
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
