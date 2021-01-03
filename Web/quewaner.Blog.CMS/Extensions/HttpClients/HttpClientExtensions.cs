using AspNetCore.Http.Extensions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace quewaner.Blog.CMS.Extensions.HttpClients
{
    public static class HttpClientExtensions
    {

        /// <summary>
        /// Get请求扩展方法   json转Obj 异常处理 ，失败写入日志，返回默认对象值
        /// </summary>
        /// <typeparam name="T">转换对象</typeparam>
        /// <param name="httpResponse"></param>
        /// <returns></returns>
        public static async Task<T> GetMapperDataAsync<T>(this HttpClient httpClient, string url, ILogger<object> logger = null)
        {
            HttpResponseMessage httpResponse = await httpClient.GetAsync(url);
            if (httpResponse.IsSuccessStatusCode)
            {
                var A = await httpResponse.Content.ReadAsStringAsync();
                return await httpResponse.Content.ReadAsJsonAsync<T>();
            }
            else
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append("\r\n");
                stringBuilder.Append(httpResponse.RequestMessage.RequestUri);
                stringBuilder.Append("\r\n");
                stringBuilder.Append(httpResponse.StatusCode);
                stringBuilder.Append("\r\n");
                stringBuilder.Append(await httpResponse.Content.ReadAsStringAsync());
                logger?.LogError(stringBuilder.ToString());
                return default(T);
            }
        }



        /// <summary>
        /// Post请求扩展方法   json转Obj 异常处理 ，失败写入日志，返回默认对象值
        /// </summary>
        /// <typeparam name="T">转换对象</typeparam>
        /// <param name="httpResponse"></param>
        /// <returns></returns>
        public static async Task<T> PostMapperDataAsync<T>(this HttpClient httpClient, string url, object obj, ILogger<object> logger = null)
        {
            HttpResponseMessage httpResponse = await httpClient.PostAsJsonAsync(url, obj);
            if (httpResponse.IsSuccessStatusCode)
            {
                return await httpResponse.Content.ReadAsJsonAsync<T>();
            }
            else
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append("\r\n");
                stringBuilder.Append(httpResponse.RequestMessage.RequestUri);
                stringBuilder.Append("\r\n");
                stringBuilder.Append(httpResponse.StatusCode);
                stringBuilder.Append("\r\n");
                stringBuilder.Append(await httpResponse.Content.ReadAsStringAsync());
                logger?.LogError(stringBuilder.ToString());
                return default(T);
            }
        }



        /// <summary>
        /// 直接获取string 异常处理 ，失败写入日志，返回默认对象值
        /// </summary>
        /// <typeparam name="T">转换对象</typeparam>
        /// <param name="httpResponse"></param>
        /// <returns></returns>
        public static async Task<string> GetMapperStringAsync(this HttpClient httpClient,string url, ILogger<object> logger = null)
        {
            HttpResponseMessage httpResponse = await httpClient.GetAsync(url);
            if (httpResponse.IsSuccessStatusCode)
            {
                return await httpResponse.Content.ReadAsStringAsync();
            }
            else
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append("\r\n");
                stringBuilder.Append(httpResponse.RequestMessage.RequestUri);
                stringBuilder.Append("\r\n");
                stringBuilder.Append(httpResponse.StatusCode);
                stringBuilder.Append("\r\n");
                stringBuilder.Append(await httpResponse.Content.ReadAsStringAsync());
                logger?.LogError(stringBuilder.ToString());
                return "";
            }
        }
    }
}
