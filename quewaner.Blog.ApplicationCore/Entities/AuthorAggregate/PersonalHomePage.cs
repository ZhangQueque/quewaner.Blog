using System;
using System.Collections.Generic;
using System.Text;

namespace quewaner.Blog.ApplicationCore.Entities.AuthorAggregate
{
    public class PersonalHomePage  //ValueObject
    {
        public string Name { get; set; }
        public int Type { get; set; }
        public string HomePageUrl { get; set; }
    }
}
