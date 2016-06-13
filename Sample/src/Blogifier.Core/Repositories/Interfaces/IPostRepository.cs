using Blogifier.Core.ViewModels;
using Blogifier.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blogifier.Core.Repositories.Interfaces
{
    public interface IPostRepository
    {
        Task<List<PostItem>> All();
        Task<Post> GetBySlug(string slug);
    }
}
