namespace Blogifier.Core.Infrastructure
{
    public class AppSettings
    {
        public static bool UseInMemoryDb { get; set; }
        public static bool InitializeData { get; set; }
        public static string ConnectionString { get; set; }
        public static string Title { get; set; }
        public static string Description { get; set; }
    }
}
