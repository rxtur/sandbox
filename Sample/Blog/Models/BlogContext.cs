using System;
using Microsoft.Data.Entity;

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
}