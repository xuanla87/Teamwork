using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NewNationals.Areas.AdminControlPanel.Models
{
    public class UserModels
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập Tài khoản!")]
        [Display(Name = "Tài khoản")]
        [MaxLength(50, ErrorMessage = "Tài khoản không vượt quá 50 ký tự!")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập Email!")]
        [Display(Name = "Email")]
        [MaxLength(50, ErrorMessage = "Email không vượt quá 50 ký tự!")]
        public string UserEmail { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập Mật khẩu!")]
        [Display(Name = "Mật khẩu")]
        [MaxLength(250, ErrorMessage = "Mật khẩu không vượt quá 250 ký tự!")]
        [MinLength(6, ErrorMessage = "Mật khẩu không ít hơn 6 ký tự!")]
        public string UserPassword { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập Họ tên!")]
        [Display(Name = "Họ tên")]
        [MaxLength(50, ErrorMessage = "Họ tên không vượt quá 50 ký tự!")]
        public string FullName { get; set; }

        [Display(Name = "Ảnh đại diện")]
        public string Avatar { get; set; }

        [Display(Name = "Trạng thái")]
        public int Status { get; set; }

        [Display(Name = "Ngày tạo")]
        public DateTime? CreateDate { get; set; }

        [Display(Name = "Ngày đăng nhập cuối")]
        public DateTime? ModifiedDate { get; set; }

        [Display(Name = "Quyền")]
        public long? RoleId { get; set; }
    }
}