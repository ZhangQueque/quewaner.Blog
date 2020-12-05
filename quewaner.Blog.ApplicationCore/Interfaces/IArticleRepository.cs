using quewaner.Blog.ApplicationCore.Entities.ArticleAggregate;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace quewaner.Blog.ApplicationCore.Interfaces
{
    public interface IArticleRepository : IAsyncRepository<Article>
    {
        Task<List<Article>> GetArticlesByTypesAsync(int page, int size, string queryStr = "", params string[] typeId);

        Task<List<Article>> GetArticlesByTypesAsync(int page, int size, string queryStr = "", string typeId = "");

        Task<List<Article>> GetArticlesByTagsAsync(int page, int size, string queryStr = "", params string[] tagId);
        Task<List<Article>> GetArticlesByTagsAsync(int page, int size, string queryStr = "", string tagId = "");

    }
}
