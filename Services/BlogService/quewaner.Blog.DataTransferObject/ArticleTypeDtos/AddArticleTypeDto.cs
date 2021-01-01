namespace quewaner.Blog.DataTransferObject.ArticleTypeDtos
{
    /// <summary>
    /// 添加文章类型模型
    /// </summary>
    public class AddArticleTypeDto
    {
        /// <summary>
        /// 类别名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }
        /// <summary>
        /// 摘要信息
        /// </summary>
        public string SummaryInfo { get; set; }
    }
}
