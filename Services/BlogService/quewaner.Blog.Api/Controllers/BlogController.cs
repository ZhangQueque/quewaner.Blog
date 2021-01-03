using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using quewaner.Blog.ApplicationCore.Entities.ArticleAggregate;
using quewaner.Blog.ApplicationCore.Exceptions;
using quewaner.Blog.ApplicationCore.Interfaces;
using quewaner.Blog.DataTransferObject.ArticleDto;
using quewaner.Blog.DataTransferObject.ArticleDtos;
using quewaner.Blog.DataTransferObject.Pages;
using quewaner.Blog.DataTransferObject.ResponseModel;
using quewaner.Blog.DataTransferObject.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace quewaner.Blog.Api.Controllers
{
    /// <summary>
    /// 博客
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly ILogger<BlogController> _logger;
        private readonly IArticleService _articleService;
        public BlogController(ILogger<BlogController> logger, IArticleService articleService)
        {
            _logger = logger;
            _articleService = articleService;
        }


        /// <summary>
        /// 添加博客
        /// </summary>
        /// <param name="addArticleDto">添加对象</param>
        /// <returns></returns>
        [HttpPost("add")]
        public async Task<ActionResult<ResponseResult<ShowArticleDto>>> AddArticleAsync([FromBody] AddArticleDto addArticleDto)
        {
            var a = DateTime.Now;

            try
            {
                var addArticle = await _articleService.AddArticleAsync(addArticleDto);
                return new ResponseResult<ShowArticleDto>(1, "添加成功", addArticle);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, ex.Message);
                return new ResponseResult<ShowArticleDto>(0, ex.Message, null);
            }
        }

        /// <summary>
        /// 删除博客
        /// </summary>
        /// <param name="id">博客主键</param>
        /// <returns></returns>
        [HttpGet("delete")]
        public async Task<ActionResult<ResponseResult<ShowArticleDto>>> DeleteArticleAsync(string id)
        {
            try
            {
                var deleteArtcle = await _articleService.DeleteArticleAsync(id);
                return new ResponseResult<ShowArticleDto>(1, "删除成功", deleteArtcle);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, ex.Message);
                return new ResponseResult<ShowArticleDto>(0, ex.Message, null);
            }
        }

        /// <summary>
        /// 更改文章
        /// </summary>
        /// <param name="updateArticleDto">更改对象</param>
        /// <returns></returns>
        [HttpPost("update")]
        public async Task<ActionResult<ResponseResult<ShowArticleDto>>> UpdateArticleAsync([FromBody] UpdateArticleDto updateArticleDto)
        {
            try
            {
                var deleteArtcle = await _articleService.UpdateArticleAsync(updateArticleDto);
                return new ResponseResult<ShowArticleDto>(1, "更改成功", deleteArtcle);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, ex.Message);
                return new ResponseResult<ShowArticleDto>(0, ex.Message, null);
            }
        }

        /// <summary>
        /// 获取博客
        /// </summary>
        /// <param name="pageParameters">查询参数</param>
        /// <returns></returns>
        [HttpGet("get")]
        public async Task<ActionResult<ResponseResult<PageList<ShowArticleDto>>>> GetArticleAsync([FromQuery] PageParameters pageParameters)
        {
            try
            {
                var pageList = await _articleService.GetArticlesAsync(pageParameters);
                return  new ResponseResult<PageList<ShowArticleDto>>(1, "查询成功", pageList);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, ex.Message);
                return new ResponseResult<PageList<ShowArticleDto>>(0, ex.Message, null);
            }
        }

        /// <summary>
        /// 获取Layui表格数据
        /// </summary>
        /// <param name="pageParameters"></param>
        /// <returns></returns>
        [HttpGet("layui/table")]
        public async Task<ActionResult<LayuiTableModel<List<ShowArticleDto>>>> GetArticleLayuiTypeAsync([FromQuery] PageParameters pageParameters)
        {

            var pageList = await _articleService.GetArticlesAsync(pageParameters);
            return new LayuiTableModel<List<ShowArticleDto>>(0, "", pageList.Items.ToList(), pageList.Count);

        }

        /// <summary>
        /// 根据ID获取文章
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        [HttpGet("one/{id}")]
        public async Task<ActionResult<ResponseResult<ShowArticleDto>>> GetArticleByIdAsync(string id)
        {
            try
            {
                var article = await _articleService.GetArticleByIdAsync(id);
                return new ResponseResult<ShowArticleDto>(1, $"{article.Title}查询成功", article);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, ex.Message);
                return new ResponseResult<ShowArticleDto>(0, ex.Message, null);
            }
        }

    }
}
