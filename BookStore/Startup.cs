using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using BookStore.Data;
using BookStore.Models.Services;
using BookStore.Models.Entities;
using Microsoft.AspNetCore.Identity;

namespace BookStore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddDbContext<BookStoreContext>(options =>
                    options.UseMySql(Configuration.GetConnectionString("conn"), builder =>
                    builder.MigrationsAssembly("BookStore")));

            services.AddScoped<SeedingService>();
            services.AddScoped<BookService>();
            services.AddScoped<AuthorService>();
            services.AddScoped<CategoryService>();
            services.AddScoped<PublisherService>();
            services.AddScoped<LoginService>();

            //REGISTER API SERVICES
            services.AddHttpClient("BookApi", client =>
            {
                client.BaseAddress = new Uri("https://localhost:44334/api/APIBooks/");
            });

            services.AddHttpClient("CategoryApi", client =>
            {
                client.BaseAddress = new Uri("https://localhost:44334/api/APICategory/");
            });

            services.AddHttpClient("AuthorApi", client =>
            {
                client.BaseAddress = new Uri("https://localhost:44334/api/APIAuthor/");
            });

            services.AddHttpClient("PublisherApi", client =>
            {
                client.BaseAddress = new Uri("https://localhost:44334/api/APIPublisher/");
            });

            // Add Identity services
            services.AddIdentity<User, IdentityRole<int>>()
                .AddEntityFrameworkStores<BookStoreContext>()
                .AddDefaultTokenProviders();

            // Add authentication and authorization services
            services.AddAuthentication()
                .AddCookie(options =>
                {
                    options.LoginPath = "/Account/Login";  // Path to login page
                    options.LogoutPath = "/Account/Logout"; // Path to logout page
                });

            // Add MVC and Razor Pages
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, SeedingService ss)
        {
            CultureInfo enUs = new CultureInfo("en-US");

            RequestLocalizationOptions localizationOptions = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(enUs),
                SupportedCultures = new List<CultureInfo>() { enUs },
                SupportedUICultures = new List<CultureInfo>() { enUs }
            };

            app.UseRequestLocalization(localizationOptions);

            if (env.IsDevelopment())
            {
                ss.Seeding();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            // Enable authentication and authorization
            app.UseAuthentication();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Login}/{action=Index}/{id?}");
            });
        }
    }
}
