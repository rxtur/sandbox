using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blogifier.Core.Models;

namespace Blogifier.Core.ViewModels
{
    public class PostDetail
    {
        public Post Post { get; set; }
        public List<CategoryListItem> Categories { get; set; }
    }
}
