using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary.Models;

namespace ClassLibrary.Services
{
    public interface ISlidehowService
    {
        bool Insert(Slidehow entity);
        bool Update(Slidehow entity);
        bool Delete(int id);

    }
    public class SlidehowService: ISlidehowService
    {
        private readonly DataContext _db = null;

        public SlidehowService()
        {
            _db = new DataContext();
        }

        /// <summary>
        /// Hàm thực hiện lệnh insert dữ liệu
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Insert(Slidehow entity)
        {
            try
            {
                _db.Slidehows.Add(entity);
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
        public bool Update(Slidehow entity)
        {
            try
            {
                var slideshow = _db.Slidehows.FirstOrDefault(x => x.Id == entity.Id);
                if (slideshow != null)
                {
                    _db.Entry(slideshow).State = EntityState.Detached;
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
                var slide = _db.Slidehows.Find(id);
                if (slide != null)
                {
                    _db.Slidehows.Remove(slide);
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
