using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityServer4.Services;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RiceShop.Clb;
using RiceShop.Clb.Entity;

namespace IdentityServer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddInMemoryClients(Config.GetClients())
                .AddInMemoryIdentityResources(Config.GetIdentityResources())
                .AddInMemoryApiResources(Config.GetApiResources())
                .AddProfileService<ProfileService>();

            //Inject the classes we just created
            services.AddTransient<IResourceOwnerPasswordValidator, ResourceOwnerPasswordValidator>();
            services.AddTransient<IProfileService, ProfileService>();

            //add Cors Orgine Policy
            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();

            }));
            services.Configure<IdentityOptions>(options =>
            {
                // Default Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 4;
                options.Password.RequiredUniqueChars = 1;
            });
            //  Register DataContext;
            services.AddDbContext<DatadbContext>(options =>
                    //options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
                    options.UseInMemoryDatabase("Db"));

            //  Register Identity Core;
            services.AddIdentity<ApplicationUser, AppIdentityRole>()
               .AddEntityFrameworkStores<DatadbContext>()
               .AddDefaultTokenProviders();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            using (var service = app.ApplicationServices.CreateScope())
            {

                var context = service.ServiceProvider.GetRequiredService<DatadbContext>();
                var user = service.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var role = service.ServiceProvider.GetRequiredService<RoleManager<AppIdentityRole>>();

                string[] roleNames = { "Admin", "Manager", "Member" };

                var u = new ApplicationUser { UserName = "abc", Email = "abc@gmail.com", PasswordHash = "1234@" };

                foreach (var roleName in roleNames)
                {
                    var roleExist = await role.RoleExistsAsync(roleName);
                    if (!roleExist)
                    {
                        //create the roles and seed them to the database: Question 1
                        IdentityResult roleResult = await role.CreateAsync(new AppIdentityRole { Name = roleName, NormalizedName = roleName });
                    }
                }

                IdentityResult result = await user.CreateAsync(u, u.PasswordHash);
                if (result.Succeeded)
                {
                    await user.AddToRoleAsync(u, "Admin");
                    await user.AddClaimAsync(u, new Claim("Permission", "Update"));
                    await user.AddClaimAsync(u, new Claim("Permission", "Read"));
                    await user.AddClaimAsync(u, new Claim("Permission", "Delete"));
                    await user.AddClaimAsync(u, new Claim("Permission", "Add"));

                    context.Categorys.Add(new Category { CategoryName = "Rice", Description = "N/A" });
                    context.SaveChanges();
                    context.Products.Add(new Product { ProductName = "Nazir", Description = "N/A", CategoryId = context.Categorys.First().CategoryId });
                    context.SaveChanges();
                    context.Suppliers.Add(new Supplier { CompanyName = "abc", OwnerName = "Ms Shop", Address = "Dhaka", ContactName = "01744" });
                    context.SaveChanges();


                }


                //context.ApplicationUsers.Add(user);
                //  context.SaveChangesAsync();
            }
            app.UseCors("MyPolicy");
            app.UseIdentityServer();

        }
    }
}
