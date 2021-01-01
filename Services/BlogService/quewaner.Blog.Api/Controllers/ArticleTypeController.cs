using Ardalis.GuardClauses;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using quewaner.Blog.ApplicationCore.Entities.ArticleAggregate;
using quewaner.Blog.ApplicationCore.Exceptions;
using quewaner.Blog.ApplicationCore.Interfaces;
using quewaner.Blog.DataTransferObject.ArticleDto;
using quewaner.Blog.DataTransferObject.ArticleTypeDtos;
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
    /// 文章分类
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleTypeController : ControllerBase
    {
        private readonly ILogger<ArticleTypeController> _logger;
        private readonly IAsyncRepository<ArticleType> _articleTypeRepository;
        private readonly IMapper _mapper;

        public ArticleTypeController(ILogger<ArticleTypeController> logger, IAsyncRepository<ArticleType> articleTypeRepository, IMapper mapper)
        {
            _logger = logger;
            _articleTypeRepository = articleTypeRepository;
            _mapper = mapper;
        }


        /// <summary>
        /// 添加文章分类
        /// </summary>
        /// <param name="addArticleTypeDto">添加对象</param>
        /// <returns></returns>
        [HttpPost("add")]
        public async Task<ActionResult<ResponseResult<ShowArticleTypeDto>>> AddArticleTypeAsync([FromBody] AddArticleTypeDto addArticleTypeDto)
        {
            try
            {
                ArticleType articleType = _mapper.Map<ArticleType>(addArticleTypeDto);
                if (await _articleTypeRepository.AddAsync(articleType))
                {
                    var showArticleType  = _mapper.Map<ShowArticleTypeDto>(articleType);
                    return new ResponseResult<ShowArticleTypeDto>(1, "添加成功", showArticleType);
                }
                return new ResponseResult<ShowArticleTypeDto>(0, "添加失败", null);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, ex.Message);
                return new ResponseResult<ShowArticleTypeDto>(0, ex.Message, null);
            }
        }

        /// <summary>
        /// 删除文章分类
        /// </summary>
        /// <param name="id">博客主键</param>
        /// <returns></returns>
        [HttpGet("delete")]
        public async Task<ActionResult<ResponseResult<ShowArticleTypeDto>>> DeleteArticleTypeAsync(string id)
        {
            try
            {
                ArticleType articleType = await _articleTypeRepository.GetByIdAsync(id);
                if (articleType != null)
                {
                    if (await _articleTypeRepository.DeleteAsync(id))
                    {
                        var showArticleType = _mapper.Map<ShowArticleTypeDto>(articleType);

                        return new ResponseResult<ShowArticleTypeDto>(1, "删除成功", showArticleType);

                    }
                }

                return new ResponseResult<ShowArticleTypeDto>(0, "删除失败", null);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, ex.Message);
                return new ResponseResult<ShowArticleTypeDto>(0, ex.Message, null);
            }
        }


        /// <summary>
        /// 更改文章类别
        /// </summary>
        /// <param name="updateArticle"></param>
        /// <returns></returns>
        [HttpPost("update")]
        public async Task<ActionResult<ResponseResult<ShowArticleTypeDto>>> UpdateArticleTypeAsync([FromBody] UpdateArticleTypeDto updateArticle)
        {
            try
            {
                ArticleType articleType = await _articleTypeRepository.GetByIdAsync(updateArticle.Id);
                Guard.Against.NullArticleType(updateArticle.Id, articleType);

                articleType.Update(updateArticle.Name, updateArticle.Icon, updateArticle.SummaryInfo, updateArticle.Status);
                if (await _articleTypeRepository.ReplaceAsync(articleType))
                {
                    var showArticleType = _mapper.Map<ShowArticleTypeDto>(articleType);
                    return new ResponseResult<ShowArticleTypeDto>(1, "更改成功", showArticleType);
                }
                return new ResponseResult<ShowArticleTypeDto>(0, "更改失败", null);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, ex.Message);
                return new ResponseResult<ShowArticleTypeDto>(0, ex.Message, null);
            }
        }

        /// <summary>
        /// 获取文章类别
        /// </summary>
        /// <param name="pageParameters">查询参数</param>
        /// <returns></returns>
        [HttpGet("get")]
        public async Task<ActionResult<ResponseResult<PageList<ShowArticleTypeDto>>>> GetArticleTypeAsync([FromQuery] PageParameters pageParameters)
        {
            try
            {
                var list = await _articleTypeRepository.GetAsync(m=>true,pageParameters.Page,pageParameters.Limit);
                var count = await _articleTypeRepository.GetCountAsync();
                var showList = _mapper.Map<List<ShowArticleTypeDto>>(list);
                return new ResponseResult<PageList<ShowArticleTypeDto>>(1, "查询成功", new PageList<ShowArticleTypeDto>(showList, pageParameters.Page,pageParameters.Limit,count));
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, ex.Message);
                return new ResponseResult<PageList<ShowArticleTypeDto>>(0, ex.Message, null);
            }
        }



        /// <summary>
        /// 获取Layui表格数据
        /// </summary>
        /// <param name="pageParameters">查询参数</param>
        /// <returns></returns>
        [HttpGet("layui/table")]
        public async Task<ActionResult<LayuiTableModel<List<ShowArticleTypeDto>>>> GetArticleTypeLayuiTableAsync([FromQuery] PageParameters pageParameters)
        {

            var list = await _articleTypeRepository.GetAsync(m => true, pageParameters.Page, pageParameters.Limit);
            int count = await _articleTypeRepository.GetCountAsync();

            var showList = _mapper.Map<List<ShowArticleTypeDto>>(list);
            return new LayuiTableModel<List<ShowArticleTypeDto>>(0, "", showList, count);
        }


        /// <summary>
        /// 根据id获取文章类别
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("id")]
        public async Task<ActionResult<ResponseResult<ShowArticleTypeDto>>> GetArticleTypeByIdAsync(string id)
        {
            try
            {
                var articleType = await _articleTypeRepository.GetByIdAsync(id);
                Guard.Against.NullArticleType(articleType.Id, articleType);
                var showArticleType = _mapper.Map<ShowArticleTypeDto>(articleType);

                return new ResponseResult<ShowArticleTypeDto>(1, $"{showArticleType.Name}查询成功", showArticleType);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, ex.Message);
                return new ResponseResult<ShowArticleTypeDto>(0, ex.Message, null);
            }
        }

    }
}
