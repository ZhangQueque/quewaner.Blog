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
        public async Task<ActionResult<ResponseResult<ArticleType>>> AddArticleAsync([FromBody] AddArticleTypeDto addArticleTypeDto)
        {
            try
            {
                ArticleType articleType = _mapper.Map<ArticleType>(addArticleTypeDto);
                if (await _articleTypeRepository.AddAsync(articleType))
                {
                    return new ResponseResult<ArticleType>(1, "添加成功", articleType);
                }
                return new ResponseResult<ArticleType>(0, "添加失败", null);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, ex.Message);
                return new ResponseResult<ArticleType>(0, ex.Message, null);
            }
        }

        /// <summary>
        /// 删除文章分类
        /// </summary>
        /// <param name="id">博客主键</param>
        /// <returns></returns>
        [HttpGet("delete")]
        public async Task<ActionResult<ResponseResult<ArticleType>>> DeleteArticleAsync(string id)
        {
            try
            {
                ArticleType articleType = await _articleTypeRepository.GetByIdAsync(id);
                if (articleType != null)
                {
                    if (await _articleTypeRepository.DeleteAsync(id))
                    {
                        return new ResponseResult<ArticleType>(1, "删除成功", articleType);

                    }
                }

                return new ResponseResult<ArticleType>(0, "删除失败", null);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, ex.Message);
                return new ResponseResult<ArticleType>(0, ex.Message, null);
            }
        }



        /// <summary>
        /// 获取Layui表格数据
        /// </summary>
        /// <param name="pageParameters">查询参数</param>
        /// <returns></returns>
        [HttpGet("layui/table")]
        public async Task<ActionResult<LayuiTableModel<List<ArticleType>>>> GetArticleTypeLayuiTableAsync([FromQuery] PageParameters pageParameters)
        {

            var list = await _articleTypeRepository.GetAsync(m => true, pageParameters.Page, pageParameters.Limit);
            int count = await _articleTypeRepository.GetCountAsync();
            return new LayuiTableModel<List<ArticleType>>(0, "", list.ToList(), count);
        }

    }
}
