namespace ClassLibrary.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Page
    {
        public long Id { get; set; }

        [Required]
        [StringLength(350)]
        public string Name { get; set; }

        [Required]
        [StringLength(350)]
        public string Url { get; set; }

        [StringLength(250)]
        public string Title { get; set; }

        [StringLength(250)]
        public string Keywords { get; set; }

        [StringLength(250)]
        public string Description { get; set; }

        public int Status { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        public long UserCreate { get; set; }

        public long UserModified { get; set; }

        [StringLength(250)]
        public string Thumbnail { get; set; }

        public string Content { get; set; }

        [StringLength(512)]
        public string Note { get; set; }

        public bool Feature { get; set; }

        public bool Home { get; set; }

        public long? CategoriesId { get; set; }

        [StringLength(50)]
        public string Taxanomy { get; set; }

        [StringLength(250)]
        public string TacGia { get; set; }

        [StringLength(250)]
        public string ToChuc { get; set; }


    }
}
