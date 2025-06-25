using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TINTOMTAT.Data.Entites
{
    [Table("Post")]
    public class Post
    {
        public long Id { get; set; }
        public string PostName { get; set; }
        public string Alias { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ShortContent { get; set; }
        public string PostImage { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? FView { get; set; }
        public int? View { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? HomePageOrder { get; set; }
        public int? Order { get; set; }
        public bool? IsDeleted { get; set; }
        public long PostTypeId { get; set; }

        [ForeignKey("PostTypeId")]
        public virtual PostType PostType { get; set; }
    }
}