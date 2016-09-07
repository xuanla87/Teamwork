namespace ServiceChoNhan.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChoManager")]
    public partial class ChoManager
    {
        [Key]
        public int ChoID { get; set; }

        public int? MemberID { get; set; }

        public string ListMemberID { get; set; }

        public DateTime? NgayGui { get; set; }

        [StringLength(50)]
        public string UserName { get; set; }
    }
}
