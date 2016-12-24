using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ClassLibrary.Commons;
using ClassLibrary.Models;

namespace ClassLibrary.Services
{
    public interface ICategoriesService
    {
        bool Insert(Category entity);
        bool Update(Category entity);
        bool Delete(int cateId);
    }
    public class CategoriesService : ICategoriesService
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
                LogSystemService logService = new LogSystemService();
                var logs = new LogSystem();
                logs.IPAddress = CommonsHelper.GetIpAddress;
                logs.CreateDate = DateTime.Now;
                logs.Messenger = "Tài khoản: " + HttpContext.Current.Session[CommonsHelper.SessionAdminCp] + " [Lỗi Thêm mới Cateogories]" +
                                 ex.ToString();
                logs.Status = false;
                logService.Insert(logs);
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
                LogSystemService logService = new LogSystemService();
                var logs = new LogSystem();
                logs.IPAddress = CommonsHelper.GetIpAddress;
                logs.CreateDate = DateTime.Now;
                logs.Messenger = "Tài khoản: " + HttpContext.Current.Session[CommonsHelper.SessionAdminCp] + " [Lỗi Edit Cateogories]" +
                                 ex.ToString();
                logs.Status = false;
                logService.Insert(logs);
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
                    category.Status = -1;
                    _db.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                LogSystemService logService = new LogSystemService();
                var logs = new LogSystem();
                logs.IPAddress = CommonsHelper.GetIpAddress;
                logs.CreateDate = DateTime.Now;
                logs.Messenger = "Tài khoản: " + HttpContext.Current.Session[CommonsHelper.SessionAdminCp] + " [Lỗi Delete Cateogories]" +
                                 ex.ToString();
                logs.Status = false;
                logService.Insert(logs);
                return false;
            }
        }
        /// <summary>
        /// thuc thi update bản ghi
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool UpdateStatus(int categoryid, int categorystatus)
        {
            try
            {
                var entity = _db.Categories.Find(categoryid);
                if (entity != null)
                {
                    if (categorystatus == 0)
                        entity.Status = 1;
                    if (categorystatus == 1)
                        entity.Status = 0;
                    _db.SaveChanges();
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                LogSystemService logService = new LogSystemService();
                var logs = new LogSystem();
                logs.IPAddress = CommonsHelper.GetIpAddress;
                logs.CreateDate = DateTime.Now;
                logs.Messenger = "Tài khoản: " + HttpContext.Current.Session[CommonsHelper.SessionAdminCp] + " [Lỗi Cập nhật Status Cateogories]" +
                                 ex.ToString();
                logs.Status = false;
                logService.Insert(logs);
                return false;
            }
        }
        /// <summary>
        /// Hàm thực hiện lệnh Delete
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool UpdateUrl(long id, string url)
        {
            try
            {
                var page = _db.Categories.Find(id);
                if (page != null)
                {
                    page.Url = url;
                    _db.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                LogSystemService logService = new LogSystemService();
                var logs = new LogSystem();
                logs.IPAddress = CommonsHelper.GetIpAddress;
                logs.CreateDate = DateTime.Now;
                logs.Messenger = "Tài khoản: " + HttpContext.Current.Session[CommonsHelper.SessionAdminCp] + " [Lỗi Update lại Url Categories]" +
                                 ex.ToString();
                logs.Status = false;
                logService.Insert(logs);
                return false;
            }
        }
        /// <summary>
        /// tạo cấu trúc category
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SelectListItem> GetCategoriesSelectList()
        {
            var cate = _db.Categories.Where(x => x.Status != -1).ToList();
            List<SelectListItem> options = new List<SelectListItem>();
            var parents = cate.Where(x => x.ParentId == null);
            foreach (var parent in parents)
            {
                options.Add(new SelectListItem()
                {
                    Value = parent.Id.ToString(),
                    Text = parent.Name
                });
                var children = cate.Where(x => x.ParentId == parent.Id);
                foreach (var child in children)
                {
                    options.Add(new SelectListItem()
                    {
                        Value = child.Id.ToString(),
                        Text = string.Format("::..{0}", child.Name)
                    });
                    //-----------------------------------------------------------------------------
                    // hien thi menu cap 3
                    var parents3 = cate.Where(x => x.ParentId == child.Id);
                    foreach (var parent3 in parents3)
                    {
                        // Add SelectListItem for the parent
                        options.Add(new SelectListItem()
                        {
                            Value = parent3.Id.ToString(),
                            Text = string.Format("::..::..{0}", parent3.Name)
                        });
                        // hien thi menu cap 4
                        //var children3 = categories.Where(x => x.CategoryParentId == parent3.CategoryId);
                        //foreach (var child3 in children3)
                        //{
                        //    options.Add(new SelectListItem()
                        //    {
                        //        Value = child3.CategoryId.ToString(),
                        //        Text = string.Format("::..::..{0}", child3.CategoryName)
                        //    });
                        //}
                    }
                }
            }
            return options;
        }
        /// <summary>
        /// Hàm trả về danh sách tất cả bản ghi
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Category> ListAllCategory()
        {
            return _db.Categories.Where(x => x.Status != -1 && x.ParentId == null).ToList();
        }
        /// <summary>
        ///  Kiểm tra chuyên mục có tồn tại không
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public bool CategoryNameExits(string input)
        {
            return _db.Categories.Any(x => x.Url == input);
        }
        /// <summary>
        /// hàm trả về danh sách Category có ParentID=NULL và trạng thái != -1
        /// trạng thái =-1 có nghĩa là bản ghi đã bị xóa không hiển thị trên he thống
        /// </summary>
        /// <returns></returns>
        public List<Category> GetCategoryByParent(long id)
        {
            return _db.Categories.Where(x => x.ParentId == null && x.Id != id && x.Status != -1).ToList();
        }
        /// <summary>
        /// hàm trả về danh sách Category 
        /// trạng thái =-1 có nghĩa là bản ghi đã bị xóa không hiển thị trên he thống
        /// </summary>
        /// <returns></returns>
        public List<Category> GetSelectListCategory()
        {
            return _db.Categories.Where(x => x.Status != -1).ToList();
        }
        /// <summary>
        /// Hàm trả về 1 bản ghi trong Category với ID truyền vào
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Category GetCategoryById(int? id)
        {
            return _db.Categories.FirstOrDefault(x => x.Id == id);
        }

        public Category getById(long? Id)
        {
            return _db.Categories.Find(Id);
        }
        public Category getByUrl(string sturl)
        {
            return _db.Categories.FirstOrDefault(x => x.Url == sturl);
        }
        public Category GetParentChild(long? Id)
        {
            return _db.Categories.FirstOrDefault(x => x.ParentId == Id);
        }

        public IEnumerable<Category> getTopCategory(int top)
        {
            return _db.Categories.Where(x => x.ParentId == null && x.taxanomy == null && x.Status!=-1).Take(top).OrderBy(x => x.Id);
        }

        public IEnumerable<Category> getByParentId(long? ParentId)
        {
            if (ParentId == null)
                return null;
            else
                return _db.Categories.Where(x => x.ParentId == ParentId && x.Status != -1);
        }

        public IEnumerable<Category> GetCategories_LEFTMENU_2(string taxanomy,long? Id)
        {
            return _db.Categories.Where(x => x.taxanomy == taxanomy && x.ParentId == Id && x.Status != -1);
        }
        public List<Category> GetCateAutoComplete(string input)
        {
            return _db.Categories.Where(x => (x.Url.Contains(input) || x.Name.Contains(input))).ToList();
        }
        /// <summary>
        /// hàm trả về danh sách mennu theo parentid
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Category> CategoryGetByParent(long cateid)
        {
            return _db.Categories.Where(x => x.ParentId == cateid).OrderBy(x => x.Id).ToList();
        }

        /// <summary>
        /// hàm trả về danh sách mennu theo parentid
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Category> CategoryGetListByParentId(int cateid)
        {
            return
                _db.Categories.Where(x => x.ParentId == cateid || x.Id == cateid && x.Status == 1)
                    .OrderBy(x => x.Id)
                    .ToList();
        }

        public Category GetByIdCategories(long cateid)
        {
            return _db.Categories.Where(x => x.ParentId == cateid || x.Id == cateid && x.Status == 1).FirstOrDefault();
        }
    }
}
