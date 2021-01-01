using System;

namespace quewaner.Blog.DataTransferObject.ArticleTypeDtos
{
    /// <summary>
    /// 文章类别展示模型
    /// </summary>
    public class ShowArticleTypeDto
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string Id { get; set; }

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

        /// <summary>
        /// 当前状态1启用 ， 0 禁用
        /// </summary>
        public int Status { get; private set; } = 1;
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
