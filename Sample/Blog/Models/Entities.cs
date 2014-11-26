using System;
using System.ComponentModel.DataAnnotations;

namespace BlogiFire.Models
{
    public class Author
    {
        public string Name { get; set; }
        public bool IsAuthenticated { get; set; }
        public bool HasBlog { get; set; }
    }

    public class Blog
    {
        public int Id { get; set; }
        [Required]
        [StringLength(120, MinimumLength = 2)]
        public string Name { get; set; }
        [Required]
        [StringLength(160, MinimumLength = 2)]
        public string Description { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Author { get; set; }
    }

    public class Post
    {
        public int Id { get; set; }
        [Required]
        public int BlogId { get; set; }
        [Required]
        [StringLength(160, MinimumLength = 2)]
        public string Title { get; set; }
        [Required]
        [StringLength(160, MinimumLength = 2)]
        public string Slug { get; set; }
        public string Content { get; set; }
        [StringLength(250, MinimumLength = 2)]
        public string Tags { get; set; }
        public int Comments { get; set; }
        public bool CommentsEnabled { get; set; }
        public DateTime Saved { get; set; }
        public DateTime Published { get; set; }
        public bool Visible
        {
            get
            {
                if (Published == DateTime.MinValue)
                    return false;

                return Published <= DateTime.UtcNow ? true : false;
            }
        }
    }
}