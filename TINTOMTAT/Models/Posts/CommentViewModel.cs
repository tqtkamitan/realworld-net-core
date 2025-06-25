using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TINTOMTAT.Models.Comments
{
    public class CommentViewModel
    {
        public long PostId { get; set; }
        public long MemberId { get; set; }
        public string Comment { get; set; }
        public string RedirecUrlComt { get; set; }
        public int ScrollTop { get; set; }
    }
}