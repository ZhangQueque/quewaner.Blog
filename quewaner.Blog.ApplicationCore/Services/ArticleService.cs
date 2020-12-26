 using quewaner.Blog.ApplicationCore.Exceptions;
using quewaner.Blog.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using quewaner.Blog.ApplicationCore.Entities.ArticleAggregate;
using quewaner.Blog.DataTransferObject.ArticleDto;
using AutoMapper;
using Ardalis.GuardClauses;
using Newtonsoft.Json;

namespace quewaner.Blog.ApplicationCore.Services
{
    /// <summary>
    /// 文章服务
    /// </summary>
    public class ArticleService : IArticleService
    {
        private readonly ILogger<ArticleService> _logger;
        private readonly IAsyncRepository<Article> _articleRepository;
        private readonly IAsyncRepository<ArticleType> _articleTypeRepository;
        private readonly IMapper _mapper;

        public ArticleService(ILogger<ArticleService> logger, IAsyncRepository<Article> articleRepository , IAsyncRepository<ArticleType> articleTypeRepository,IMapper mapper )
        {
            _logger = logger;
            _articleRepository = articleRepository;
            _articleTypeRepository = articleTypeRepository;
            _mapper = mapper;
        }


        /// <summary>
        /// 添加文章
        /// </summary>
        /// <param name="addArticleDto">添加文章对象</param>
        /// <returns></returns>
        public async Task<Article> AddArticleAsync(AddArticleDto addArticleDto)
        {
            ArticleType articleType = await _articleTypeRepository.GetByIdAsync(addArticleDto.ArticleTypeId);
            Guard.Against.NullArticleType(addArticleDto.ArticleTypeId, articleType);
            var article = _mapper.Map<Article>(addArticleDto);
            article.SetArticleType(articleType);
            if (await _articleRepository.AddAsync(article))
            {
                return article;
            }
            _logger.LogError($"文章添加失败。失败数据:{JsonConvert.SerializeObject(article)}");
            return null;
        }


       
    }
}
