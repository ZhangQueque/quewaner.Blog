using Ardalis.GuardClauses;
using quewaner.Blog.ApplicationCore.Entities.ArticleAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace quewaner.Blog.ApplicationCore.Exceptions
{
    public static class GuardExtensions
    {
        public static void NullArticle(this IGuardClause guardClause,string articleId,Article article , string requestUrl = "", string requestData = "")
        {
            if (article == null)
            {
                throw new ArticleNotFoundException(articleId,requestUrl,requestData);
            }
        }
    }
}
