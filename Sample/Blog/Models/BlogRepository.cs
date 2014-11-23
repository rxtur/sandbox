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
            return await db.Blogs.OrderBy(b => b.Name).ToListAsync();
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
        }
        public async Task Update(Blog item)
        {
            try
            {
                var dbItem = db.Blogs.SingleOrDefault(i => i.Id == item.Id);

                dbItem.Name = item.Name;

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
    }
}