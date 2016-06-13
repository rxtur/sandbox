using Blogifier.Core.Models;
using Blogifier.Core.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Blogifier.Core.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        BlogifierDbContext _db;
        public CategoryRepository(BlogifierDbContext db)
        {
            _db = db;
        }

        public async Task<List<Category>> Find(Expression<Func<Category, bool>> predicate, int page = 1, int pageSize = 10)
        {
            var skip = page * pageSize - pageSize;

            var cats = _db.Categories.AsNoTracking().Include(c => c.PostCategories)
                .Where(predicate).OrderBy(c => c.Title).AsAsyncEnumerable();

            if (skip == 0)
                return await cats.Take(pageSize).ToList();
            else
                return await cats.Skip(skip).Take(pageSize).ToList();
        }

        public async Task<Category> BySlug(string slug)
        {
            return await _db.Categories.AsNoTracking().Include(c => c.PostCategories)
                .FirstOrDefaultAsync(c => c.Slug == slug);
        }
    }
}
