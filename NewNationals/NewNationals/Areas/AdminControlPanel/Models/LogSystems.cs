using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NewNationals.Areas.AdminControlPanel.Models
{
    public class LogSystems
    {
        [Key]
        public long LogId { get; set; }

        public DateTime CreateDate { get; set; }

        [Required]
        [StringLength(50)]
        public string IPAddress { get; set; }

        public string Messenger { get; set; }

        public bool? Status { get; set; }
    }
}