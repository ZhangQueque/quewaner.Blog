using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using quewaner.Blog.ApplicationCore.Interfaces;
using quewaner.Blog.Infrastructure.Config;
using quewaner.Blog.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using System.Reflection;
using quewaner.Blog.ApplicationCore.Services;
using System.IO;
using quewaner.Blog.ApplicationCore.Profiles;

namespace quewaner.Blog.Api
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

            services.AddControllers();

            //配置跨域
            services.AddCors(options=>options.AddPolicy("cors", set=>set.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin()));

            //配置Swagger文章注释
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "quewaner.Blog.Api", Version = "v1" });
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "quewaner.Blog.Api.xml"));
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "quewaner.Blog.ApplicationCore.xml"));
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "quewaner.Blog.DataTransferObject.xml"));

            });
       
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            //注配置文件内容            
            services.Configure<MongoDatabaseSettings>(Configuration.GetSection("MongoDatabaseSettings"));
            //这地方只是演示怎么从服务中给 接口注册 相对应的配置文件信息
            services.AddSingleton<IMongoDatabaseSettings>(sp => sp.GetRequiredService<IOptions<MongoDatabaseSettings>>().Value);

            services.AddScoped(typeof(IAsyncRepository<>),typeof(MongoDBRepository<>));

            services.AddScoped<IArticleService, ArticleService>();

         }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "quewaner.Blog.Api v1"));
            }
            else
            { app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "quewaner.Blog.Api v1"); });
            }
            app.UseCors("cors");
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
