namespace ServiceChoNhan.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LogPH")]
    public partial class LogPH
    {
        [Key]
        public int PhID { get; set; }

        public int? MemberID { get; set; }

        public int? SoLuong { get; set; }

        public DateTime? NgayPH { get; set; }

        [Required]
        [StringLength(50)]
        public string UserName { get; set; }
    }
}
