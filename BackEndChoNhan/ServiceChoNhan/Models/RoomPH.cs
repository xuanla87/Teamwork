namespace ServiceChoNhan.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RoomPH")]
    public partial class RoomPH
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(50)]
        public string UserName { get; set; }

        [Key]
        [Column(Order = 1)]
        public DateTime CreateDate { get; set; }
    }
}
