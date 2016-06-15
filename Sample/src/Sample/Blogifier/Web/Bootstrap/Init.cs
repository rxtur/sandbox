using Blogifier.Core.Infrastructure;
using Blogifier.Core.Models;
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
            services.AddSingleton<IBlogRepository, BlogRepository>();
            services.AddSingleton<ICategoryRepository, CategoryRepository>();

            AppSettings.UseInMemoryDb = true;
            AppSettings.InitializeData = true;

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
