namespace ServiceChoNhan.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LogActive")]
    public partial class LogActive
    {
        [Key]
        public int ActiveID { get; set; }

        public int? MemberID { get; set; }

        public DateTime? ActiveDate { get; set; }

        [StringLength(50)]
        public string UserName { get; set; }
    }
}
