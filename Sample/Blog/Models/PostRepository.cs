using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace BlogiFire.Models
{
    public class PostRepository : IPostRepository
    {
        BlogContext db;
        public PostRepository()
        {
            this.db = new BlogContext();
        }
        public async Task<List<Post>> All()
        {
            return await db.Posts.OrderByDescending(p => p.Saved).ToListAsync();
        }
        public async Task<List<Post>> Find(Expression<Func<Post, bool>> predicate)
        {
            return await db.Posts.Where(predicate).ToListAsync();
        }
        public async Task<Post> GetById(int id)
        {
            return await db.Posts.FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task Add(Post item)
        {
            db.Posts.Add(item);
            await db.SaveChangesAsync();
        }
        public async Task Update(Post item)
        {
            try
            {
                var dbPost = db.Posts.SingleOrDefault(i => i.Id == item.Id);

                dbPost.Published = item.Published;
                dbPost.Title = item.Title;
                dbPost.Content = item.Content;
                dbPost.Saved = DateTime.UtcNow;

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
            var item = await db.Posts.FirstOrDefaultAsync(i => i.Id == id);
            db.Posts.Remove(item);
            await db.SaveChangesAsync();
        }
    }
}