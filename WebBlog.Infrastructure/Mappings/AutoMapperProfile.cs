﻿using AutoMapper;
using WebBlog.Application.Dto;
using WebBlog.Application.Dtos.CommentDtos;
using WebBlog.Domain.Entities;
using WebBlog.Infrastructure.Identity;

namespace WebBlog.Infrastructure.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Post, PostDto>().ReverseMap();

            CreateMap<CreateUserRequest, UserDto>();

            CreateMap<AppUser, UserDto>().ReverseMap();

            CreateMap<Comment, CommentDto>().ReverseMap();
        }
    }
}
