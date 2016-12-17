
namespace ClassLibrary.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    public partial class Comment
    {
        public long Id { get; set; }

        [Required]
        [StringLength(150)]
        public string FullName { get; set; }

        [Required]
        [StringLength(150)]
        public string Email { get; set; }

        [Required]
        [StringLength(200)]
        public string Messager { get; set; }

        public DateTime CreateDate { get; set; }

        public int? Status { get; set; }

        public long PageId { get; set; }


    }
}
