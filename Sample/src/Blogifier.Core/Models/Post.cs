using System;
using System.Collections.Generic;

namespace Blogifier.Core.Models
{
    public class Post
    {
        public Post() { }

        public int PostId { get; set; }
        public int BlogId { get; set; }

        public string Slug { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }

        public DateTime Saved { get; set; }
        public DateTime Published { get; set; }

        public Blog Blog { get; set; }
        public List<PostMeta> Metas { get; set; }
        public List<PostCategory> PostCategories { get; set; }
    }
}