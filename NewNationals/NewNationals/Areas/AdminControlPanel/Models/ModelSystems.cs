using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NewNationals.Areas.AdminControlPanel.Models
{
    public class ModelSystems
    {
        public string HomeBoxFirst { get; set; }
        public string HomeBoxSecond { get; set; }
        public string HomeBoxThird { get; set; }
        public string HomeBoxFourth { get; set; }
        public string HomeBoxFifth { get; set; }
        public string HomeBoxSixth { get; set; }

        [Display(Name = "CÀI ĐẶT FOOTER 1")]
        public string FooterInfo1 { get; set; }

        [Display(Name = "CÀI ĐẶT FOOTER 2")]
        public string FooterInfo2 { get; set; }

        [Display(Name = "CÀI ĐẶT FOOTER 3")]
        public string FooterInfo3 { get; set; }

        [Display(Name = "THÔNG TIN")]
        public string THONGTIN { get; set; }

        [Display(Name = "RIÊNG TƯ")]
        public string RIENGTU { get; set; }

        [Display(Name = "ĐIỀU KHOẢN")]
        public string DIEUKHOAN { get; set; }
    }
}