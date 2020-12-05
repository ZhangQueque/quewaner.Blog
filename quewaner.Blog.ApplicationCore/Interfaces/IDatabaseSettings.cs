using System;
using System.Collections.Generic;
using System.Text;

namespace quewaner.Blog.ApplicationCore.Interfaces
{
    public interface IDatabaseSettings
    {
        string ConnectionString { get; set; }
    }
}
