using System.Text;
using System.Text.RegularExpressions;

namespace Blogifier.Core.Infrastructure
{
    public class Utility
    {
        public static string SlugFromTitle(string title)
        {
            title = title.ToLowerInvariant();
            var bytes = Encoding.GetEncoding("utf-8").GetBytes(title);
            title = Encoding.ASCII.GetString(bytes);
            title = Regex.Replace(title, @"\s", "-", RegexOptions.Compiled);
            title = Regex.Replace(title, @"[^a-z0-9\s-_]", "", RegexOptions.Compiled);
            title = title.Trim('-', '_');
            title = Regex.Replace(title, @"([-_]){2,}", "$1", RegexOptions.Compiled);
            return title;
        }
    }
}
