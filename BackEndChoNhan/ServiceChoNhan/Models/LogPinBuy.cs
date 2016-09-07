namespace ServiceChoNhan.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LogPinBuy")]
    public partial class LogPinBuy
    {
        [Key]
        public int BuyPinID { get; set; }

        public int? PinID { get; set; }

        public int? MemberID { get; set; }

        public int? SoLuong { get; set; }

        public DateTime? NgayBan { get; set; }

        [Required]
        [StringLength(50)]
        public string UserName { get; set; }
    }
}
