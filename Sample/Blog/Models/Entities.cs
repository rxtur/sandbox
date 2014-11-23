using System;
using System.Collections.Generic;

namespace BlogiFire.Models
{
    public class Blog
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public virtual List<Post> Posts { get; set; }
    }

    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int Comments { get; set; }
        public DateTime Created { get; set; }
        public DateTime Published { get; set; }
        public bool Visible
        {
            get
            {
                return Published <= DateTime.UtcNow ? true : false;
            }
        }
        public int BlogId { get; set; }
        public virtual Blog Blog { get; set; }
    }
}