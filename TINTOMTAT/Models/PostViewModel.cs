using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace TINTOMTAT.Models
{
    public class PostViewModel
    {
        public long Id { get; set; }

        [DisplayName("Post Name")]
        public string PostName { get; set; }
        public string Alias { get; set; }

        [DisplayName("Short Content")]
        public string ShortContent { get; set; }

        [DisplayName("Content")]
        public string Content { get; set; }

        [DisplayName("Post Image")]
        public string PostImage { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }
        public DateTime CreatedDate { get; set; }
        [DisplayName("Virtual View")]
        public int? FView { get; set; }
        public int? View { get; set; }
        public DateTime? UpdatedDate { get; set; }

        [DisplayName("Home Page Order")]
        public int? HomePageOrder { get; set; }

        [DisplayName("Order")]
        public int? Order { get; set; }
        public bool? IsDeleted { get; set; }

        [DisplayName("Post Type")]
        public long PostTypeId { get; set; }

        public string PostTypeName { get; set; }
    }
}