namespace Blogifier.Core.Models
{
    public class CategoryMeta
    {
        public int CategoryMetaId { get; set; }

        public int CategoryId { get; set; }
        public string MetaKey { get; set; }
        public string MetaValue { get; set; }
    }
}