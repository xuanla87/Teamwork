namespace ClassLibrary.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Menu
    {
        public long Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Name { get; set; }

        [StringLength(350)]
        public string TargetUrl { get; set; }

        public long? ParentId { get; set; }

        [StringLength(50)]
        public string Tanoxomy { get; set; }

        public byte Order { get; set; }

        [StringLength(150)]
        public string stExtension { get; set; }
    }
}
