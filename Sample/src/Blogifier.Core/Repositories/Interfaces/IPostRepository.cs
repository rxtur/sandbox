using Blogifier.Core.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blogifier.Core.Repositories.Interfaces
{
    public interface IPostRepository
    {
        Task<List<PostItem>> All();
    }
}
