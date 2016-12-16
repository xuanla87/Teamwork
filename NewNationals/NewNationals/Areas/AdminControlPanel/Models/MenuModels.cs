using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NewNationals.Areas.AdminControlPanel.Models
{
    public class MenuModels
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "Nhập tên menu")]
        [StringLength(150)]
        [Display(Name = "Tên menu")]
        public string Name { get; set; }


        public string TypeTargetUrl { get; set; }

        [StringLength(350)]
        [Display(Name = "Đường dẫn bài viết")]
        public string TargetUrl { get; set; }

        [Display(Name = "Menu cha")]
        public long? ParentId { get; set; }

        [StringLength(50)]
        [Display(Name = "Phân loại")]
        public string Tanoxomy { get; set; }

        [Required(ErrorMessage = "Nhập vị trí sắp xếp (kiểu số)")]
        [Display(Name = "Vị trí sắp xếp")]
        public byte Order { get; set; }

        [StringLength(150)]
        [Display(Name = "Mở rộng")]
        public string stExtension { get; set; }
    }
}