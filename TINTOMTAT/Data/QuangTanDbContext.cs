using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TINTOMTAT.Data.Entites;

namespace TINTOMTAT.Data
{
    public class QuangTanDbContext : DbContext
    {
        public QuangTanDbContext() : base("quangtanBdConnectstring")
        {
        }       


        public DbSet<PostType> PostTypes { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}