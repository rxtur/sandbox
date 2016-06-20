using Blogifier.Core.Models;
using Blogifier.Core.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Blogifier.Core.Repositories
{
    public class BlogRepository : IBlogRepository
    {
        BlogifierDbContext _db;
        public BlogRepository(BlogifierDbContext db)
        {
            _db = db;
        }

        public bool BlogExists(string slug)
        {
            return _db.Blogs.Select(b => b.Slug == slug).ToList().Count > 0;
        }

        public async Task<Blog> BySlug(string slug)
        {
            return await _db.Blogs.AsNoTracking()
                .FirstOrDefaultAsync(b => b.Slug == slug);
        }

        public int IdFromSlug(string slug)
        {
            var item = _db.Blogs.AsNoTracking()
                .FirstOrDefault(b => b.Slug == slug);

            return item.BlogId;
        }
    }
}
