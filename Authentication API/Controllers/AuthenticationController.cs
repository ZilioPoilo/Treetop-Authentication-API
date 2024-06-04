﻿using Authentication_API.AppMapping;
using Authentication_API.Dtos;
using Authentication_API.Models;
using Authentication_API.Services;
using Authentication_API.Services.DataServices;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Authentication_API.Controllers
{
    [Route("api/authentication-api/v1")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private const string CookieKey = "RefreshToken";

        private readonly GuestService _guestService;
        private readonly IMapper _mapper;
        private readonly JwtService _jwtService;

        public AuthenticationController(GuestService service, IMapper mapper, JwtService jwtService)
        {
            _guestService = service;
            _mapper = mapper;
            _jwtService = jwtService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login()
        {
            return StatusCode(200);
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            Guest model = _mapper.Map<Guest>(dto);
            model.CreatePasswordHash(dto.Password);
            model = await _guestService.CreateAsync(model);

            GuestDto guestDto = new GuestDto()
            {
                JwtToken = _jwtService.GenerateAuthorizationToken(model.Id, "User"),
                UserId = model.Id
            };

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc).AddDays(30),
                Secure = true,
                SameSite = SameSiteMode.None
            };

            Response.Cookies.Append(CookieKey, guestDto.JwtToken, cookieOptions);

            return StatusCode(200, guestDto);
        }
    }
}
