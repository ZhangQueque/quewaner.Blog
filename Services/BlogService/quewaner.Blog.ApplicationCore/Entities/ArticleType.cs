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


        /// <summary>
        /// 当前状态1启用 ， 0 禁用
        /// </summary>
        public int Status { get; private set; } = 1;

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatTime { get; private set; } = DateTime.Now;

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; private set; } = DateTime.Now;

        /// <summary>
        /// 更改
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="icon">图标</param>
        /// <param name="summaryInfo">简介</param>
        /// <param name="status">状态</param>
        public void Update(string name,string icon  , string  summaryInfo , int status)
        {
            Name = name;
            Icon = icon;
            SummaryInfo = summaryInfo;
            Status = status;
            UpdateTime = DateTime.Now;
        }

    }
}
