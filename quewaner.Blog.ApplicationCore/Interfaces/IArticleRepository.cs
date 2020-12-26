using quewaner.Blog.ApplicationCore.Entities.ArticleAggregate;
using quewaner.Blog.DataTransferObject.ArticleDto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace quewaner.Blog.ApplicationCore.Interfaces
{
    /// <summary>
    /// 文章服务接口
    /// </summary>
    public interface IArticleService
    {
        /// <summary>
        /// 添加文章
        /// </summary>
        /// <param name="addArticleDto">添加文章对象</param>
        /// <returns></returns>
        Task<Article> AddArticleAsync(AddArticleDto addArticleDto);
    }
}
