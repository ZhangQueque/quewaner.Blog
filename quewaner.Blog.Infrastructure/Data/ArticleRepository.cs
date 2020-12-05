using quewaner.Blog.ApplicationCore.Entities.ArticleAggregate;
using quewaner.Blog.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace quewaner.Blog.Infrastructure.Data
{
    public class ArticleRepository:MongoDBRepository<Article>,IArticleRepository
    {
        public ArticleRepository(IMongoDatabaseSettings mongoDatabaseSettings):base(mongoDatabaseSettings)
        {

        }

        public async Task<List<Article>> GetArticlesByTagsAsync(int page, int size, string queryStr = "", params string[] tagId)
        {
            var list = await GetAsync(m=>(m.Title.Contains(queryStr)|| m.SummaryInfo.Contains(queryStr) ) );
            throw new NotImplementedException();
        }

        public Task<List<Article>> GetArticlesByTagsAsync(int page, int size, string queryStr = "", string tagId = "")
        {
            throw new NotImplementedException();
        }

        public Task<List<Article>> GetArticlesByTypesAsync(int page, int size, string queryStr = "", params string[] typeId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Article>> GetArticlesByTypesAsync(int page, int size, string queryStr = "", string typeId = "")
        {
            throw new NotImplementedException();
        }
    }
}
