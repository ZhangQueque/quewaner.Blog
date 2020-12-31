using System;
using System.Collections.Generic;
using System.Text;

namespace quewaner.Blog.DataTransferObject.ArticleDto
{
    /// <summary>
    /// 更改文章模型
    /// </summary>
    public class UpdateArticleDto
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 简介
        /// </summary>
        public string SummaryInfo { get; set; }
        /// <summary>
        /// 头图
        /// </summary>
        public string Icon { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 类别ID
        /// </summary>
        public string ArticleTypeId { get; set; }

        /// <summary>
        /// 当前状态1启用 ， 0 禁用
        /// </summary>
        public int Status { get; set; }


    }
}
