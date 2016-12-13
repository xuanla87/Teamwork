using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClassLibrary.Services;
using ClassLibrary.Commons;
using ClassLibrary.Models;
using NewNationals.Areas.AdminControlPanel.Models;

namespace NewNationals.Areas.AdminControlPanel.Models
{
    public class SlideShowModels
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên Slide")]
        [Display(Name = "Tên Slide")]
        [MaxLength(50,ErrorMessage = "Tên Slide không vượt quá 50 ký tự!")]
        public string Name { get; set; }

        [Display(Name = "Link hình ảnh")]
        [MaxLength(350, ErrorMessage = "Đường dẫn hình ảnh quá dài không vượt quá 350 ký tự!")]
        public string ImageUrl { get; set; }

        [Display(Name = "Ghi chú")]
        public string Note { get; set; }

        [Display(Name = "Link bài viết")]
        public string TargetUrl { get; set; }

        [Display(Name = "Vị trí sắp xếp")]
        public int? Order { get; set; }

        [Required()]
        [StringLength(50)]
        [Display(Name = "Phân loại")]
        public string Tanoxomy { get; set; }

        [Display(Name = "Trạng thái")]
        public int? Status { get; set; }

        [Display(Name = "Ngày bắt đầu")]
        public DateTime StartDate { get; set; }

        [Display(Name = "Ngày kết thúc")]
        public DateTime EndDate { get; set; }
    }
}