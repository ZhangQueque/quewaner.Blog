using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using quewaner.Blog.ApplicationCore.Entities.ArticleAggregate;
using quewaner.Blog.ApplicationCore.Exceptions;
using quewaner.Blog.ApplicationCore.Interfaces;
using quewaner.Blog.DataTransferObject.ArticleDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace quewaner.Blog.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IAsyncRepository<Article> asyncRepository;
        private readonly IArticleService articleService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger,IAsyncRepository<Article> asyncRepository, IArticleService articleService)
        {
            _logger = logger;
            this.asyncRepository = asyncRepository;
            this.articleService = articleService;
        }

        [HttpGet]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
          
          
            await asyncRepository.AddAsync(new Article("ArticleTitle", "1", "2","3",new ArticleType("Type1", "Type2", "Type3")));
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
