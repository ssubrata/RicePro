using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RiceShop.Clb;
using RiceShop.Clb.Entity;

namespace RiceShop.Api
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
            services.AddAutoMapper();
            //  Register MVC;
            services.AddMvcCore()
                .AddAuthorization()
                .AddJsonFormatters();

            //  Register DataContext;
            services.AddDbContext<DatadbContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            //  Register Identity Core;
            services.AddIdentity<ApplicationUser, AppIdentityRole>()
               .AddEntityFrameworkStores<DatadbContext>()
               .AddDefaultTokenProviders();

            //register plociy for Cors Origine
            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));
            

            //custom Aithorization with Permisson
            services.AddAuthorization(options =>
            {
                options.AddPolicy("ReadPolicy", policy =>
                 {
                     policy.RequireClaim("Permission","Read");
                 });

                options.AddPolicy("UpdatePolicy", policy =>
                {
                    policy.RequireClaim("Permission", "Update");
                });

                options.AddPolicy("DeletePolicy", policy =>
                {
                    policy.RequireClaim("Permission","Delete");
                });
                options.AddPolicy("AddPolicy", policy =>
                {
                    policy.RequireClaim("Permission","Add");

                });
            });

            //  Register Authetication for Api ;
            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddIdentityServerAuthentication(options =>
                {
                    options.ApiName = "riceShopApi";
                    options.Authority = "https://localhost:5004";
                    options.RequireHttpsMetadata = true;

                });
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseCors("MyPolicy");
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
