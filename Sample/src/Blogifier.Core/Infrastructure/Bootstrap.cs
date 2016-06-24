using Blogifier.Core.Infrastructure;
using Blogifier.Core.Models;
using Blogifier.Core.Repositories;
using Blogifier.Core.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Blogifier.Core.Infrastructure
{
    public class Bootstrap
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddSingleton<IPostRepository, PostRepository>();
            services.AddSingleton<IBlogRepository, BlogRepository>();
            services.AddSingleton<ICategoryRepository, CategoryRepository>();

            AppSettings.UseInMemoryDb = true;
            AppSettings.InitializeData = true;

            AppSettings.Title = "Name of the blog";
            AppSettings.Description = "Short description of the blog";
            AppSettings.Theme = "Standard";
            AppSettings.ItemsPerPage = 2;
            AppSettings.ConnectionString = "Server=.\\SQLEXPRESS;Database=Blogifier;Trusted_Connection=True;MultipleActiveResultSets=true";

            if (AppSettings.UseInMemoryDb)
            {
                services.AddDbContext<BlogifierDbContext>(options => options.UseInMemoryDatabase());
            }
            else
            {
                services.AddDbContext<BlogifierDbContext>(options => options.UseSqlServer(AppSettings.ConnectionString));
            }

            if (AppSettings.InitializeData)
            {
                var setup = new Setup();
                setup.SeedData();
            }
        }
    }
}
