namespace quewaner.Blog.CMS.Extensions.Configs
{

    /// <summary>
    /// 基础配置信息接口
    /// </summary>
    public interface IStaticConfig
    {   
        /// <summary>
        /// Api地址
        /// </summary>
        string BlogApiUrl { get; set; }
        /// <summary>
        /// 形成添加博客的APIURL
        /// </summary>
        /// <returns></returns>
        string GenerateAddBlogApiUrl();
        /// <summary>
        /// 形成删除博客的APIURL
        /// </summary>
        /// <returns></returns>
        string GenerateDeleteBlogApiUrl();
        /// <summary>
        /// 形成更改博客的APIURL
        /// </summary>
        /// <returns></returns>
        string GenerateGetBlogApiUrl();
        /// <summary>
        /// 形成获取博客的APIURL
        /// </summary>
        /// <returns></returns>
        string GenerateGetByIdBlogApiUrl(string id);
        /// <summary>
        /// 形成获取Layui表格数据博客的APIURL
        /// </summary>
        /// <returns></returns>
        string GenerateGetLayuiTableBlogApiUrl();
        /// <summary>
        /// 形成根据ID获取博客的APIURL
        /// </summary>
        /// <returns></returns>
        string GenerateUpdateBlogApiUrl();
    }
}