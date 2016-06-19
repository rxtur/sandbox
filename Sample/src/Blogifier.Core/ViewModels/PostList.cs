using System.Collections.Generic;

namespace Blogifier.Core.ViewModels
{
    public class PostList
    {
        public PostList(int page, int pageSize)
        {
            Posts = new List<PostListItem>();
            CurrentPage = page;
            PageSize = pageSize;
        }
        public List<PostListItem> Posts { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalCnt { get; set; }
        public Pager Pager
        {
            get
            {
                return new Pager(CurrentPage, PageSize, TotalCnt);
            }
        }
    }
}