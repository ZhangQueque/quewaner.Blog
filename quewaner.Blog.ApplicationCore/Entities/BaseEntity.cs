using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace quewaner.Blog.ApplicationCore.Entities
{

    public abstract class BaseEntity<T>
    {
        [BsonId]
        [BsonRepresentation( MongoDB.Bson.BsonType.ObjectId)]
        public virtual T Id { get; protected set; }
    }
}
