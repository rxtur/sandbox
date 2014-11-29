using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BlogiFire.Models
{
    public class BlogRepository : IBlogRepository
    {
        BlogContext db;
        public BlogRepository()
        {
            this.db = new BlogContext();
        }
        public async Task<List<Blog>> All()
        {
            return await db.Blogs.OrderBy(b => b.Title).ToListAsync();
        }
        public async Task<List<Blog>> Find(Expression<Func<Blog, bool>> predicate)
        {
            return await db.Blogs.Where(predicate).ToListAsync();
        }
        public async Task<Blog> GetById(int id)
        {
            return await db.Blogs.FirstOrDefaultAsync(b => b.Id == id);
        }
        public async Task Add(Blog item)
        {
            db.Blogs.Add(item);
            await db.SaveChangesAsync();

            await Seed(item);
        }
        public async Task Update(Blog item)
        {
            try
            {
                var dbItem = db.Blogs.SingleOrDefault(i => i.Id == item.Id);

                dbItem.Title = item.Title;

                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                throw;
            }
        }
        public async Task Delete(int id)
        {
            var item = await db.Blogs.FirstOrDefaultAsync(i => i.Id == id);
            db.Blogs.Remove(item);
            await db.SaveChangesAsync();
        }

        public async Task Seed(Blog blog)
        {
            var blogPosts = db.Posts.Where(p => p.BlogId == blog.Id);
            if(blogPosts == null || blogPosts.Count() == 0)
            {
                for (int i = 1; i <= 25; i++)
                {
                    var post = new Post();
                    post.BlogId = blog.Id;
                    post.AuthorName = blog.AuthorName;
                    post.Title = string.Format("Post title{0}", i);
                    post.Slug = string.Format("post-title{0}", i);
                    post.Content = string.Format("This is content of the post {0}.", i);
                    post.Published = DateTime.UtcNow.AddHours((i - 26) * 5);
                    post.Saved = post.Published;

                    db.Posts.Add(post);
                }
            }         
            await db.SaveChangesAsync();
        }
    }
}