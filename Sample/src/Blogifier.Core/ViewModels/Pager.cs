namespace Blogifier.Core.ViewModels
{
    public class Pager
    {
        public Pager(int page, int size, int total)
        {
            NewerPage = page - 1;
            NewerCss = page > 1 ? "previous" : "previous disabled";

            OlderPage = page + 1;
            var lastItem = page * size;
            OlderCss = total > lastItem ? "next" : "next disabled";

            if (page < 1 || lastItem > total + size)
            {
                RedirectToError = true;
            }
        }
        public int OlderPage { get; set; }
        public string OlderCss { get; set; }
        public int NewerPage { get; set; }
        public string NewerCss { get; set; }
        public bool RedirectToError { get; set; }
    }
}
