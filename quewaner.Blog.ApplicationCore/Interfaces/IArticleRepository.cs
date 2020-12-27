using quewaner.Blog.ApplicationCore.Entities.ArticleAggregate;
using quewaner.Blog.DataTransferObject.ArticleDto;
using quewaner.Blog.DataTransferObject.Pages;
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
        /// <summary>
        /// 删除文章
        /// </summary>
        /// <param name="id">博客ID</param>
        /// <returns></returns>
        Task<Article> DeleteArticleAsync(string id);
        /// <summary>
        /// 获取文章分页数据
        /// </summary>
        /// <param name="pageParameters">分页查询参数</param>
        /// <returns></returns>
        Task<PageList<Article>> GetArticlesAsync(PageParameters pageParameters);
        /// <summary>
        /// 更改文章信息
        /// </summary>
        /// <param name="updateArticleDto">更改文章模型</param>
        /// <returns></returns>
        Task<Article> UpdateArticleAsync(UpdateArticleDto updateArticleDto);
    }
}
