namespace quewaner.Blog.DataTransferObject.Pages
{
    public class PageParameters
    {

        private int page = 1;

        /// <summary>
        /// 页索引
        /// </summary>
        public int Page
        {
            get { return page; }
            set { page = value; }
        }

        private int limit = 10;

        /// <summary>
        /// 每页显示数量
        /// </summary>
        public int Limit
        {
            get { return limit; }
            set { limit = value > 300 ? 300 : value; }
        }

        /// <summary>
        /// 关键字
        /// </summary>
        public string Keyword { get; set; } = "";

        /// <summary>
        /// 文章类别
        /// </summary>
        public string ArticleTypeId { get; set; } = "";

        /// <summary>
        /// 当前状态1启用 ， 0 禁用  3为全部
        /// </summary>
        public int Status { get; set; } = 3;
    }
}
