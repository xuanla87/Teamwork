using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NewNationals.Areas.AdminControlPanel.Models
{
    public class CommentModels
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "Nhập họ tên")]
        [Display(Name = "Họ tên")]
        [StringLength(150)]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Nhập Email")]
        [Display(Name = "Email")]
        [StringLength(150)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Nhập Nội dung")]
        [Display(Name = "Nội dung")]
        [StringLength(200)]
        public string Messager { get; set; }

        [Display(Name = "Ngày Đăng")]
        public DateTime? CreateDate { get; set; }

        [Display(Name = "Trạng thái")]
        public int? Status { get; set; }
    }
}