namespace ServiceChoNhan.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Member")]
    public partial class Member
    {
        public int MemberID { get; set; }

        public int? MemberParent { get; set; }

        [Required]
        [StringLength(50)]
        public string UserName { get; set; }

        [Required]
        [StringLength(250)]
        public string Password { get; set; }

        [Required]
        [StringLength(50)]
        public string FullName { get; set; }

        public DateTime? NgaySinh { get; set; }

        public bool? GioiTinh { get; set; }

        [StringLength(500)]
        public string DiaChi { get; set; }

        [StringLength(500)]
        public string Email { get; set; }

        [StringLength(50)]
        public string SoCMND { get; set; }

        [StringLength(50)]
        public string SoTaiKhoanBank { get; set; }

        [StringLength(500)]
        public string TenNganHang { get; set; }

        public int? SoLuongPIN { get; set; }

        public DateTime? CreateDate { get; set; }

        public int? StatusActive { get; set; }

        [StringLength(100)]
        public string Depth { get; set; }

        public int? Decen { get; set; }

        public int? Levels { get; set; }

        public bool? StatusNew { get; set; }

        public bool? StatusPH { get; set; }

        public bool? StatusGH { get; set; }

        public double? TotalPH { get; set; }

        public double? TotalGH { get; set; }

        public double? TotalHoaHong { get; set; }

        public bool? Status { get; set; }
    }
}
