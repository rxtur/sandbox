using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Blogifier.Core.Models
{
    public class Blog
    {
        public Blog() { }

        public int BlogId { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Slug { get; set; }

        [Required]
        [StringLength(160, MinimumLength = 2)]
        public string Title { get; set; }

        [Required]
        [StringLength(250, MinimumLength = 2)]
        public string Description { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string AuthorName { get; set; }

        [Required]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Email is not valid.")]
        [DataType(DataType.EmailAddress)]
        public string AuthorEmail { get; set; }

        public DateTime Saved { get; set; }

        public List<Post> Posts { get; set; }
        public List<BlogMeta> Metas { get; set; }
        public List<Asset> Assets { get; set; }
    }
}