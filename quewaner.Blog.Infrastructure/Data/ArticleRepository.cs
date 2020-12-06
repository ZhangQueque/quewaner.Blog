using MongoDB.Driver;
using quewaner.Blog.ApplicationCore.Entities.ArticleAggregate;
using quewaner.Blog.ApplicationCore.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quewaner.Blog.Infrastructure.Data
{
    /// <summary>
    /// 文章仓储
    /// </summary>
    public class ArticleRepository : MongoDBRepository<Article>, IArticleRepository
    {
        public ArticleRepository(IMongoDatabaseSettings mongoDatabaseSettings) : base(mongoDatabaseSettings)
        {

        }

        /// <summary>
        /// 通过标签筛选文章
        /// </summary>
        /// <param name="page">页索引</param>
        /// <param name="size">每页显示数量</param>
        /// <param name="queryStr">查询关键词</param>
        /// <param name="tagIds">标签Ids</param>
        /// <returns></returns>
        public async Task<IReadOnlyList<Article>> GetArticlesByTagsAsync(int page, int size, string queryStr = "", params string[] tagIds)
        {
            SortDefinitionBuilder<Article> sortDefinitionBuilder = new SortDefinitionBuilder<Article>();
            if (tagIds.Length > 0)
            {
                List<Article> articles = new List<Article>();
                foreach (var tagId in tagIds)
                {
                    articles.AddRange(await GetAsync(m => m.ArticleTags.Where(tag => tag.Id == tagId).Any() && (m.Title.Contains(queryStr) || m.SummaryInfo.Contains(queryStr)), page, size, sortDefinitionBuilder.Descending(m => m.CreatTime)));
                }
                return articles.Distinct().ToList();
            }
            return (await GetAsync(m => m.Title.Contains(queryStr) || m.SummaryInfo.Contains(queryStr), page, size, sortDefinitionBuilder.Descending(m => m.CreatTime))).Distinct().ToList();
            //IEqualityComparer
        }


        /// <summary>
        /// 通过类型筛选文章
        /// </summary>
        /// <param name="page">页索引</param>
        /// <param name="size">每页显示数量</param>
        /// <param name="queryStr">查询关键词</param>
        /// <param name="typeIds">类型Ids</param>
        public async Task<IReadOnlyList<Article>> GetArticlesByTypesAsync(int page, int size, string queryStr = "", params string[] typeIds)
        {
            SortDefinitionBuilder<Article> sortDefinitionBuilder = new SortDefinitionBuilder<Article>();
            if (typeIds.Length > 0)
            {
                List<Article> articles = new List<Article>();
                foreach (var typeId in typeIds)
                {
                    articles.AddRange(await GetAsync(m => m.ArticleTypes.Where(type => type.Id == typeId).Any() && (m.Title.Contains(queryStr) || m.SummaryInfo.Contains(queryStr)), page, size, sortDefinitionBuilder.Descending(m => m.CreatTime)));
                }
                return articles.Distinct().ToList();
            }
            return (await GetAsync(m => m.Title.Contains(queryStr) || m.SummaryInfo.Contains(queryStr), page, size, sortDefinitionBuilder.Descending(m => m.CreatTime))).Distinct().ToList();
            //IEqualityComparer
        }

        /// <summary>
        /// 通过标签筛选获取文章总数量
        /// </summary>
        /// <param name="queryStr">查询关键词</param>
        /// <param name="tagIds">标签Ids</param>
        /// <returns></returns>
        public async Task<int> GetCountByTagsAsync(string queryStr = "", params string[] tagIds)
        {
            SortDefinitionBuilder<Article> sortDefinitionBuilder = new SortDefinitionBuilder<Article>();
            if (tagIds.Length > 0)
            {
                List<Article> articles = new List<Article>();
                foreach (var tagId in tagIds)
                {
                    articles.AddRange(await GetAsync(m => m.ArticleTags.Where(tag => tag.Id == tagId).Any() && (m.Title.Contains(queryStr) || m.SummaryInfo.Contains(queryStr))));
                }
                return articles.Distinct().Count();
            }
            return (await GetCountAsync(m => m.Title.Contains(queryStr) || m.SummaryInfo.Contains(queryStr)));
         }

   


        /// <summary>
        /// 通过类型筛选文章总数量
        /// </summary>
        /// <param name="page">页索引</param>
        /// <param name="size">每页显示数量</param>
        /// <param name="queryStr">查询关键词</param>
        /// <param name="typeIds">类型Ids</param>
        public async Task<int> GetArticlesByTypesAsync(string queryStr = "", params string[] typeIds)
        {
            SortDefinitionBuilder<Article> sortDefinitionBuilder = new SortDefinitionBuilder<Article>();
            if (typeIds.Length > 0)
            {
                List<Article> articles = new List<Article>();
                foreach (var typeId in typeIds)
                {
                    articles.AddRange(await GetAsync(m => m.ArticleTypes.Where(type => type.Id == typeId).Any() && (m.Title.Contains(queryStr) || m.SummaryInfo.Contains(queryStr))));
                }
                return articles.Distinct().Count();
            }
            return (await GetCountAsync(m => m.Title.Contains(queryStr) || m.SummaryInfo.Contains(queryStr)));
         }

 



    }
}
