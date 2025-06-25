using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TINTOMTAT.Models.Members
{
    public class MemberViewModel
    {
        public long Id { get; set; }
        public string DisplayName { get; set; }
        public DateTime? CreateDate { get; set; }
        public string CreateDateDisplay => CreateDate == null ? "" : CreateDate.Value.ToString("dd/MM/yyyy - HH-mm");
        public string Email { get; set; }
        public bool? IsDeleted { get; set; }
        public string IsDeletedDisplay => IsDeleted == null ? "Active" : (IsDeleted == true ? "Is Deleted" : "ACtive");
    }
}