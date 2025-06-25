using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TINTOMTAT.Models.PostPortalViewModel
{
    public class PostDetailViewModel
    {
        public long Id { get; set; }
        public string PostName { get; set; }
        public string Alias { get; set; }
        public string PostImage { get; set; }
        public string Content { get; set; }
        public int? View { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedDateDisplay => CreatedDate.ToString("dd/MM/yyyy");
    }
}