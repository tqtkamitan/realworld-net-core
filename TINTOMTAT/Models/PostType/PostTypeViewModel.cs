using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace TINTOMTAT.Models.PostType
{
    public class PostTypeViewModel
    {
        public long Id { get; set; }

        [DisplayName("Post Type Name")]
        public string PostTypeName { get; set; }

        public string Alias { get; set; }

        [DisplayName("Order")]
        public int? Order { get; set; }
    }
}