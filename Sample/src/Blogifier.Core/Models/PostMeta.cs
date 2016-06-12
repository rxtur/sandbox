namespace Blogifier.Core.Models
{
    public class PostMeta
    {
        public int PostMetaId { get; set; }

        public int PostId { get; set; }
        public string MetaKey { get; set; }
        public string MetaValue { get; set; }
    }
}