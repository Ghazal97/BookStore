using Bookstore.Models;
using Bookstore.Models.Repos;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
<<<<<<< HEAD
using Microsoft.AspNetCore.Routing;
=======
>>>>>>> 5d1c906842388b669b23cd2408486d226166797e
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bookstore
{
    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        //public IConfiguration configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            //services.AddSingleton<IBookstoreData<TEntity>, AuthorRepository>();
            services.AddScoped<IBookstoreRepository<Author>,AuthorDBRepository>();
            services.AddScoped<IBookstoreRepository<Book>,BookDBRepository>();
            services.AddDbContext<BookStoreDbContext>(Options => 
            {
                Options.UseSqlServer(configuration.GetConnectionString("SqlCon"));
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            //app.UseMvcWithDefaultRoute();
            app.UseMvc(route  => {

                route.MapRoute("default", "{controller=Book}/{action=Index}/{id?}");
            });
        }
    }
}