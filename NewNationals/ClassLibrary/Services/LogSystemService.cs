using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary.Models;

namespace ClassLibrary.Services
{
    public interface ILogSystemService
    {
        bool Insert(LogSystem entity);
        bool Update(LogSystem entity);
        bool Delete(long logid);
    }
    public class LogSystemService : ILogSystemService
    {
        private readonly DataContext _db = null;
        public LogSystemService()
        {
            _db = new DataContext();
        }

        public bool Insert(LogSystem entity)
        {
            try
            {
                _db.LogSystem.Add(entity);
                _db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(LogSystem entity)
        {
            try
            {
                _db.Entry(entity).State = EntityState.Modified;
                _db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(long logid)
        {
            try
            {
                var getLogSystem = _db.LogSystem.Find(logid);
                _db.LogSystem.Remove(getLogSystem);
                _db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool UpdateStatus(int Id, bool input)
        {
            try
            {
                var entity = _db.LogSystem.Find(Id);
                if (input)
                    entity.Status = false;
                else
                    entity.Status = true;
                _db.Entry(entity).State = EntityState.Modified;
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public List<LogSystem> ListAll()
        {
            return _db.LogSystem.ToList();
        }
        public IEnumerable<LogSystem> GetByListAllOrderDesc()
        {
            return _db.LogSystem.OrderByDescending(x => x.CreateDate).ToList();
        }

        public LogSystem GetIpAddress(string iPinput)
        {
            return _db.LogSystem.Where(x => x.IPAddress == iPinput).OrderByDescending(x => x.CreateDate).FirstOrDefault();
        }
    }
}

