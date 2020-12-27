using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quewaner.Blog.DataTransferObject.Pages
{
    /// <summary>
    /// 分页帮助类
    /// </summary>
    /// <typeparam name="T">对象</typeparam>
    public   class PageList<T>
        where T:class
    {

        public PageList(IReadOnlyList<T> items,int pageIndex,int pageSize,int count)
        {
            Items = items;
            PageIndex = pageIndex;
            PageSize = pageSize;
            Count = count;
            PageTotal = (int)Math.Ceiling((decimal)count / pageSize); //取于有的话就+1
        }

        public PageList()//序列化必须
        {

        }

        public IReadOnlyList<T> Items { get; }
        public int PageIndex { get; }
        public int PageSize { get; }
        public int Count { get; }
        public int PageTotal { get; set; }

    }
}
