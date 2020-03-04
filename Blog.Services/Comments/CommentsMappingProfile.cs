using System;
using AutoMapper;
using DataAccessComment = Blog.DataAccess.Entities.Comment;

namespace Blog.Services.Comments
{
    public class CommentsMappingProfile : Profile
    {
        public CommentsMappingProfile()
        {
            CreateMap<DataAccessComment, Comment>();
            CreateMap<UpdateCommentRequest, DataAccessComment>().ForMember(x => x.CreatedDate, opt => opt.MapFrom(p => DateTime.UtcNow));
        }
    }
}