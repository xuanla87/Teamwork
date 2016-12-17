using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NewNationals.Models
{
    public class ModelComments
    {
        [Required(ErrorMessage = "Bạn vui lòng nhập nội dung!")]
        [MaxLength(200, ErrorMessage = "Bình luận chỉ giới hạn trong 200 ký tự!")]
        public string ContentComment { get; set; }
        [Required(ErrorMessage = "Bạn vui lòng nhập họ và tên!")]
        [MaxLength(100, ErrorMessage = "Họ và Tên quá dài!")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Bạn vui lòng nhập email!")]
        [MaxLength(100, ErrorMessage = "Email quá dài!")]
        public string Email { get; set; }
        public string Captcha { get; set; }
        [Compare("Captcha", ErrorMessage = "Bạn nhập mã xác nhận không chính xác!")]
        [Required(ErrorMessage = "Bạn vui lòng nhập mã xác nhận!")]
        public string ConfirmCaptcha { get; set; }
        public long PageId { get; set; }
    }
}