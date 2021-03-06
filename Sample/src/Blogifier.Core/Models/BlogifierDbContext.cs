﻿using Blogifier.Core.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Blogifier.Core.Models
{
    public class BlogifierDbContext : DbContext
    {
        #region DB Sets
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<BlogMeta> BlogMetas { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostMeta> PostMetas { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryMeta> CategoryMetas { get; set; }
        public DbSet<PostCategory> PostCategories { get; set; }
        public DbSet<Asset> Assets { get; set; }
        #endregion

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (AppSettings.UseInMemoryDb)
            {
                optionsBuilder.UseInMemoryDatabase();
            }
            else
            {
                optionsBuilder.UseSqlServer(AppSettings.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Blog>().ForSqlServerToTable("bf_blogs");
            modelBuilder.Entity<BlogMeta>().ForSqlServerToTable("bf_blogmetas");
            modelBuilder.Entity<Post>().ForSqlServerToTable("bf_posts");
            modelBuilder.Entity<PostMeta>().ForSqlServerToTable("bf_postmetas");
            modelBuilder.Entity<Category>().ForSqlServerToTable("bf_categories");
            modelBuilder.Entity<CategoryMeta>().ForSqlServerToTable("bf_categorymetas");
            modelBuilder.Entity<PostCategory>().ForSqlServerToTable("bf_postcategories");
            modelBuilder.Entity<Asset>().ForSqlServerToTable("bf_assets");
        }
    }
}
