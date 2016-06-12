﻿namespace Blogifier.Core.Models
{
    public class PostCategory
    {
        public int PostCategoryId { get; set; }

        public int PostId { get; set; }
        public Post Post { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}