using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using quewaner.Blog.ApplicationCore.Entities.ArticleAggregate;
using quewaner.Blog.ApplicationCore.Exceptions;
using quewaner.Blog.ApplicationCore.Interfaces;
using quewaner.Blog.DataTransferObject.ArticleDto;
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
        public async Task<ActionResult<ResponseResult<Article>>> AddArticleAsync([FromBody] AddArticleDto addArticleDto)
        {
            try
            {
                var addArticle = await _articleService.AddArticleAsync(addArticleDto);
                return addArticle == null ? new ResponseResult<Article>(0, "添加失败", null) : new ResponseResult<Article>(1, "添加成功", addArticle);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, ex.Message);
                return new ResponseResult<Article>(0, ex.Message, null);
            }
        }

        /// <summary>
        /// 删除博客
        /// </summary>
        /// <param name="id">博客主键</param>
        /// <returns></returns>
        [HttpGet("delete")]
        public async Task<ActionResult<ResponseResult<Article>>> DeleteArticleAsync(string id)
        {
            try
            {
                var deleteArtcle = await _articleService.DeleteArticleAsync(id);
                return deleteArtcle == null ? new ResponseResult<Article>(0, "删除失败", null) : new ResponseResult<Article>(1, "删除成功", deleteArtcle);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, ex.Message);
                return new ResponseResult<Article>(0, ex.Message, null);
            }
        }

        /// <summary>
        /// 更改文章
        /// </summary>
        /// <param name="updateArticleDto">更改对象</param>
        /// <returns></returns>
        [HttpPost("update")]
        public async Task<ActionResult<ResponseResult<Article>>> UpdateArticleAsync([FromBody] UpdateArticleDto updateArticleDto)
        {
            try
            {
                var deleteArtcle = await _articleService.UpdateArticleAsync(updateArticleDto);
                return deleteArtcle == null ? new ResponseResult<Article>(0, "删除失败", null) : new ResponseResult<Article>(1, "删除成功", deleteArtcle);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, ex.Message);
                return new ResponseResult<Article>(0, ex.Message, null);
            }
        }

        /// <summary>
        /// 获取博客
        /// </summary>
        /// <param name="pageParameters">查询参数</param>
        /// <returns></returns>
        [HttpGet("get")]
        public async Task<ActionResult<ResponseResult<PageList<Article>>>> GetArticleAsync([FromQuery] PageParameters pageParameters)
        {
            try
            {
                var pageList = await _articleService.GetArticlesAsync(pageParameters);
                return pageList == null ? new ResponseResult<PageList<Article>>(0, "查询失败", null) : new ResponseResult<PageList<Article>>(1, "查询成功", pageList);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, ex.Message);
                return new ResponseResult<PageList<Article>>(0, ex.Message, null);
            }
        }

        /// <summary>
        /// 获取Layui表格数据
        /// </summary>
        /// <param name="pageParameters"></param>
        /// <returns></returns>
        [HttpGet("layui/table")]
        public async Task<ActionResult<LayuiTableModel<List<Article>>>> GetArticleLayuiTypeAsync([FromQuery] PageParameters pageParameters)
        {

            var pageList = await _articleService.GetArticlesAsync(pageParameters);
            return new LayuiTableModel<List<Article>>(0, "", pageList.Items.ToList(), pageList.Count);

        }

    }
}
