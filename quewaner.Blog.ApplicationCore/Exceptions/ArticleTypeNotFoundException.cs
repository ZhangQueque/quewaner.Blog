using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace quewaner.Blog.ApplicationCore.Exceptions
{
    public class ArticleTypeNotFoundException : Exception
    {
        public ArticleTypeNotFoundException()
        {
        }

        public ArticleTypeNotFoundException(string articleTypeId, string requestUrl = "", string requestData = "") : base($"报错时间：{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff")}文章没有找到，文章ID为：{articleTypeId},请求地址为：{requestUrl},请求参数为:{requestData}")
        {

        }

        public ArticleTypeNotFoundException(string articleTypeId, string requestUrl = "", string requestData = "", Exception exception = null) : base($"报错时间：{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff")}文章没有找到，文章ID为：{articleTypeId},请求地址为：{requestUrl},请求参数为:{requestData}", exception)
        {

        }
        public ArticleTypeNotFoundException(string message) : base(message)
        {
        }

        public ArticleTypeNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ArticleTypeNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
