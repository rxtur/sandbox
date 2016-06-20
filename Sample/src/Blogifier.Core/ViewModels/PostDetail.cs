using Blogifier.Core.Models;
using System.Collections.Generic;

namespace Blogifier.Core.ViewModels
{
    public class PostDetail
    {
        public PostDetail()
        {
            Post = new Post();
            Categories = new List<CategoryListItem>();
        }
        public Post Post { get; set; }
        public List<CategoryListItem> Categories { get; set; }
    }
}
