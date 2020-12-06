using quewaner.Blog.ApplicationCore.Entities.ArticleAggregate;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace quewaner.Blog.ApplicationCore.Exceptions
{
    /// <summary>
    /// 自定义文章异常类
    /// </summary>
    public class ArticleNotFoundException : Exception
    {
        public ArticleNotFoundException()
        {
        }

        public ArticleNotFoundException(string message) : base(message)
        {
        }

        public ArticleNotFoundException(string articleId,string requestUrl="",string requestData = "") : base($"报错时间：{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff")}文章没有找到，文章ID为：{articleId},请求地址为：{requestUrl},请求参数为:{requestData}")
        {

        }

        public ArticleNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ArticleNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
