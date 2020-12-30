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
            services.AddSwaggerGen(c => //≈‰÷√SwaggerŒƒ’¬◊¢ Õ
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "quewaner.Blog.Api", Version = "v1" });
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "quewaner.Blog.Api.xml"));
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "quewaner.Blog.ApplicationCore.xml"));
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "quewaner.Blog.DataTransferObject.xml"));

            });

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            //◊¢≈‰÷√Œƒº˛ƒ⁄»› 
            services.Configure<MongoDatabaseSettings>(Configuration.GetSection("MongoDatabaseSettings"));
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
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "quewaner.Blog.Api v1"); });
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
