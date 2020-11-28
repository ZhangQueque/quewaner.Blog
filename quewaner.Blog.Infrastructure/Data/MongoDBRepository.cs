using MongoDB.Bson;
using MongoDB.Driver;
using quewaner.Blog.ApplicationCore.Entities;
using quewaner.Blog.ApplicationCore.Interfaces;
using quewaner.Blog.Infrastructure.Config;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace quewaner.Blog.Infrastructure.Data
{
    public class MongoDBRepository<T> where T:BaseEntity<ObjectId>, IAggregateRoot
    {
        protected IMongoDatabase db;
        protected IMongoCollection<T> collection;
        public MongoDBRepository(IMongoDatabaseSettings mongoDatabaseSettings)
        {
           var  mongoClient = new MongoClient(mongoDatabaseSettings.ConnectionString);
           db  =  mongoClient.GetDatabase(mongoDatabaseSettings.DatabaseName);
           collection =   db.GetCollection<T>(mongoDatabaseSettings.ArticleCollectionName);
        }

        public MongoDBRepository(IMongoDatabaseSettings mongoDatabaseSettings, string collectionName)
        {
            var mongoClient = new MongoClient(mongoDatabaseSettings.ConnectionString);
            db = mongoClient.GetDatabase(mongoDatabaseSettings.DatabaseName);
            collection = db.GetCollection<T>(collectionName);
        }

        /// <summary>
        /// 添加进集合
        /// </summary>
        /// <param name="t">添加对象类型</param>
        /// <returns></returns>
        public async Task<bool> InsertFromCollection(T t )
        {
            await collection.InsertOneAsync(t);
            return await IsExists(t.Id);
        }


        public async Task<bool> DeleteFromCollection(ObjectId objectId)
        {
            var deleteResult= await collection.DeleteOneAsync(m=>m.Id == objectId);
            return deleteResult.DeletedCount>0;
        }


        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        public async Task<bool> IsExists(ObjectId id)
        {
            IAsyncCursor<T> obj = await collection.FindAsync<T>(m => m.Id == id);
            return (await obj.FirstOrDefaultAsync()) != null;
        }
    }
}
