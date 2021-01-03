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
        public string Code { get;  set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get;  set; }

        /// <summary>
        /// 摘要
        /// </summary>
        public string SummaryInfo { get;  set; }

        /// <summary>
        /// 头图
        /// </summary>
        public string Icon { get;  set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get;  set; }

        /// <summary>
        /// 阅读数量
        /// </summary>
        public int ReadNumber { get;  set; } = 0;

        /// <summary>
        /// 点赞数量
        /// </summary>
        public int PollNumber { get;  set; }

        /// <summary>
        /// 当前状态1启用 ， 0 禁用
        /// </summary>
        public int Status { get;  set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatTime { get;  set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get;  set; }

        /// <summary>
        /// 文章类别
        /// </summary>
        public ShowArticleTypeDto ArticleType { get;  set; }
    }
}
