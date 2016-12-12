using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NewNationals.Areas.AdminControlPanel.Models
{
    public class ChangePassModels
    {
        [Display(Name = "Tài khoản")]
        public string Username { get; set; }

        [Display(Name = "Mật khẩu hiện tại")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu cũ!")]
        public string PassOld { get; set; }

        [Display(Name = "Mật khẩu mới")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu mới!")]
        public string PasswordNew { get; set; }

        [Display(Name = "Gõ lại mật khẩu mới")]
        [Required(ErrorMessage = "Vui lòng nhập lại mật khẩu mới!")]
        [DataType(DataType.Password)]
        [Compare("PasswordNew", ErrorMessage = "Nhập lại mật khẩu chưa khớp nhau!")]
        public string PasswordNewConfirm { get; set; }
    }
}