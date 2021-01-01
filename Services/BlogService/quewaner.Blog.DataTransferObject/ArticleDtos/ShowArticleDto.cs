using quewaner.Blog.DataTransferObject.ArticleTypeDtos;
using System;

namespace quewaner.Blog.DataTransferObject.ArticleDtos
{
    /// <summary>
    /// 文章展示模型
    /// </summary>
    public class ShowArticleDto
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string Id { get; set; }


        /// <summary>
        /// 自定义主键编码
        /// </summary>
        public string Code { get; private set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        /// 摘要
        /// </summary>
        public string SummaryInfo { get; private set; }

        /// <summary>
        /// 头图
        /// </summary>
        public string Icon { get; private set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; private set; }

        /// <summary>
        /// 阅读数量
        /// </summary>
        public int ReadNumber { get; private set; } = 0;

        /// <summary>
        /// 点赞数量
        /// </summary>
        public int PollNumber { get; private set; }

        /// <summary>
        /// 当前状态1启用 ， 0 禁用
        /// </summary>
        public int Status { get; private set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatTime { get; private set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; private set; }

        /// <summary>
        /// 文章类别
        /// </summary>
        public ShowArticleTypeDto ArticleType { get; private set; }
    }
}
