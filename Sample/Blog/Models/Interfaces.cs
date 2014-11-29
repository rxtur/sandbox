using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BlogiFire.Models
{
    public interface IBlogRepository
    {
        Task<List<Blog>> All();
        Task<List<Blog>> Find(Expression<Func<Blog, bool>> predicate);
        Task<Blog> GetById(int id);
        Task Add(Blog item);
        Task Update(Blog item);
        Task Delete(int id);
    }

    public interface IPostRepository
    {
        Task<List<Post>> All();
        Task<List<Post>> Find(Expression<Func<Post, bool>> predicate, int page = 1, int pageSize = 10);
        Task<Post> GetById(int id);
        Task Add(Post item);
        Task Update(Post item);
        Task Delete(int id);
    }

    public interface ISettingsRepository
    {
        Task<List<Setting>> Find(Expression<Func<Setting, bool>> predicate);
        Task Add(Setting item);
        Task Update(Setting item);
        Task Delete(int id);
    }
}