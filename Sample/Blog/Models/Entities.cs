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
        public Blog()
        {
            PostsPerPage = 10;
            DaysToComment = 0;
            IsModerated = false;
            Theme = "standard";
            Image = "site.jpg";
        }
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string AuthorId { get; set; }
        public string AuthorName { get; set; }
        public string AuthorEmail { get; set; }
        public int PostsPerPage { get; set; }
        public int DaysToComment { get; set; }
        public bool IsModerated { get; set; }
        public string Theme { get; set; }
        public string Image { get; set; }
        public bool IsSelected { get; set; }
    }

    public class Post
    {
        public int Id { get; set; }
        [Required]
        public int BlogId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Slug { get; set; }
        public string Content { get; set; }
        public string Tags { get; set; }
        public int Comments { get; set; }
        public bool CommentsEnabled { get; set; }
        public DateTime Saved { get; set; }
        public DateTime Published { get; set; }
        public string AuthorName { get; set; }
        public bool Visible
        {
            get
            {
                if (Published == DateTime.MinValue)
                    return false;

                return Published <= DateTime.UtcNow ? true : false;
            }
        }
        public bool IsSelected { get; set; }
    }

    public class Setting
    {
        public int Id { get; set; }
        public int BlogId { get; set; }
        public string SettingKey { get; set; }
        public string SettingValue { get; set; }
    }
}