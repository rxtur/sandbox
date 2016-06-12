using System.Collections.Generic;

namespace Blogifier.Core.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public int ParentId { get; set; }

        public string Slug { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public List<CategoryMeta> Metas { get; set; }
        public List<PostCategory> PostCategories { get; set; }
    }
}
