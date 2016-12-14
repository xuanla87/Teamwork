using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NewNationals.Areas.AdminControlPanel.Models
{
    public class RoleModels
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên quyền")]
        [Display(Name = "Tên Quyền")]
        [StringLength(50)]
        public string RoleName { get; set; }

        [StringLength(500)]
        [Display(Name = "Ghi chú")]
        public string Note { get; set; }
    }
}