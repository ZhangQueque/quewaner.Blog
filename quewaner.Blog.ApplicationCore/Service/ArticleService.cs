using quewaner.Blog.ApplicationCore.Entities.ArticleAggregate;
using quewaner.Blog.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace quewaner.Blog.ApplicationCore.Service
{
    public class ArticleService: IArticleService
    {
        private readonly IAsyncRepository<Article> _articRepository;
        private readonly IAsyncRepository<ArticleTag> _tagRepository;

        public ArticleService(IAsyncRepository<Article> articRepository , IAsyncRepository<ArticleTag> tagRepository)
        {
            _articRepository = articRepository;
            _tagRepository = tagRepository;
        }

        public async Task<bool> AddArticle(Article article)
        {
           
           return await  _articRepository.InsertAsync(article);
        }

        public async Task<bool> DeleteArticle(string articleId)
        {          
            return await _articRepository.DeleteAsync(articleId);
        }

        public async Task<IReadOnlyList<Article>> GetArticleAsync(int page ,int size ,string queryStr="",string tagIds="",string typeIds="")
        {
            //类型
            if (!string.IsNullOrEmpty(typeIds))
            {
                return await GetArticleByTypesAsync(page,size,queryStr,typeIds);
            }
            //标签
            else
            {
                return await GetArticleByTagsAsync(page, size, queryStr, tagIds);
            }
        }



        /// <summary>
        /// 通过标签筛选文章
        /// </summary>
        /// <param name="page">页索引</param>
        /// <param name="size">每页显示数量</param>
        /// <param name="queryStr">查询关键词</param>
        /// <param name="tagIds">标签Ids</param>
        /// <returns></returns>
        private async Task<IReadOnlyList<Article>> GetArticleByTagsAsync(int page, int size, string queryStr = "", string tagIds = "")
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// 通过类型筛选文章
        /// </summary>
        /// <param name="page">页索引</param>
        /// <param name="size">每页显示数量</param>
        /// <param name="queryStr">查询关键词</param>
        /// <param name="typeIds">类型Ids</param>
        private async Task<IReadOnlyList<Article>> GetArticleByTypesAsync(int page, int size, string queryStr = "", string typeIds = "")
        {
            throw new NotImplementedException();
        }

    }
}
