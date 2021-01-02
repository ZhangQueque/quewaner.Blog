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
using quewaner.Blog.DataTransferObject.Pages;
using MongoDB.Driver;
using quewaner.Blog.DataTransferObject.ArticleDtos;

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

        public ArticleService(ILogger<ArticleService> logger, IAsyncRepository<Article> articleRepository, IAsyncRepository<ArticleType> articleTypeRepository, IMapper mapper)
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
        public async Task<ShowArticleDto> AddArticleAsync(AddArticleDto addArticleDto)
        {
            ArticleType articleType = await _articleTypeRepository.GetByIdAsync(addArticleDto.ArticleTypeId);
            Guard.Against.NullArticleType(addArticleDto.ArticleTypeId, articleType);
            var article = _mapper.Map<Article>(addArticleDto);
            article.SetArticleType(articleType);
            //返回展示模型
            if (await _articleRepository.AddAsync(article))
            {
                var showArticle = _mapper.Map<ShowArticleDto>(article);
                return showArticle;
            }
            _logger.LogError($"文章添加失败。失败数据:{JsonConvert.SerializeObject(article)}");
            return null;
        }

        /// <summary>
        /// 删除文章
        /// </summary>
        /// <param name="id">博客ID</param>
        /// <returns></returns>
        public async Task<ShowArticleDto> DeleteArticleAsync(string id)
        {
            var article = await _articleRepository.GetByIdAsync(id);
            if (article != null)

            {  //返回展示模型
                var showArticle = _mapper.Map<ShowArticleDto>(article);
                if (await _articleRepository.DeleteAsync(id))
                {
                    return showArticle;
                }
            }
            _logger.LogError($"文章删除失败。主键:{id},失败数据:{JsonConvert.SerializeObject(article)}");
            return null;
        }

        /// <summary>
        /// 更改文章信息
        /// </summary>
        /// <param name="updateArticleDto">更改文章模型</param>
        /// <returns></returns>
        public async Task<ShowArticleDto> UpdateArticleAsync(UpdateArticleDto updateArticleDto)
        {
            ArticleType articleType = await _articleTypeRepository.GetByIdAsync(updateArticleDto.ArticleTypeId);
            Guard.Against.NullArticleType(updateArticleDto.ArticleTypeId, articleType);

            Article article = await _articleRepository.GetByIdAsync(updateArticleDto.Id);
            Guard.Against.NullArticle(updateArticleDto.Id, article);

            article.Update(updateArticleDto.Title, updateArticleDto.SummaryInfo, updateArticleDto.Icon, updateArticleDto.Content, updateArticleDto.Status, articleType);

            if (await _articleRepository.ReplaceAsync(article))
            {
                //返回展示模型
                var showArticle = _mapper.Map<ShowArticleDto>(article);
                return showArticle;
            }
            _logger.LogError($"文章更新失败。失败数据:{JsonConvert.SerializeObject(article)}");
            return null;
        }



        /// <summary>
        /// 获取文章分页数据
        /// </summary>
        /// <param name="pageParameters">分页查询参数</param>
        /// <returns></returns>
        public async Task<PageList<ShowArticleDto>> GetArticlesAsync(PageParameters pageParameters)
        {
            IReadOnlyList<Article> list = null;

            int count = 0;
            //在这里判断文章类型是否为空，而不是在lamada中判断，是因为mongodb会去根据这个ID查询，但是发现这个ID并不是一个有效的24位十六进制字符串  ，会报错
            if (!string.IsNullOrEmpty(pageParameters.ArticleTypeId))
            {
                list = await _articleRepository.GetAsync(
                 m => (string.IsNullOrEmpty(pageParameters.Keyword)
                || m.Title.Contains(pageParameters.Keyword)
                || m.SummaryInfo.Contains(pageParameters.Keyword))
                && (m.ArticleType.Id == pageParameters.ArticleTypeId), pageParameters.Page, pageParameters.Limit, Builders<Article>.Sort.Descending(sort => sort.CreatTime));

                count = await _articleRepository.GetCountAsync(
                 m => (string.IsNullOrEmpty(pageParameters.Keyword)
                || m.Title.Contains(pageParameters.Keyword)
                || m.SummaryInfo.Contains(pageParameters.Keyword))
                && (m.ArticleType.Id == pageParameters.ArticleTypeId)
                 );
            }
            else
            {
                list = await _articleRepository.GetAsync(
                 m => (string.IsNullOrEmpty(pageParameters.Keyword)
                || m.Title.Contains(pageParameters.Keyword)
                || m.SummaryInfo.Contains(pageParameters.Keyword))

              , pageParameters.Page, pageParameters.Limit, Builders<Article>.Sort.Descending(sort => sort.CreatTime));

                count = await _articleRepository.GetCountAsync(
                 m => (string.IsNullOrEmpty(pageParameters.Keyword)
                || m.Title.Contains(pageParameters.Keyword)
                || m.SummaryInfo.Contains(pageParameters.Keyword))

                 );
            }

            var showArticles = _mapper.Map<IReadOnlyList<ShowArticleDto>>(list);
            if (pageParameters.Status!=3)
            {
                showArticles = showArticles.Where(m => m.Status == pageParameters.Status).ToList();
            }
            return new PageList<ShowArticleDto>(showArticles, pageParameters.Page, pageParameters.Limit, count);
        }

        /// <summary>
        /// 根据ID获取文章
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public async Task<ShowArticleDto> GetArticleByIdAsync(string id)
        {

            Article article = await _articleRepository.GetByIdAsync(id);
            Guard.Against.NullArticle(id, article);
            //返回展示模型
            var showArticle = _mapper.Map<ShowArticleDto>(article);

            return showArticle;
        }

    }
}
