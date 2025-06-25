using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TINTOMTAT.Data.Entites
{
    [Table("Comment")]
    public class Comment
    {
        public long Id { get; set; }
        public string CommentData { get; set; }
        public DateTime? CreatedDate { get; set; }
        //public long? BinhLuanChaId { get; set; }
        public long? MemberId { get; set; }
        public long? PostId { get; set; }
        public bool? IsDeleted { get; set; }

        [ForeignKey("MemberId")]
        public virtual Member Member { get; set; }

        [ForeignKey("PostId")]
        public virtual Post Post { get; set; }
    }
}