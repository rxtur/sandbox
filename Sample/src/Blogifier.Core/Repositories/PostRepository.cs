using Blogifier.Core.Models;
using Blogifier.Core.Repositories.Interfaces;
using Blogifier.Core.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        public async Task<PostList> Find(Expression<Func<Post, bool>> predicate, int page = 1, int pageSize = 10)
        {
            var skip = page * pageSize - pageSize;
            var pagedList = new PostList(page, pageSize);

            var posts = _db.Posts.AsNoTracking().Where(predicate).OrderByDescending(p => p.Published)
                .Include(p => p.PostCategories).Include(p => p.Blog).ToList();

            pagedList.TotalCnt = posts.Count;

            if(skip == 0)
                pagedList.Posts = GetItems(posts).Take(pageSize).ToList();
            else
                pagedList.Posts = GetItems(posts).Skip(skip).Take(pageSize).ToList();

            return await Task.Run(() => pagedList);
        }

        public async Task<PostList> ByCategory(string slug, string blog = "all", int page = 1, int pageSize = 10)
        {
            var skip = page * pageSize - pageSize;
            var pagedList = new PostList(page, pageSize);

            var cat = _db.Categories.Where(c => c.Slug == slug).FirstOrDefault();
            var posts = blog == "all" ? 
                _db.Posts.AsNoTracking().Include(p => p.Blog).Include(p => p.PostCategories).ToList() :
                _db.Posts.AsNoTracking().Include(p => p.Blog).Include(p => p.PostCategories).Where(p => p.Blog.Slug == blog).ToList();

            var categorized = new List<Post>();
            if (cat != null)
            {
                foreach (var p in posts)
                {
                    foreach (var c in p.PostCategories)
                    {
                        if (c.CategoryId == cat.CategoryId)
                        {
                            categorized.Add(p);
                            break;
                        }
                    }
                }
            }
            pagedList.TotalCnt = categorized.Count;

            if (skip == 0)
                pagedList.Posts = GetItems(categorized).Take(pageSize).ToList();
            else
                pagedList.Posts = GetItems(categorized).Skip(skip).Take(pageSize).ToList();

            return await Task.Run(() => pagedList);
        }

        public PostDetail BySlug(string slug)
        {
            var item = new PostDetail();
            item.Post = _db.Posts.AsNoTracking()
                .Include(p => p.Blog)
                .Include(p => p.PostCategories)
                .FirstOrDefault(p => p.Slug == slug);

            item.Categories = GetCategories(item.Post);
            return item;
        }

        public async Task Add(Post item)
        {
            item.Saved = DateTime.UtcNow;

            //TODO: temp handling
            item.Published = DateTime.UtcNow;
            item.Slug = item.Title.Replace(" ", "-").ToLower();
            item.Description = item.Content;

            _db.Posts.Add(item);
            await _db.SaveChangesAsync();
        }

        public async Task Update(Post item)
        {
            var itemToUpdate = await _db.Posts.FirstOrDefaultAsync(i => i.PostId == item.PostId);

            itemToUpdate.Saved = DateTime.UtcNow;
            itemToUpdate.Title = item.Title;
            itemToUpdate.Content = item.Content;

            _db.Posts.Update(itemToUpdate);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var item = await _db.Posts.FirstOrDefaultAsync(i => i.PostId == id);
            _db.Posts.Remove(item);
            await _db.SaveChangesAsync();
        }

        #region Methods
        private List<PostListItem> GetItems(List<Post> postList)
        {
            var posts = new List<PostListItem>();
            foreach (var p in postList)
            {
                posts.Add(GetItem(p));
            }
            return posts;
        }

        private PostListItem GetItem(Post post)
        {
            var item = new PostListItem
            {
                Slug = post.Slug,
                Title = post.Title,
                Content = post.Content,
                Published = post.Published,
                AuthorName = post.Blog.AuthorName,
                BlogSlug = post.Blog.Slug,
                AuthorEmail = post.Blog.AuthorEmail,
                Categories = new List<CategoryListItem>()
            };
            item.Categories = GetCategories(post);
            return item;
        }

        private List<CategoryListItem> GetCategories(Post post)
        {
            var catList = new List<CategoryListItem>();
            if (post.PostCategories != null && post.PostCategories.Count > 0)
            {
                foreach (var pc in post.PostCategories)
                {
                    var cat = _db.Categories.AsNoTracking().Where(c => c.CategoryId == pc.CategoryId).FirstOrDefault();
                    catList.Add(new CategoryListItem { CategoryId = cat.CategoryId, Slug = cat.Slug, Title = cat.Title });
                }
            }
            return catList;   
        }
        #endregion
    }
}
