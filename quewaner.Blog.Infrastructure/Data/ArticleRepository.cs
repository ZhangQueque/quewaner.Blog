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

        public async Task<IReadOnlyList<Article>> GetArticlesByTagsAsync(int page, int size, string queryStr = "", string tagId = "")
        {
            SortDefinitionBuilder<Article> sortDefinitionBuilder = new SortDefinitionBuilder<Article>();
            if (!string.IsNullOrEmpty(tagId))
            {
                return (await GetAsync(m => m.ArticleTags.Where(tag => tag.Id == tagId).Any() && (m.Title.Contains(queryStr) || m.SummaryInfo.Contains(queryStr)), page, size, sortDefinitionBuilder.Descending(m => m.CreatTime))).Distinct().ToList();
            }
            return (await GetAsync(m => m.Title.Contains(queryStr) || m.SummaryInfo.Contains(queryStr), page, size, sortDefinitionBuilder.Descending(m => m.CreatTime))).Distinct().ToList();
        }

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

        public async Task<IReadOnlyList<Article>> GetArticlesByTypesAsync(int page, int size, string queryStr = "", string typeId = "")
        {
            SortDefinitionBuilder<Article> sortDefinitionBuilder = new SortDefinitionBuilder<Article>();
            if (!string.IsNullOrEmpty(typeId))
            {
                return (await GetAsync(m => m.ArticleTypes.Where(type => type.Id == typeId).Any() && (m.Title.Contains(queryStr) || m.SummaryInfo.Contains(queryStr)), page, size, sortDefinitionBuilder.Descending(m => m.CreatTime))).Distinct().ToList();
            }
            return (await GetAsync(m => m.Title.Contains(queryStr) || m.SummaryInfo.Contains(queryStr), page, size, sortDefinitionBuilder.Descending(m => m.CreatTime))).Distinct().ToList();
        }
    }
}
