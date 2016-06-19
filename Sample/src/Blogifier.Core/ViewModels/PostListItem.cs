using System;
using System.Collections.Generic;

namespace Blogifier.Core.ViewModels
{
    public class PostListItem
    {
        public string Slug { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime Published { get; set; }

        public string BlogSlug { get; set; }
        public string AuthorName { get; set; }
        public string AuthorEmail { get; set; }

        public ICollection<CategoryListItem> Categories { get; set; }
    }
}
