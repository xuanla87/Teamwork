using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary.Models;

namespace ClassLibrary.Services
{
    public interface IUserService
    {
        bool Insert(User entity);
        bool Update(User entity);
        bool Delete(int id);

    }
    public class UserService: IUserService
    {
        private readonly DataContext _db = null;

        public UserService()
        {
            _db = new DataContext();
        }

        /// <summary>
        /// Hàm thực hiện lệnh insert dữ liệu
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Insert(User entity)
        {
            try
            {
                _db.Users.Add(entity);
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
        public bool Update(User entity)
        {
            try
            {
                var user = _db.Users.FirstOrDefault(x => x.Id == entity.Id);
                if (user != null)
                {
                    _db.Entry(user).State = EntityState.Detached;
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
                var user = _db.Users.Find(id);
                if (user != null)
                {
                    _db.Users.Remove(user);
                    _db.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        /// hàm kiểm tra đăng nhập
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public int CheckLogin(string username, string password)
        {
            var ad = _db.Users.SingleOrDefault(x => x.UserName == username && x.Status == 1);
            if (ad == null)
                return 0;
            else
            {
                if (ad.UserPassword == password)
                    return 1;
                else
                    return 0;
            }
        }
        /// <summary>
        ///  Kiểm tra UserName có tồn tại không
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public bool UserNameExits(string input)
        {
            return _db.Users.Any(x => x.UserName == input);
        }
        /// <summary>
        /// Hàm trả về danh sách tất cả bản ghi
        /// </summary>
        /// <returns></returns>
        public List<User> ListAllUser()
        {
            return _db.Users.ToList();
        }

        /// <summary>
        /// Hàm trả về 1 bản ghi trong Admin với ĐK là id truyền vào
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public User GetUserById(int id)
        {
            return _db.Users.SingleOrDefault(x => x.Id == id);
        }
        /// <summary>
        /// Hàm trả về 1 bản ghi trong Admin với ĐK là username truyền vào
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public User GetUserByUserName(string username)
        {
            return _db.Users.SingleOrDefault(x => x.UserName == username);
        }
    }
}
