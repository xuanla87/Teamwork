using ServiceChoNhan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceChoNhan.Services
{
    public interface IMemberService
    {
        bool Insert(Member entity);
        bool Update(Member entity);
        //bool Delete(int Id);
        bool Delete(string userName);

    }
    public class MemberService : IMemberService
    {
        public bool Insert(Member entity)
        {
            return true;
        }
        /// <summary>
        /// Nguyễn đức Bắc
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Update(Member entity)
        {
            return true;
        }
        
        //public bool Delete(int Id)
        //{
        //    return true;
        //}

        public bool Delete(string userName)
        {
            return true;
        }
    }
}
