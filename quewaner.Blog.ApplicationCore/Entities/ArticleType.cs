using quewaner.Blog.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace quewaner.Blog.ApplicationCore.Entities.ArticleAggregate
{
    /// <summary>
    /// 文章类别
    /// </summary>
    public class ArticleType:BaseEntity<string>, IAggregateRoot
    {
        public ArticleType()
        {

        }
        public ArticleType(string name,string icon,string summaryInfo)
        {
            this.Name = name;
            this.Icon = icon;
            this.SummaryInfo = summaryInfo;
        }
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
