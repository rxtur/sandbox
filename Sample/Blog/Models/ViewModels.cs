using System;
using System.Collections.Generic;

namespace BlogiFire.Models
{
    public class PostViewModel
    {
        public Blog Blog { get; set; }
        public List<Post> Posts { get; set; }
    }
}