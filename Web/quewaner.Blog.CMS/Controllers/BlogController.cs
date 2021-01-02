using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using quewaner.Blog.CMS.Extensions.Configs;
using quewaner.Blog.CMS.Extensions.HttpClients;
using quewaner.Blog.DataTransferObject.ArticleTypeDtos;
using quewaner.Blog.DataTransferObject.Pages;
using quewaner.Blog.DataTransferObject.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace quewaner.Blog.CMS.Controllers
{
    public class BlogController : Controller
    {
        private readonly ILogger<BlogController> _logger;
        private readonly IStaticConfig _staticConfig;
        private readonly HttpClient _httpClient;
        public BlogController(ILogger<BlogController> logger, IHttpClientFactory httpClientFactory,IStaticConfig staticConfig)
        {
            _logger = logger;
            this._staticConfig = staticConfig;
            // _httpClient = httpClientFactory.CreateClient("name");//工厂创建   services.AddHttpClient("",options=>options.BaseAddress = new Uri(Configuration["StaticConfigs:BlogApiUrl"]));
            _httpClient = httpClientFactory.CreateClient();
         }
        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> Add()
        {
            ResponseResult <PageList<ShowArticleTypeDto>> responseResult = await _httpClient.GetMapperDataAsync<ResponseResult<PageList<ShowArticleTypeDto>>>(_staticConfig.GenerateGetArticleTypeApiUrl());
            if (responseResult.Code ==1)
            {
                ViewData["ArticleTypeList"] = responseResult.Data.Items.ToList();

            }
            else
            {
                ViewData["ArticleTypeList"] = new List<ShowArticleTypeDto>();
            }
            return View();
        }
    }
}
