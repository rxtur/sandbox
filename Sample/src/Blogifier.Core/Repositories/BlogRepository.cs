﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blogifier.Core.Repositories.Interfaces;
using Blogifier.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Blogifier.Core.Repositories
{
    public class BlogRepository : IBlogRepository
    {
        BlogifierDbContext _db;
        public BlogRepository(BlogifierDbContext db)
        {
            _db = db;
        }

        public bool BlogExists(string slug)
        {
            return _db.Blogs.Select(b => b.Slug == slug).ToList().Count > 0;
        }

        public List<string> BlogsLookup()
        {
            return _db.Blogs.Select(b => b.Slug).ToList();
        }
    }
}
