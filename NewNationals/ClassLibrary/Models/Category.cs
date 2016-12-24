namespace ClassLibrary.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Category
    {
        public long Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Name { get; set; }

        [Required]
        [StringLength(150)]
        public string Url { get; set; }

        [StringLength(500)]
        public string Note { get; set; }

        [StringLength(250)]
        public string Title { get; set; }

        [StringLength(250)]
        public string Keyword { get; set; }

        [StringLength(250)]
        public string Description { get; set; }

        public int Status { get; set; }

        public long? ParentId { get; set; }

        [StringLength(50)]
        public string taxanomy { get; set; }
    }
}
