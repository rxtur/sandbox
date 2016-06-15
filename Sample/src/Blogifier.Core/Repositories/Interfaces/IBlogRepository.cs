using System.Collections.Generic;

namespace Blogifier.Core.Repositories.Interfaces
{
    public interface IBlogRepository
    {
        List<string> BlogsLookup();
        bool BlogExists(string slug);
    }
}
