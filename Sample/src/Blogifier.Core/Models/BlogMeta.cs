namespace Blogifier.Core.Models
{
    public class BlogMeta
    {
        public int BlogMetaId { get; set; }

        public int BlogId { get; set; }
        public string MetaKey { get; set; }
        public string MetaValue { get; set; }
    }
}