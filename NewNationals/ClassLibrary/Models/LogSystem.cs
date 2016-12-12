
namespace ClassLibrary.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LogSystem")]
    public partial class LogSystem
    {
        [Key]
        public long LogId { get; set; }

        public DateTime CreateDate { get; set; }

        [Required]
        [StringLength(50)]
        public string IPAddress { get; set; }

        public string Messenger { get; set; }

        public bool? Status { get; set; }
    }
}
