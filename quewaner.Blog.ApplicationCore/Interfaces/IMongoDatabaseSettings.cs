using System;
using System.Collections.Generic;
using System.Text;

namespace quewaner.Blog.ApplicationCore.Interfaces
{
    public interface IMongoDatabaseSettings: IDatabaseSettings
    {
        string ArticleCollectionName { get; set; }
        string DatabaseName { get; set; }
    }
}
