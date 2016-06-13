using Blogifier.Core.Models;
using Blogifier.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Blogifier.Core.Repositories.Interfaces
{
    public interface IPostRepository
    {
        Task<List<PostItem>> All();
        Task<List<PostItem>> ByCategory(string slug, string blog = "all", int page = 1, int pageSize = 10);
        Task<List<PostItem>> Find(Expression<Func<Post, bool>> predicate, int page = 1, int pageSize = 10);
        Task<Post> BySlug(string slug);
    }
}
