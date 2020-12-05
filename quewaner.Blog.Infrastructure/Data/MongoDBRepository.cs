using MongoDB.Driver;
using quewaner.Blog.ApplicationCore.Entities;
using quewaner.Blog.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace quewaner.Blog.Infrastructure.Data
{
    /// <summary>
    /// MongoDB仓储模型
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MongoDBRepository<T> : IAsyncRepository<T> where T : BaseEntity<string>, IAggregateRoot
    {
        protected IMongoDatabase db;
        protected IMongoCollection<T> collection;

        public MongoDBRepository(IMongoDatabaseSettings mongoDatabaseSettings)
        {
            var mongoClient = new MongoClient(mongoDatabaseSettings.ConnectionString);
            db = mongoClient.GetDatabase(mongoDatabaseSettings.DatabaseName);
            collection = db.GetCollection<T>(mongoDatabaseSettings.ArticleCollectionName);
        }

        public MongoDBRepository(IMongoDatabaseSettings mongoDatabaseSettings, string collectionName)
        {
            var mongoClient = new MongoClient(mongoDatabaseSettings.ConnectionString);
            db = mongoClient.GetDatabase(mongoDatabaseSettings.DatabaseName);
            collection = db.GetCollection<T>(collectionName);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="t">添加对象</param>
        /// <returns></returns>
        public async Task<bool> InsertAsync(T t)
        {
            await collection.InsertOneAsync(t);
            return await IsExistsAsync(t.Id);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(string id)
        {
            var deleteResult = await collection.DeleteOneAsync(m => m.Id == id);
            return deleteResult.DeletedCount > 0;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="id">要更新实体的主键</param>
        /// <param name="t">更新数据</param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(string id, T t)
        {
            var replaceOneResult = await collection.ReplaceOneAsync(m => m.Id == id, t);
            return replaceOneResult.ModifiedCount > 0;
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="expression">条件</param>
        /// <param name="page">页索引</param>
        /// <param name="size">每页显示数量</param>
        /// <param name="sortDefinition">排序</param>
        /// <returns></returns>
        public async Task<List<T>> GetAsync(Expression<Func<T, bool>> expression,int page = 1,int size =10, SortDefinition<T> sortDefinition = null)
        {
            var filterDefinition = Builders<T>.Filter.Where(expression);
            FindOptions<T> findOptions = new FindOptions<T>();
            if (sortDefinition!=null)
                findOptions.Sort = sortDefinition;
            findOptions.Skip = (page-1)*size;
            findOptions.Limit = size;
            return (await collection.FindAsync(filterDefinition)).ToList();
        }

        /// <summary>
        /// 获取总数量
        /// </summary>
        /// <returns></returns>
        public async Task<int> GetCountAsync()
        {
            return (int) (await collection.CountDocumentsAsync(m=>true));
        }

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public async Task<bool> IsExistsAsync(string id)
        {
            IAsyncCursor<T> obj = await collection.FindAsync<T>(m => m.Id == id);
            return (await obj.FirstOrDefaultAsync()) != null;
        }
    }
}