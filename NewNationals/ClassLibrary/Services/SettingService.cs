using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary.Models;

namespace ClassLibrary.Services
{
    public interface ISettingService
    {
        bool Insert(Setting entity);
        bool Update(Setting entity);
        bool Delete(int id);

    }
    public class SettingService : ISettingService
    {
        private readonly DataContext _db = null;

        public SettingService()
        {
            _db = new DataContext();
        }

        /// <summary>
        /// Hàm thực hiện lệnh insert dữ liệu
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Insert(Setting entity)
        {
            try
            {
                _db.Settings.Add(entity);
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
        public bool Update(Setting entity)
        {
            try
            {
                var setting = _db.Settings.FirstOrDefault(x => x.Id == entity.Id);
                if (setting != null)
                {
                    _db.Entry(setting).State = EntityState.Detached;
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
                var setting = _db.Settings.Find(id);
                if (setting != null)
                {
                    _db.Settings.Remove(setting);
                    _db.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public Setting GetSettings(string key)
        {
            return _db.Settings.Where(x => x.stKey == key && x.Status == true).FirstOrDefault();
        }

        public List<Setting> GetAll()
        {
            return _db.Settings.ToList();
        }
        /// <summary>
        /// Lưu giá trị theo stKey
        /// </summary>
        /// <param name="stKey"></param>
        /// <param name="stValue"></param>
        /// <returns></returns>
        public bool saveValue(string stKey, string stValue)
        {
            try
            {
                Setting entity = _db.Settings.FirstOrDefault(x => x.stKey == stKey);
                if (entity != null)
                {
                    entity.stValue = stValue;
                    _db.Entry(entity).State = EntityState.Modified;
                    _db.SaveChanges();
                }
                else
                {
                    entity = new Setting();
                    entity.stKey = stKey;
                    entity.stValue = stValue;
                    entity.Status = true;
                    Insert(entity);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// Lấy dữ liệu theo stKey
        /// </summary>
        /// <param name="stKey"></param>
        /// <returns></returns>
        public string getValue(string stKey)
        {
            try
            {
                Setting entity = _db.Settings.FirstOrDefault(x => x.stKey == stKey);
                return entity.stValue;
            }
            catch
            {
                return null;
            }
        }
    }
}
