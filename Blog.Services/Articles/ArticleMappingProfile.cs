using System;

using AutoMapper;

using DataAccessArticle = Blog.DataAccess.Entities.Article;

namespace Blog.Services.Articles
{
    public class ArticleMappingProfile : Profile
    {
        public ArticleMappingProfile()
        {
            CreateMap<DataAccessArticle, Article>().ForMember(x => x.User, opt => opt.MapFrom(p => p.User));
            CreateMap<UpdateArticleRequest, DataAccessArticle>().ForMember(x => x.CreatedDate, opt => opt.MapFrom(p => DateTime.UtcNow));
        }
    }
}