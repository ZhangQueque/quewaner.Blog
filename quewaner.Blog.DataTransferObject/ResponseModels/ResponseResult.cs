using System;
using System.Collections.Generic;
using System.Text;

namespace quewaner.Blog.DataTransferObject.ResponseModel
{
   /// <summary>
   /// 响应结果
   /// </summary>
   /// <typeparam name="T"></typeparam>
   public  class ResponseResult<T>
    {
        public ResponseResult(int code ,string message="",T data =default)
        {
            Code = code;
            Message = message;
            Data = data;
        }
        public ResponseResult()
        {

        }
        /// <summary>
        /// 0 失败  1 成功
        /// </summary>
        public int Code { get; set; }
        /// <summary>
        /// 警告信息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 返回数据
        /// </summary>
        public T Data { get; set; }
    }
}
