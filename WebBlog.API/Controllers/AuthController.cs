﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebBlog.Application.Dtos.ApiRequestDtos;
using WebBlog.Application.ExternalServices;
using static WebBlog.Application.Dtos.ApiRequestDtos.AuthDtos;

namespace WebBlog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var response = await _authService.LoginAsync(dto, IpAddress());
            return Ok(response);

        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(CreateUserRequest dto)
        {
            var token = await _authService.RegisterAsync(dto);
            return Ok(new { token = token });
        }
        // helper

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            var response = await _authService.RefreshTokenAsync(refreshToken, IpAddress());
            await SetTokenCookie(response.RefreshToken);
            return Ok(response);

        }
        private async Task SetTokenCookie(string token)
        {

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTimeOffset.UtcNow.AddDays(7)
            };
            Response.Cookies.Append("refreshToken", token, cookieOptions);
        }
        private string IpAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }

    }
}
