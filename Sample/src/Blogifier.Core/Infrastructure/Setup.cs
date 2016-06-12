using Blogifier.Core.Models;
using System;

namespace Blogifier.Core.Infrastructure
{
    public class Setup
    {
        private BlogifierDbContext _context;

        public Setup(BlogifierDbContext context)
        {
            _context = context;
        }

        public void SeedData()
        {
            _context.Database.EnsureCreated();

            SeedBlogs();
            SeedCategories();
            SeedPosts();
            _context.SaveChanges();

            SeedPostCategories();
            _context.SaveChanges();
        }

        private void SeedBlogs()
        {
            _context.Blogs.Add(new Blog { Title = "Dogs blog", Description = "Awesome blog about dogs", Slug = "dog-blog", Saved = DateTime.Now, AuthorName = "Doug Johnson", AuthorEmail = "doug@us.com" });
            _context.Blogs.Add(new Blog { Title = "Cats and kittens", Description = "All about cats and kittens", Slug = "cats-kittens", Saved = DateTime.Now, AuthorName = "Katty Jones", AuthorEmail = "katty@us.com" });
        }

        private void SeedCategories()
        {
            _context.Categories.Add(new Category { Title = "Uncategorized", Slug = "uncategorized", Description = "Uncategorized posts", ParentId = 0 });
            _context.Categories.Add(new Category { Title = "Pets", Slug = "pets", Description = "Posts about pets", ParentId = 0 });
            _context.Categories.Add(new Category { Title = "Dogs", Slug = "dogs", Description = "Posts about dogs", ParentId = 2 });
            _context.Categories.Add(new Category { Title = "Labradors", Slug = "labrador", Description = "Posts about labradors", ParentId = 3 });
            _context.Categories.Add(new Category { Title = "Bulldogs", Slug = "bulldogs", Description = "Posts about bulldogs", ParentId = 3 });
            _context.Categories.Add(new Category { Title = "Cats", Slug = "cats", Description = "Posts about cats", ParentId = 2 });
            _context.Categories.Add(new Category { Title = "Persian", Slug = "persian", Description = "About persian cats", ParentId = 6 });
        }

        private void SeedPosts()
        {
            _context.Posts.Add(new Post {
                BlogId = 1,
                Slug = "uncategorized-post",
                Title = "Uncategorized post",
                Description = "This is uncategorized post example. Cumque pariatur voluptatem, facilis modi at obcaecati voluptates rem, sunt atque repellat officiis illum numquam dolorem ea consequuntur laborum nobis.",
                Content = "This is uncategorized post example. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Cumque pariatur voluptatem, facilis modi at obcaecati voluptates rem, sunt atque repellat officiis illum numquam dolorem ea consequuntur laborum nobis. Laudantium, suscipit.",
                Saved = DateTime.Now.AddDays(-45),
                Published = DateTime.Now.AddDays(-45)
            });
            _context.Posts.Add(new Post {
                BlogId = 2,
                Slug = "post-about-dogs",
                Title = "Post about dogs",
                Description = "This is the post about dogs. Cumque pariatur voluptatem, facilis modi at obcaecati voluptates rem, sunt atque repellat officiis illum numquam dolorem ea consequuntur laborum nobis.",
                Content = "This is the post about dogs. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Cumque pariatur voluptatem, facilis modi at obcaecati voluptates rem, sunt atque repellat officiis illum numquam dolorem ea consequuntur laborum nobis. Laudantium, suscipit.",
                Saved = DateTime.Now.AddDays(-40),
                Published = DateTime.Now.AddDays(-40)
            });
            _context.Posts.Add(new Post {
                BlogId = 2,
                Slug = "first-post-about-labradors",
                Title = "First post about labradors",
                Description = "This is the first post about labradors. Cumque pariatur voluptatem, facilis modi at obcaecati voluptates rem, sunt atque repellat officiis illum numquam dolorem ea consequuntur laborum nobis.",
                Content = "This is the first post about labradors. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Cumque pariatur voluptatem, facilis modi at obcaecati voluptates rem, sunt atque repellat officiis illum numquam dolorem ea consequuntur laborum nobis. Laudantium, suscipit.",
                Saved = DateTime.Now.AddDays(-35),
                Published = DateTime.Now.AddDays(-35)
            });
            _context.Posts.Add(new Post
            {
                BlogId = 2,
                Slug = "second-post-about-labradors",
                Title = "Second post about labradors",
                Description = "This is the second post about labradors. Cumque pariatur voluptatem, facilis modi at obcaecati voluptates rem, sunt atque repellat officiis illum numquam dolorem ea consequuntur laborum nobis.",
                Content = "This is the second post about labradors. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Cumque pariatur voluptatem, facilis modi at obcaecati voluptates rem, sunt atque repellat officiis illum numquam dolorem ea consequuntur laborum nobis. Laudantium, suscipit.",
                Saved = DateTime.Now.AddDays(-30),
                Published = DateTime.Now.AddDays(-30)
            });
            _context.Posts.Add(new Post
            {
                BlogId = 2,
                Slug = "post-about-bulldogs",
                Title = "Post about bulldogs",
                Description = "This is the post about bulldogs. Cumque pariatur voluptatem, facilis modi at obcaecati voluptates rem, sunt atque repellat officiis illum numquam dolorem ea consequuntur laborum nobis.",
                Content = "This is the post about bulldogs. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Cumque pariatur voluptatem, facilis modi at obcaecati voluptates rem, sunt atque repellat officiis illum numquam dolorem ea consequuntur laborum nobis. Laudantium, suscipit.",
                Saved = DateTime.Now.AddDays(-25),
                Published = DateTime.Now.AddDays(-25)
            });
            _context.Posts.Add(new Post
            {
                BlogId = 1,
                Slug = "post-about-persian-cats",
                Title = "Post about persian cats",
                Description = "This is the post about persian cats. Cumque pariatur voluptatem, facilis modi at obcaecati voluptates rem, sunt atque repellat officiis illum numquam dolorem ea consequuntur laborum nobis.",
                Content = "This is the post about persian cats. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Cumque pariatur voluptatem, facilis modi at obcaecati voluptates rem, sunt atque repellat officiis illum numquam dolorem ea consequuntur laborum nobis. Laudantium, suscipit.",
                Saved = DateTime.Now.AddDays(-5),
                Published = DateTime.Now.AddDays(-5)
            });
        }

        private void SeedPostCategories()
        {
            _context.PostCategories.Add(new PostCategory { CategoryId = 1, PostId = 1 });
            _context.PostCategories.Add(new PostCategory { CategoryId = 3, PostId = 2 });
            _context.PostCategories.Add(new PostCategory { CategoryId = 4, PostId = 3 });
            _context.PostCategories.Add(new PostCategory { CategoryId = 4, PostId = 4 });
            _context.PostCategories.Add(new PostCategory { CategoryId = 3, PostId = 5 });
            _context.PostCategories.Add(new PostCategory { CategoryId = 5, PostId = 5 });
            _context.PostCategories.Add(new PostCategory { CategoryId = 6, PostId = 6 });
            _context.PostCategories.Add(new PostCategory { CategoryId = 7, PostId = 6 });
        }

    }
}
