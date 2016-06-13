﻿using Blogifier.Core.Models;
using Blogifier.Core.Repositories;
using Blogifier.Core.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Blogifier.Web.Bootstrap
{
    public class Init
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddSingleton<IPostRepository, PostRepository>();
            services.AddSingleton<IBlogRepository, BlogRepository> ();

            var connection = @"Server=.\\SQLEXPRESS;Database=Blogifier;Trusted_Connection=True;MultipleActiveResultSets=true";
            services.AddDbContext<BlogifierDbContext>(options => options.UseSqlServer(connection));
        }
    }
}
