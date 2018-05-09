using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookCave.Data;
using BookCave.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace BookCave
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            //ServiceProvider = serviceProvider; 
        }

        public IConfiguration Configuration { get; }
        //public IServiceProvider ServiceProvider {get ;}
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AuthenticationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("AuthenticationConnection")));
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<AuthenticationDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(config => {
                config.User.RequireUniqueEmail = true;
                config.Password.RequiredLength = 8;
            });
            services.ConfigureApplicationCookie(options => {
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromHours(3);

                options.LoginPath = "/Account/Login";
                options.AccessDeniedPath = "/Account/AccessDenied";
                options.SlidingExpiration = true;
            });
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            await CreateRolesAndUsers(serviceProvider);
        }
        
        private async Task CreateRolesAndUsers(IServiceProvider serviceProvider)
        {


            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            string[] roleNames = { "Admin", "User" };
            IdentityResult roleResult;
            foreach (var roleName in roleNames)
            {
                var roleExist = await RoleManager.RoleExistsAsync(roleName);
                // athuga hvort role er til
                if (!roleExist)
                {
                    //bua til role og setja i database
                    roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
            //finna user
            var user = await UserManager.FindByEmailAsync("admin@admin.is");

            if(user != null)
            {
                var adminUser = new ApplicationUser
                {
                    UserName = "admin@admin.is",
                    Email = "admin@admin.is",
                    FirstName = "admin",
                    LastName = "admin",
                    Street = "",
                    ZipCode = "",
                    City = "",
                    Country = "",
                    Gender = "",
                    Phone = "",
                };
                string adminPassword = "Admin123!";
                var createAdminUser = await UserManager.CreateAsync(adminUser, adminPassword);
                if (createAdminUser.Succeeded)
                {
                    //Admin gefið role
                    await UserManager.AddToRoleAsync(adminUser, "Admin");
                }
            }
        }
    }
}
