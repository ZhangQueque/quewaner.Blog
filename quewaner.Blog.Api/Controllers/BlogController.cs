using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using quewaner.Blog.ApplicationCore.Entities.ArticleAggregate;
using quewaner.Blog.ApplicationCore.Exceptions;
using quewaner.Blog.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace quewaner.Blog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IAsyncRepository<Article> _articleRepository;
        public BlogController(ILogger<WeatherForecastController> logger, IAsyncRepository<Article> articleRepository)
        {
            _logger = logger;
            _articleRepository = articleRepository;
        }
    }
}
