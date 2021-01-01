using AutoMapper;
using quewaner.Blog.ApplicationCore.Entities.ArticleAggregate;
using quewaner.Blog.DataTransferObject.ArticleDto;
using quewaner.Blog.DataTransferObject.ArticleDtos;
using quewaner.Blog.DataTransferObject.ArticleTypeDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace quewaner.Blog.ApplicationCore.Profiles
{
    /// <summary>
    /// AutoMapper映射
    /// </summary>
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //从a转到b
            CreateMap<AddArticleDto, Article>();
            CreateMap<UpdateArticleDto, Article>();
            CreateMap<AddArticleTypeDto, ArticleType>();
            CreateMap(typeof(Article), typeof(ShowArticleDto));
            CreateMap<ArticleType,  ShowArticleTypeDto>();

        }
    }
}
