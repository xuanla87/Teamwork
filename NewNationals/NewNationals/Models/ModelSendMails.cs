using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NewNationals.Models
{
    public class ModelSendMails
    {
        [Display(Name = "Tên Người Gửi")]
        public string FullName { get; set; }
        [Display(Name = "Email Người Gửi")]
        public string MailFrom { get; set; }
        [Display(Name = "Email Người Nhận")]
        public string MailTo { get; set; }
        [Display(Name = "Tiêu Đề Thư")]
        public string Subject { get; set; }
        public long PageId { get; set; }
        [Display(Name = "Trang Được Gửi")]
        public string PageTitle { get; set; }
        [Display(Name = "Nội Dung Thư")]
        public string Content { get; set; }
    }
}