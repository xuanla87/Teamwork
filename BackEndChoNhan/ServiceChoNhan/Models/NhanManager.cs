namespace ServiceChoNhan.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NhanManager")]
    public partial class NhanManager
    {
        [Key]
        public int NhanID { get; set; }

        public int? MemberID { get; set; }

        public string ListMemberID { get; set; }

        public DateTime? NgayNhan { get; set; }
    }
}
