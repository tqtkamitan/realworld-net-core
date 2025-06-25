using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TINTOMTAT.Data.Entites
{
    [Table("Member")]
    public class Member
    {
        public long Id { get; set; }
        public string DisplayName { get; set; }
        public int AccountTypeId { get; set; }
        public string AccountType { get; set; }
        public string Email { get; set; }
        public DateTime? CreateDate { get; set; }
        public string CreateBy { get; set; }
        public string Password { get; set; }
        public bool? IsDeleted { get; set; }
    }
}