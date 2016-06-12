using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blogifier.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Blogifier.Core.ViewModels
{
    public class PostListVM
    {
        public PostListVM(BlogifierDbContext db)
        {
            PageCnt = 2;

            var posts = new List<Post>();
            var cats = new List<Category>();

            var postList = db.Posts.OrderByDescending(p => p.Published).Include(p => p.PostCategories).ToList();

            //foreach (var p in postList)
            //{
            //    if(p.PostCategories != null && p.PostCategories.Count > 0)
            //    {
            //        //cats = db.Categories.Where(c => c.CategoryId)
            //        //var ids = arraylist.Cast<string>().ToList();
            //        //var tmp = db.Categories.Select(c => ids.Contains(c.CategoryId)).ToList();
            //        foreach (var pc in p.PostCategories)
            //        {
            //            var z = p.PostCategories;
            //            var x = db.Categories.Where(c => c.CategoryId == pc.CategoryId).ToList()[0];
            //            //p.AddCategory(x);
            //        }
            //    }
            //}

            Posts = postList;
        }
        public int PageCnt { get; set; }

        public List<Post> Posts { get; set; }
    }
}
