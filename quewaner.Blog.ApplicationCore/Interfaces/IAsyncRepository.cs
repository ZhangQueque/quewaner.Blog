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
        
        Task<bool> DeleteAsync(string id);
        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> expression, int page = 1, int size = 10, SortDefinition<T> sortDefinition = null);
        Task<T> GetByIdAsync(string id);
        Task<int> GetCountAsync();
        Task<int> GetCountAsync(Expression<Func<T, bool>> expression);
        Task<bool> InsertAsync(T t);
        Task<bool> IsExistsAsync(string id);
        Task<bool> UpdateAsync(string id, T t);
    }
}