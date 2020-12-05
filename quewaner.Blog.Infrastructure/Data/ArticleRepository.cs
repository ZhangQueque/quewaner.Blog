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
    public class ArticleRepository : MongoDBRepository<Article>, IArticleRepository
    {
        public ArticleRepository(IMongoDatabaseSettings mongoDatabaseSettings) : base(mongoDatabaseSettings)
        {

        }

        public async Task<IReadOnlyList<Article>> GetArticlesByTagsAsync(int page, int size, string queryStr = "", params string[] tagIds)
        {
            SortDefinitionBuilder<Article> sortDefinitionBuilder = new SortDefinitionBuilder<Article>();
            List<Article> articles = new List<Article>();
            if (tagIds.Length>0)
            {
                foreach (var tagId in tagIds)
                {
                    articles.AddRange( await GetAsync(m => m.ArticleTags.Where(tag => tag.Id == tagId).Any() && (m.Title.Contains(queryStr) || m.SummaryInfo.Contains(queryStr)), page, size, sortDefinitionBuilder.Descending(m => m.CreatTime)));
                }
            }
            articles =( await GetAsync(m => m.Title.Contains(queryStr) || m.SummaryInfo.Contains(queryStr) , page, size, sortDefinitionBuilder.Descending(m => m.CreatTime)) ).ToList();
            return articles.Distinct().ToList();
            //IEqualityComparer
        }

        public Task<IReadOnlyList<Article>> GetArticlesByTagsAsync(int page, int size, string queryStr = "", string tagId = "")
        {
             throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Article>> GetArticlesByTypesAsync(int page, int size, string queryStr = "", params string[] typeId)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Article>> GetArticlesByTypesAsync(int page, int size, string queryStr = "", string typeId = "")
        {
            throw new NotImplementedException();
        }
    }
}
