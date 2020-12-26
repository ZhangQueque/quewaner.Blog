using MongoDB.Driver;
using quewaner.Blog.ApplicationCore.Entities;
using quewaner.Blog.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace quewaner.Blog.ApplicationCore.Interfaces
{
    /// <summary>
    /// 仓储接口
    /// </summary>
    /// <typeparam name="T">泛型</typeparam>
    public interface IAsyncRepository<T> where T : BaseEntity<string>, IAggregateRoot
    {
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        Task<bool> DeleteAsync(string id);
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="expression">条件</param>
        /// <param name="page">页索引</param>
        /// <param name="size">每页显示数量</param>
        /// <param name="sortDefinition">排序</param>
        /// <returns></returns>
        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> expression, int page = 1, int size = 10, SortDefinition<T> sortDefinition = null);
        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        Task<T> GetByIdAsync(string id);
        /// <summary>
        ///  /// <summary>
        /// 获取总数量
        /// </summary>
        /// <returns></returns>
        /// </summary>
        /// <returns></returns>
        Task<int> GetCountAsync();
        /// <summary>
        /// 获取符合条件的总数量
        /// </summary>
        /// <returns></returns>
        Task<int> GetCountAsync(Expression<Func<T, bool>> expression);
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="t">添加对象</param>
        /// <returns></returns>
        Task<bool> AddAsync(T t);
        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        Task<bool> IsExistsAsync(string id);
        /// <summary>
        /// 整个对象更新
        /// </summary>
        /// <param name="id">要更新实体的主键</param>
        /// <param name="t">更新数据</param>
        /// <returns></returns>
        Task<bool> ReplaceAsync(string id, T t);
        /// <summary>
        /// 单个字段更新
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="field">更新的字段</param>
        /// <param name="value">更新的值</param>
        /// <returns></returns>
        Task<bool> UpdateAsync(string id, string field, string value);
    }
}