﻿using Blogifier.Core.Models;
using Blogifier.Core.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace Blogifier.Core.Repositories
{
    public class BlogRepository : IBlogRepository
    {
        BlogifierDbContext _db;
        public BlogRepository(BlogifierDbContext db)
        {
            _db = db;
        }

        public async Task<Blog> Add(Blog item)
        {
            item.Saved = DateTime.UtcNow;
            item.Slug = SlugFromTitle(item.Title);

            _db.Blogs.Add(item);
            await _db.SaveChangesAsync();

            var added = _db.Blogs.Where(b => b.Slug == item.Slug).FirstOrDefault();
            return added;
        }

        public bool BlogExists(string slug)
        {
            return _db.Blogs.Where(b => b.Slug == slug).ToList().Count > 0;
        }

        public async Task<Blog> ByIdentity(string name)
        {
            return await _db.Blogs.AsNoTracking()
                .FirstOrDefaultAsync(b => b.IdentityName == name);
        }

        public async Task<Blog> BySlug(string slug)
        {
            return await _db.Blogs.AsNoTracking()
                .FirstOrDefaultAsync(b => b.Slug == slug);
        }

        public async Task Delete(int id)
        {
            var item = await _db.Blogs.FirstOrDefaultAsync(i => i.BlogId == id);
            _db.Blogs.Remove(item);
            await _db.SaveChangesAsync();
        }

        public int IdFromSlug(string slug)
        {
            var item = _db.Blogs.AsNoTracking()
                .FirstOrDefault(b => b.Slug == slug);

            return item.BlogId;
        }

        public async Task<Blog> Update(Blog item)
        {
            var itemToUpdate = await _db.Blogs.FirstOrDefaultAsync(i => i.BlogId == item.BlogId);
                                    
            itemToUpdate.Title = item.Title;
            itemToUpdate.Slug = item.Slug;
            itemToUpdate.Description = item.Description;
            itemToUpdate.AuthorEmail = item.AuthorEmail;
            itemToUpdate.AuthorName = item.AuthorName;
            itemToUpdate.Saved = DateTime.UtcNow;

            _db.Blogs.Update(itemToUpdate);
            await _db.SaveChangesAsync();

            //if (item.Metas == null || item.Metas.Count == 0)
            //{
            //    //TODO: save meta
            //}
            return itemToUpdate;
        }

        private string SlugFromTitle(string title)
        {
            var slug = Infrastructure.Utility.SlugFromTitle(title);
            if (_db.Blogs.AsNoTracking().Where(b => b.Slug == slug).FirstOrDefault() != null)
            {
                for (int i = 2; i < 100; i++)
                {
                    if (_db.Blogs.AsNoTracking().Where(b => b.Slug == slug + i.ToString()).FirstOrDefault() == null)
                    {
                        return slug + i.ToString();
                    }
                }
            }
            return slug;
        }
    }
}
