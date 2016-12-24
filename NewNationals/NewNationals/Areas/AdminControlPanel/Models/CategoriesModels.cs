using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NewNationals.Areas.AdminControlPanel.Models
{
    public class CategoriesModels
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên chuyên mục")]
        [Display(Name = "Tên chuyên mục")]
        [MaxLength(150, ErrorMessage = "Tên chuyên mục không vượt quá 150 ký tự!")]
        public string Name { get; set; }

        [Display(Name = "Ghi chú")]
        public string Note { get; set; }

        public string Url { get; set; }

        [Display(Name = "Thẻ Tiêu đề (SEO)")]
        public string Title { get; set; }

        [Display(Name = "Thẻ từ khóa (SEO)")]
        public string Keyword { get; set; }

        [Display(Name = "Thẻ Mô tả (SEO)")]
        public string Description { get; set; }

        [Display(Name = "Trạng thái")]
        public int Status { get; set; }

        [Display(Name = "Chuyên mục cha")]
        public long? ParentId { get; set; }

        [Display(Name = "Chọn chuyên mục")]
        public string Taxanomy { get; set; }

        [Display(Name = "Lấy Link từ bài viết trỏ vào chuyên mục này")]
        public string GeTargettUrl { get; set; }
    }
}