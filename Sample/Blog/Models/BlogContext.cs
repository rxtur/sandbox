using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Framework.OptionsModel;
using Microsoft.Framework.ConfigurationModel;

namespace BlogiFire.Models
{
    public class BlogContext : DbContext
    {
        private static bool _created = false;

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Setting> Settings { get; set; }

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
            try
            {
                // try to get connection string from parent application
                // assuming it uses default values as in asp.net VS template
                var config = new Configuration().AddJsonFile("config.json").AddEnvironmentVariables();
                builder.UseSqlServer(config.Get("Data:DefaultConnection:ConnectionString"));
            }
            catch (Exception)
            {
                // fall back to using local SQL express
                builder.UseSqlServer("Server=.\\SQLEXPRESS;Database=BlogiFire;Trusted_Connection=True;MultipleActiveResultSets=true");
            }      
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Blog>().ForRelational().Table("bf_blogs");
            builder.Entity<Post>().ForRelational().Table("bf_posts");
            builder.Entity<Setting>().ForRelational().Table("bf_settings");

            base.OnModelCreating(builder);
        }
    }
}