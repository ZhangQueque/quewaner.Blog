using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace quewaner.Blog.ApplicationCore.Entities
{

    public abstract class BaseEntity<T>
    {
        /// <summary>
        /// 让ObjectId 以string的方式传输
        /// </summary>
        [BsonId]
        [BsonRepresentation( MongoDB.Bson.BsonType.ObjectId)]
        public virtual T Id { get; protected set; }
    }
}
