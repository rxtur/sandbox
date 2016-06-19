using Blogifier.Core.Models;
using Blogifier.Core.ViewModels;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Blogifier.Core.Repositories.Interfaces
{
    public interface IPostRepository
    {
        Task<PostList> Find(Expression<Func<Post, bool>> predicate, int page = 1, int pageSize = 10);
        Task<PostList> ByCategory(string slug, string blog = "all", int page = 1, int pageSize = 10);
        PostDetail BySlug(string slug);
    }
}
