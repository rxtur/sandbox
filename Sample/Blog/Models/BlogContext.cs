using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.SqlServer;
using System.Collections.Generic;

namespace BlogiFire.Models
{
    public class BlogContext : DbContext
    {
        private static bool _created = false;

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }

        public BlogContext()
        {
            if (!_created)
            {
                Database.AsRelational().ApplyMigrations();
                _created = true;
            }
        }

        protected override void OnConfiguring(DbContextOptions builder)
        {
            //builder.UseInMemoryStore();
            builder.UseSqlServer("Server=.\\SQLEXPRESS;Database=BlogiFire;Trusted_Connection=True;MultipleActiveResultSets=true");
        }
    }

    public class Blog
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<Post> Posts { get; set; }
    }

    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int Comments { get; set; }
        public DateTime Created { get; set; }
        public bool Published { get; set; }
        public int BlogId { get; set; }
        public virtual Blog Blog { get; set; }
    }
}