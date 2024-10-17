﻿using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBlog.Application.Dtos.ApiRequestDtos;
using WebBlog.Application.Exceptions;
using WebBlog.Application.ExternalServices;
using WebBlog.Infrastructure.Helpers;
using WebBlog.Infrastructure.Identity;
using static WebBlog.Application.Dtos.ApiRequestDtos.AuthDtos;

namespace WebBlog.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;
        public AuthService(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, IConfiguration configuration)
        {
            _signInManager = signInManager;
            _userManager = userManager; 
            _configuration = configuration;
        }
        public async Task<string> LoginAsync(LoginDto dto)
        {
            var user = await _userManager.FindByNameAsync(dto.UserName);
            if (user == null)
            {
                throw new UnauthorizeException("Username is not existed");
            }
            var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);
            if (result.Succeeded)
            {
               return HttpContextHelper.GenerateToken(user, _configuration);
            }
            else
            {
                throw new UnauthorizeException("Password is incorrect"); ;
            }
        }

        public Task<string> RegisterAsync(CreateUserRequest dto)
        {
            throw new NotImplementedException();
        }
    }
}