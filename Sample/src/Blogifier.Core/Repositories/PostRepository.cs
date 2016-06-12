using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blogifier.Core.ViewModels;
using Blogifier.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Blogifier.Core.Repositories
{
    public class PostRepository : Interfaces.IPostRepository
    {
        BlogifierDbContext _db;
        public PostRepository()
        {

        }

        public Task<List<PostItem>> All()
        {
            throw new NotImplementedException();
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
