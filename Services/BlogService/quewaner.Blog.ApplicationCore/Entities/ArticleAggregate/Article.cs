using MongoDB.Bson;
using quewaner.Blog.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace quewaner.Blog.ApplicationCore.Entities.ArticleAggregate
{
    /// <summary>
    /// 文章
    /// </summary>
    public class Article : BaseEntity<string>, IAggregateRoot
    {
        public Article()
        {

        }
        public Article(string title, string summaryInfo, string icon, string content, ArticleType articleType)
        {
            this.Code = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0).ToString();
            this.Title = title;
            this.SummaryInfo = summaryInfo;
            this.Icon = icon;
            this.Content = content;
            this.ArticleType = articleType;
        }

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
        public int PollNumber { get; private set; } = 0;

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
        /// 文章类别
        /// </summary>
        public ArticleType ArticleType { get; private set; }

        /// <summary>
        /// 设置文章类型
        /// </summary>
        /// <param name="articleType"></param>
        public void SetArticleType(ArticleType articleType)
        {
            ArticleType = articleType;
        }

        /// <summary>
        /// 更改
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="summaryInfo">简介</param>
        /// <param name="icon">头图</param>
        /// <param name="content">内容</param>
        /// <param name="articleType">文章类别</param>
        /// <param name="status">文章状态</param>
        public void Update(string title, string summaryInfo, string icon, string content,int status, ArticleType articleType)
        {
            this.Title = title;
            this.SummaryInfo = summaryInfo;
            this.Icon = icon;
            this.Content = content;
            this.ArticleType = articleType;
            this.UpdateTime = DateTime.Now;
            this.Status = status;
        }

    }
}
