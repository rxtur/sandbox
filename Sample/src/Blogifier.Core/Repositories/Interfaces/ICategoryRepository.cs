using Blogifier.Core.Models;
using Blogifier.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Blogifier.Core.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<Category>> Find(Expression<Func<Category, bool>> predicate, int page = 1, int pageSize = 10);
        Task<Category> BySlug(string slug);
    }
}
