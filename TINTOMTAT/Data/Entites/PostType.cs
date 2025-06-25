using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace TINTOMTAT.Data.Entites
{
    [Table("PostType")]
    public class PostType
    {
        public long Id { get; set; }
        public string PostTypeName { get; set; }
        public string Alias { get; set; }
        public string Logo { get; set; }
        public int? Order { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual List<Post> Posts { get; set; }
    }
}