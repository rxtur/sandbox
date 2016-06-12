using Blogifier.Core.Models;
using Blogifier.Core.Repositories.Interfaces;
using Blogifier.Core.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blogifier.Core.Repositories
{
    public class PostRepository : IPostRepository
    {
        BlogifierDbContext _db;
        public PostRepository(BlogifierDbContext db)
        {
            _db = db;
        }

        //public async Task<List<Post>> All()
        //{
        //    var posts = _db.Posts.AsNoTracking().AsQueryable();
        //    return await posts.OrderByDescending(p => p.Published).ToListAsync();
        //}

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
                    AuthorSlug = p.Blog.Slug,
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

        public List<PostItem> GetPosts(BlogifierDbContext db)
        {
            var posts = new List<PostItem>();
            var postList = db.Posts.AsNoTracking().OrderByDescending(p => p.Published).Include(p => p.PostCategories).ToList();

            foreach (var p in postList)
            {
                var item = new PostItem {
                    Slug = p.Slug,
                    Title = p.Title,
                    Content = p.Content,
                    Published = p.Published,
                    Categories = new List<CategoryItem>()
                };

                if(p.PostCategories != null && p.PostCategories.Count > 0)
                {
                    foreach (var pc in p.PostCategories)
                    {
                        var cat = db.Categories.AsNoTracking().Where(c => c.CategoryId == pc.CategoryId).FirstOrDefault();
                        var catItem = new CategoryItem { Slug = cat.Slug, Title = cat.Title };
                        item.Categories.Add(catItem);
                    }
                }
                posts.Add(item);
            }
            return posts;
        }
    }
}
