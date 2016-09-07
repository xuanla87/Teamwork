namespace ServiceChoNhan.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LogReport")]
    public partial class LogReport
    {
        [Key]
        public int ReportID { get; set; }

        public int? MemberID { get; set; }

        public string NoiDung { get; set; }

        public DateTime? NgayGui { get; set; }
    }
}
