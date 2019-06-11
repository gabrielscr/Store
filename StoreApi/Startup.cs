using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StoreApi.Infra;

namespace StoreApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddFeatureFolders();

            services.AddMediatR(typeof(Startup));

            services.AddDbContext<StoreContext>(opts =>
            {
                opts.UseSqlServer(Configuration.GetConnectionString("StoreConnection"), opt => opt.EnableRetryOnFailure());
            });

            services.AddCors(opts =>
            {
                opts.AddPolicy("Dev", opt => opt.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().AllowCredentials());
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();

            }
            else
            {
                app.UseHsts();
            }

            app.UseCors("Dev");

            app.UseHttpsRedirection();

            app.UseMvc(opts =>
            {
                opts.MapRoute(
                    name: "default",
                    template: "api/{controller}/{action}/{id?}");
            });

            if (env.IsDevelopment())
            {
                app.UseWelcomePage();
            }
        }

    }
}
