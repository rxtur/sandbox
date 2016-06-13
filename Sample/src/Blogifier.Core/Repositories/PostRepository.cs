using Blogifier.Core.Models;
using Blogifier.Core.Repositories.Interfaces;
using Blogifier.Core.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace Blogifier.Core.Repositories
{
    public class PostRepository : IPostRepository
    {
        BlogifierDbContext _db;
        public PostRepository(BlogifierDbContext db)
        {
            _db = db;
        }

        public async Task<List<PostItem>> All()
        {
            var posts = new List<PostItem>();
            var postList = _db.Posts.AsNoTracking().OrderByDescending(p => p.Published).Include(p => p.PostCategories).Include(p => p.Blog).ToList();

            foreach (var p in postList)
            {
                var item = new PostItem
                {
                    Slug = p.Slug,
                    Title = p.Title,
                    Content = p.Content,
                    Published = p.Published,
                    AuthorName = p.Blog.AuthorName,
                    BlogSlug = p.Blog.Slug,
                    AuthorEmail = p.Blog.AuthorEmail,
                    Categories = new List<CategoryItem>()
                };

                if (p.PostCategories != null && p.PostCategories.Count > 0)
                {
                    foreach (var pc in p.PostCategories)
                    {
                        var cat = _db.Categories.AsNoTracking().Where(c => c.CategoryId == pc.CategoryId).FirstOrDefault();
                        var catItem = new CategoryItem { Slug = cat.Slug, Title = cat.Title };
                        item.Categories.Add(catItem);
                    }
                }
                posts.Add(item);
            }
            return await Task.Run(() => posts);
        }

        public async Task<Post> GetBySlug(string slug)
        {
            return await _db.Posts.AsNoTracking().Include(p => p.Blog).FirstOrDefaultAsync(p => p.Slug == slug);
        }
    }
}
