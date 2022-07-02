using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RedisTest.Repository.Interface;
using RedisTest.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.IO;
using Microsoft.EntityFrameworkCore;
using RedisTest.Models.DBEntity;
using RedisTest.Service.Interface;
using RedisTest.Service;

namespace RedisTest
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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "RedisTest", Version = "v1" });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    { new OpenApiSecurityScheme(){ }, new List<string>() }
                });

                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                //c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });

            //註冊資料庫
            services.AddDbContext<NumberDBContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("NumberDB")));

            //註冊 Redis
            services.AddStackExchangeRedisCache(options => options.Configuration = Configuration["RedisConfig:RedisMemoryCache"]);
            //注入Service
            services.AddTransient<INumberService, NumberService>();
            //注入Repositorys
            services.AddTransient<IMemoryCacheRepository, MemoryCacheRepository>();
            services.AddTransient<INumberRepository, NumberRepository>();
            services.AddTransient<IDBRepository, DBRepository>();
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RedisTest"));
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
