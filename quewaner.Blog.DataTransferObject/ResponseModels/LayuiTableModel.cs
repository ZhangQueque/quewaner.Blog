using System;
using System.Collections.Generic;
using System.Text;

namespace quewaner.Blog.DataTransferObject.ResponseModels
{
    /// <summary>
    /// Layui表格模型
    /// </summary>
    public class LayuiTableModel<T>
    {

        public LayuiTableModel(int code,string msg="",T data=default, int count=0) 
        {
            Code = code;
            Msg = msg;
            Data = data;
            Count = count;
        }
        /// <summary>
        /// 0成功 1失败 
        /// </summary>
        public int Code { get; set; }

        public string Msg { get; set; }

        public T Data { get; set; }

        public int Count { get; set; }
    }
}
