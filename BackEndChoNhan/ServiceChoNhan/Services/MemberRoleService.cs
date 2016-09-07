using ServiceChoNhan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceChoNhan.Services
{
    public class MemberRoleService
    {
        DataContext db = null;
        public MemberRoleService()
        {
            db = new DataContext();
        }
        public int getRoleIdByMemberId(int Id)
        {
            //using (var db = new DataContext())
            //{
            try
            {
                return db.MemberRole.Where(x => x.MemberID == Id).FirstOrDefault().RoleId;
            }
            catch
            {
                return 0;
            }
            //}
        }

        public string getRoleNameByMemberId(int Id)
        {
            try
            {
                var role = db.MemberRole.Where(x => x.MemberID == Id).FirstOrDefault().RoleId;
                return db.Role.Find(role).RoleName;
            }
            catch
            {
                return null;
            }
        }
    }
}
