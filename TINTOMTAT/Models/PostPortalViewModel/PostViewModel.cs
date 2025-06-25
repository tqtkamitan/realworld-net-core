using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TINTOMTAT.Models.PostPortalViewModel
{
    public class PostViewModel
    {
        public long Id { get; set; }
        public string PostName { get; set; }
        public string Alias { get; set; }
        public int? View { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ShortContent { get; set; }
        public string Image { get; set; }
    }
}