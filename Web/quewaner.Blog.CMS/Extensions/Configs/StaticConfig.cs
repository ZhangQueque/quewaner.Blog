using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace quewaner.Blog.CMS.Extensions.Configs
{
    /// <summary>
    /// 基础配置信息
    /// </summary>
    public class StaticConfig : IStaticConfig
    {
        /// <summary>
        /// Api地址
        /// </summary>
        public string BlogApiUrl { get; set; }


        /// <summary>
        /// 形成添加博客的APIURL
        /// </summary>
        /// <returns></returns>
        public string GenerateAddBlogApiUrl()
        {
            return string.Format("{0}/api/Blog/add", BlogApiUrl);
        }

        /// <summary>
        /// 形成删除博客的APIURL
        /// </summary>
        /// <returns></returns>
        public string GenerateDeleteBlogApiUrl()
        {
            return string.Format("{0}/api/Blog/delete", BlogApiUrl);
        }

        /// <summary>
        /// 形成更改博客的APIURL
        /// </summary>
        /// <returns></returns>
        public string GenerateUpdateBlogApiUrl()
        {
            return string.Format("{0}/api/Blog/update", BlogApiUrl);
        }

        /// <summary>
        /// 形成获取博客的APIURL
        /// </summary>
        /// <returns></returns>
        public string GenerateGetBlogApiUrl()
        {
            return string.Format("{0}/api/Blog/get", BlogApiUrl);
        }

        /// <summary>
        /// 形成获取Layui表格数据博客的APIURL
        /// </summary>
        /// <returns></returns>
        public string GenerateGetLayuiTableBlogApiUrl()
        {
            return string.Format("{0}/api/Blog/layui/table", BlogApiUrl);
        }

        /// <summary>
        /// 形成根据ID获取博客的APIURL
        /// </summary>
        /// <returns></returns>
        public string GenerateGetByIdBlogApiUrl(string id)
        {
            return string.Format("{0}/api/Blog/{1}", BlogApiUrl,id);
        }


        /// <summary>
        /// 形成文章分类APIURL
        /// </summary>
        /// <returns></returns>
        public string GenerateGetArticleTypeApiUrl()
        {
            return string.Format("{0}/api/ArticleType/get", BlogApiUrl);
        }

    }
}
