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

        [Display(Name = "CÀI ĐẶT FOOTER TRANG")]
        public string FooterInfo { get; set; }
    }
}