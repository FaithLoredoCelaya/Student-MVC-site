using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FLC_Lab3.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FLC_Lab3
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;
        public Startup(IWebHostEnvironment env)
        {
            Configuration = new ConfigurationBuilder().SetBasePath(env.ContentRootPath).AddJsonFile("appsettings.json").Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddSession(so =>
            {
                so.IdleTimeout = TimeSpan.FromSeconds(60);
            });
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration["ConnectionString:ConnectionString"]));
            services.AddTransient<EditPlus>();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
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

            app.UseAuthorization();

            app.UseStatusCodePages();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("Home", "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute("Students", "{controller=Students}/{action=Index}/{id?}");
                endpoints.MapControllerRoute("Courses", "{controller=Courses}/{action=Index}/{id?}");
                endpoints.MapControllerRoute("Enrollments", "{controller=Enrollments}/{action=Index}/{id?}");
            });
        }
    }
}
