namespace ServiceChoNhan.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LogGH")]
    public partial class LogGH
    {
        [Key]
        public int GhID { get; set; }

        public int? MemberID { get; set; }

        public int? SoLuong { get; set; }

        public DateTime? NgayGH { get; set; }

        [StringLength(50)]
        public string UserName { get; set; }
    }
}
