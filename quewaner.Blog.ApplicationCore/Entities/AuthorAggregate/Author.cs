using quewaner.Blog.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace quewaner.Blog.ApplicationCore.Entities.AuthorAggregate
{
    /// <summary>
    /// 作者信息
    /// </summary>
    public class Author : BaseEntity<string>, IAggregateRoot
    {
        public Author(string name, string icon, string motto, List<PersonalHomePage> homePages)
        {

            Name = name;
            Icon = icon;
            Motto = motto;
            _personalHomePages.AddRange(homePages);
        }

        public string Name { get; set; }

        public string Icon { get; set; }

        public string Motto { get; set; }

        private List<PersonalHomePage> _personalHomePages = new List<PersonalHomePage>();

        public IReadOnlyCollection<PersonalHomePage> PersonalHomePages => _personalHomePages.AsReadOnly();
    }
}
