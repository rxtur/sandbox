using Blogifier.Core.Models;
using System.Threading.Tasks;

namespace Blogifier.Core.Repositories.Interfaces
{
    public interface IBlogRepository
    {
        bool BlogExists(string slug);
        Task<Blog> BySlug(string slug);
        Task<Blog> ByIdentity(string name);
        Task<Blog> Add(Blog item);
    }
}
