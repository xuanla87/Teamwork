namespace ClassLibrary.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Slidehow
    {
        public long Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(350)]
        public string ImageUrl { get; set; }

        [StringLength(512)]
        public string Note { get; set; }

        [StringLength(250)]
        public string TargetUrl { get; set; }

        public int? Order { get; set; }

        [Required]
        [StringLength(50)]
        public string Tanoxomy { get; set; }

        public int? Status { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
