﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using HOLIDAY_WEB_API.Exceptions;
using HOLIDAY_WEB_API.Interfaces;
using Microsoft.IdentityModel.Tokens;


namespace HOLIDAY_WEB_API.Services;

public class AuthorizationUser : IAuthorizationUser
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IConfiguration _configuration;

    public AuthorizationUser(IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
    {
        _httpContextAccessor = httpContextAccessor;
        _configuration = configuration;
    }
    public string CreateJwt(string userId, string username, string email, string role)
    {
        JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

        string? jwtKey = _configuration["JwtKey"];
        if (jwtKey == null)
        {
            throw new ObjectNotFound(nameof(jwtKey));
        }

        string? jwtAudience = _configuration["JwtAudience"];
        if (jwtAudience == null)
        {
            throw new ObjectNotFound(nameof(jwtAudience));
        }

        string? jwtIssuer = _configuration["JwtIssuer"];
        if (jwtIssuer == null)
        {
            throw new ObjectNotFound(nameof(jwtIssuer));
        }

        string? cookieExpires = _configuration["CookieExpires"];
        if (cookieExpires == null)
        {
            throw new ObjectNotFound(nameof(cookieExpires));
        }

        bool success = double.TryParse(cookieExpires, out double cookieExpiresMinutes);
        if (!success)
        {
            throw new FormatException();
        }

        // Generate a secure key with a key size of 256 bits
        byte[] keyBytes = Encoding.UTF8.GetBytes(jwtKey);

        SecurityTokenDescriptor securityTokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                    new (ClaimTypes.NameIdentifier, userId),
                    new (ClaimTypes.Name, username),
                    new (ClaimTypes.Email, email),
                    new (ClaimTypes.Role, role)
            }),
            Expires = DateTime.Now.AddMinutes(cookieExpiresMinutes),
            Audience = jwtAudience,
            Issuer = jwtIssuer,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)
        };

        SecurityToken token = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
        string tokenString = jwtSecurityTokenHandler.WriteToken(token);

        return tokenString;
    }

    public void ClearJwtCookie()
    {
        string? cookieName = _configuration["CookieName"];
        if (cookieName == null)
        {
            throw new ObjectNotFound(nameof(cookieName));
        }

        HttpContext? httpContext = _httpContextAccessor.HttpContext;
        if (httpContext == null)
        {
            throw new ObjectNotFound(typeof(HttpContext));
        }

        httpContext.Response.Cookies.Delete(cookieName);
    }

    public int GetUserIdFromClaims()
    {
        HttpContext? httpContext = _httpContextAccessor.HttpContext;
        if (httpContext == null)
        {
            throw new ObjectNotFound(typeof(HttpContext));
        }

        Claim? claim = httpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
        if (claim == null)
        {
            throw new ObjectNotFound(typeof(Claim));
        }

        bool success = int.TryParse(claim.Value, out int parsedId);
        if (!success)
        {
            throw new FormatException();
        }

        return parsedId;
    }
}