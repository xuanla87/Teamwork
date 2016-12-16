using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NewNationals.Areas.AdminControlPanel.Models
{
    public class SettingModels
    {
        public short Id { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập KEY")]
        [StringLength(50)]
        [Display(Name = "Key cấu hình")]
        public string stKey { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập Nội dung")]
        [StringLength(50)]
        [Display(Name = "Nội dung cấu hình")]
        public string stValue { get; set; }

        [Display(Name = "Trạng thái")]
        public bool Status { get; set; }
    }
}