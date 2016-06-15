﻿using Blogifier.Core.Models;
using Blogifier.Core.Repositories.Interfaces;
using Blogifier.Core.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System;

namespace Blogifier.Core.Repositories
{
    public class PostRepository : IPostRepository
    {
        BlogifierDbContext _db;
        public PostRepository(BlogifierDbContext db)
        {
            _db = db;
        }

        public async Task<List<PostItem>> All()
        {
            var postList = _db.Posts.AsNoTracking().OrderByDescending(p => p.Published)
                .Include(p => p.PostCategories).Include(p => p.Blog).ToList();

            var posts = GetItems(postList);

            return await Task.Run(() => posts);
        }

        public async Task<List<PostItem>> Find(Expression<Func<Post, bool>> predicate, int page = 1, int pageSize = 10)
        {
            var skip = page * pageSize - pageSize;

            var postList = _db.Posts.AsNoTracking().Where(predicate).OrderByDescending(p => p.Published)
                .Include(p => p.PostCategories).Include(p => p.Blog).ToList();

            List<PostItem> posts;
            if(skip == 0)
                posts = GetItems(postList).Take(pageSize).ToList();
            else
                posts = GetItems(postList).Skip(skip).Take(pageSize).ToList();

            return await Task.Run(() => posts);
        }

        public async Task<Post> BySlug(string slug)
        {
            return await _db.Posts.AsNoTracking().Include(p => p.Blog).FirstOrDefaultAsync(p => p.Slug == slug);
        }

        public async Task<List<PostItem>> ByCategory(string slug, string blog = "all", int page = 1, int pageSize = 10)
        {
            var skip = page * pageSize - pageSize;
            var cat = _db.Categories.Where(c => c.Slug == slug).FirstOrDefault();
            var posts = blog == "all" ? _db.Posts.AsNoTracking().Include(p => p.Blog).Include(p => p.PostCategories) :
                _db.Posts.AsNoTracking().Include(p => p.Blog).Include(p => p.PostCategories);

            var tmpPosts = new List<Post>();
            if (cat != null)
            {
                foreach (var p in posts)
                {
                    foreach (var c in p.PostCategories)
                    {
                        if (c.CategoryId == cat.CategoryId)
                        {
                            tmpPosts.Add(p);
                            break;
                        }
                    }
                }
            }
            List<PostItem> catPosts;
            if (skip == 0)
                catPosts = GetItems(tmpPosts).Take(pageSize).ToList();
            else
                catPosts = GetItems(tmpPosts).Skip(skip).Take(pageSize).ToList();

            return await Task.Run(() => catPosts);
        }

        private List<PostItem> GetItems(List<Post> postList)
        {
            var posts = new List<PostItem>();
            foreach (var p in postList)
            {
                var item = new PostItem
                {
                    Slug = p.Slug,
                    Title = p.Title,
                    Content = p.Content,
                    Published = p.Published,
                    AuthorName = p.Blog.AuthorName,
                    BlogSlug = p.Blog.Slug,
                    AuthorEmail = p.Blog.AuthorEmail,
                    Categories = new List<CategoryItem>()
                };
                if (p.PostCategories != null && p.PostCategories.Count > 0)
                {
                    foreach (var pc in p.PostCategories)
                    {
                        var cat = _db.Categories.AsNoTracking().Where(c => c.CategoryId == pc.CategoryId).FirstOrDefault();
                        var catItem = new CategoryItem { Slug = cat.Slug, Title = cat.Title };
                        item.Categories.Add(catItem);
                    }
                }
                posts.Add(item);
            }
            return posts;
        }
    }
}