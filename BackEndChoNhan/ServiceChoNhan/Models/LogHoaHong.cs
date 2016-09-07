namespace ServiceChoNhan.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LogHoaHong")]
    public partial class LogHoaHong
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string UserCho { get; set; }

        public double HoaHong { get; set; }

        public DateTime NgayNhan { get; set; }

        [StringLength(1024)]
        public string Note { get; set; }

        [Required]
        [StringLength(50)]
        public string UserNhan { get; set; }
    }
}
