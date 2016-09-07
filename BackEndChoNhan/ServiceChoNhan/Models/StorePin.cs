namespace ServiceChoNhan.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("StorePin")]
    public partial class StorePin
    {
        [Key]
        public int PinID { get; set; }

        public int? SoLuong { get; set; }

        public DateTime? NgayTao { get; set; }

        public string UserName { get; set; }
    }
}
