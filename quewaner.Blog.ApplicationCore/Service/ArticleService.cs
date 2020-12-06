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

        public async Task<IReadOnlyList<Article>> GetArticleAsync(int page ,int size ,string queryStr="",)
        { 
        
        }

        public async Task<IReadOnlyList<Article>> GetArticleByTagsAsync(int page, int size, string queryStr = "",)
        {

        }
        
    }
}
