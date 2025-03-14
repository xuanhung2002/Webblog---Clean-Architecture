﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBlog.Application.Dto;

namespace WebBlog.Application.Interfaces
{
    public interface IUserCacheService
    {
        Task<UserDto> GetUserFromCacheById(Guid id);
        Task<List<UserDto>> GetUserFromCacheByIds(List<Guid> ids);
        Task RefreshUserCache(Guid? id = null);
    }
}
