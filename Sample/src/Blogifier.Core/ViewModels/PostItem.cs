using System;
using System.Collections.Generic;

namespace Blogifier.Core.ViewModels
{
    public class PostItem
    {
        public string Slug { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime Published { get; set; }
        public ICollection<CategoryItem> Categories { get; set; }
    }
}
