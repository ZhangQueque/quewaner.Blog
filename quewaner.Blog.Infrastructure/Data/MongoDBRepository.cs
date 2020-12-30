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
            //配置文件集合的名称（已不使用）
            //collection = db.GetCollection<T>(mongoDatabaseSettings.ArticleCollectionName);
            //根据 T 实体名来命名集合名称
            collection = db.GetCollection<T>(typeof(T).Name);
        } 

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="t">添加对象</param>
        /// <returns></returns>
        public async Task<bool> AddAsync(T t)
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
        /// 整个对象更新
        /// </summary>
        /// <param name="id">要更新实体的主键</param>
        /// <param name="t">更新数据</param>
        /// <returns></returns>
        public async Task<bool> ReplaceAsync( T t)
        {
            var replaceOneResult = await collection.ReplaceOneAsync(m => m.Id == t.Id, t);
            return replaceOneResult.ModifiedCount > 0;
        }

        /// <summary>
        /// 单个字段更新
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="field">更新的字段</param>
        /// <param name="value">更新的值</param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(string id, string field,string value)
        {
            var update = Builders<T>.Update.Set(field, value); //设置要更新的值
            var  updateResult =await collection.UpdateOneAsync(m=>m.Id==id, update);
            return updateResult.ModifiedCount > 0;
        }


        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="expression">条件</param>
        /// <param name="page">页索引</param>
        /// <param name="size">每页显示数量</param>
        /// <param name="sortDefinition">排序</param>
        /// <returns></returns>
        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> expression, int page = 1, int size = 10, SortDefinition<T> sortDefinition = null)
        {
            var filterDefinition = Builders<T>.Filter.Where(expression); //条件筛选
            FindOptions<T> findOptions = new FindOptions<T>(); //设置分页排序
            if (sortDefinition != null)
                findOptions.Sort = sortDefinition;
            findOptions.Skip = (page - 1) * size;
            findOptions.Limit = size;  
            return (await collection.FindAsync(filterDefinition, findOptions)).ToList();
        }

        /// <summary>
        /// 获取总数量
        /// </summary>
        /// <returns></returns>
        public async Task<int> GetCountAsync()
        {
            return (int)(await collection.CountDocumentsAsync(m => true));
        }


        /// <summary>
        /// 获取符合条件的总数量
        /// </summary>
        /// <returns></returns>
        public async Task<int> GetCountAsync(Expression<Func<T, bool>> expression)
        {
            return (int)(await collection.CountDocumentsAsync(expression));
        }

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public async Task<bool> IsExistsAsync(string id)
        {
            return (await GetByIdAsync(id)) != null;
        }

        /// <summary>
        /// 更具ID获取对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<T> GetByIdAsync(string id)
        {
            IAsyncCursor<T> obj = await collection.FindAsync<T>(m => m.Id == id);
            return await obj.FirstOrDefaultAsync();
        }
    }
}