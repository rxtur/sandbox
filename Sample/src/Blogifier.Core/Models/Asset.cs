namespace Blogifier.Core.Models
{
    public class Asset
    {
        public int AssetId { get; set; }
        public int BlogId { get; set; }

        public string Url { get; set; }
        public string Title { get; set; }

        public AssetType AssetType { get; set; }
    }

    public enum AssetType
    {
        Image = 0,
        Attachment = 1
    }
}
