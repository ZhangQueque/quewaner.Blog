using System;
using System.Collections.Generic;
using System.Text;

namespace quewaner.Blog.Infrastructure.Config
{
    
    public class MongoDatabaseSettings : IMongoDatabaseSettings
    {
        public string ArticleCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IMongoDatabaseSettings
    {
        string ArticleCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
