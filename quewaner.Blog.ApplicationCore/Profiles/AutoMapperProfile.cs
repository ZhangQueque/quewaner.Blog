using AutoMapper;
using quewaner.Blog.ApplicationCore.Entities.ArticleAggregate;
using quewaner.Blog.DataTransferObject.ArticleDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace quewaner.Blog.ApplicationCore.Profiles
{
   public  class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AddArticleDto ,Article>();
        }
    }
}
