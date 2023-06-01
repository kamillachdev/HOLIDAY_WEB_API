using HOLIDAY_WEB_API.data_access;
using HOLIDAY_WEB_API.Services;
using HOLIDAY_WEB_API.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using HOLIDAY_WEB_API.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;

namespace HOLIDAY_WEB_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add configuration
            builder.Configuration.AddJsonFile("appsettings.json");

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddHttpContextAccessor();
            builder.Services.AddDbContext<UserDbContext>();
            builder.Services.AddScoped<IUserServices, UserServices>();
            builder.Services.AddScoped<IRequestServices, RequestServices>();
            builder.Services.AddScoped<IAuthDL, AuthDL>();

            var provider = builder.Services.BuildServiceProvider();
            var configuration = provider.GetRequiredService<IConfiguration>();

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtKey"])),
                    ValidateIssuer = true,
                    ValidIssuer = configuration["JwtIssuer"],
                    ValidateAudience = true,
                    ValidAudience = configuration["JwtAudience"],
                };
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = ctx =>
                    {
                        string? cookieName = configuration["CookieName"];
                        if (cookieName == null)
                        {
                            throw new ObjectNotFound(nameof(cookieName));
                        }

                        var token = ctx.Request.Cookies[cookieName];
                        Console.WriteLine($"Token received: {token}");

                        if (!string.IsNullOrEmpty(token))
                        {
                            // Parse the token using JwtSecurityTokenHandler
                            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
                            var jwtSecurityToken = jwtSecurityTokenHandler.ReadJwtToken(token);

                            var algorithm = jwtSecurityToken.Header.Alg;

                            // Check if the algorithm is HMAC with SHA-256
                            if (algorithm.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                            {
                                var keyBytes = Convert.FromBase64String(configuration["JwtKey"]);
                                var securityKey = new SymmetricSecurityKey(keyBytes);

                                // Ensure the key size is at least 256 bits
                                if (securityKey.KeySize < 256)
                                {
                                    throw new ArgumentOutOfRangeException("The key size must be at least 256 bits.");
                                }
                            }
                        }

                        ctx.Token = token;

                        return Task.CompletedTask;
                    },
                    OnAuthenticationFailed = ctx =>
                    {
                        Console.WriteLine(ctx.Exception.ToString());
                        return Task.CompletedTask;
                    }
                };
            });

            builder.Services.AddScoped<IAuthorizationUser, AuthorizationUser>();

            builder.Services.AddCors(options =>
            {
                var frontendURL = configuration.GetValue<string>("frontend_url");

                options.AddDefaultPolicy(builder =>
                {
                    builder.WithOrigins(frontendURL)
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();
                });
            });


            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseRouting();

            app.UseCors();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
