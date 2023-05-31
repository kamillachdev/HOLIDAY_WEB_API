using HOLIDAY_WEB_API.data_access;
using HOLIDAY_WEB_API.Services;
using HOLIDAY_WEB_API.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using HOLIDAY_WEB_API.Exceptions;

namespace HOLIDAY_WEB_API
{

    /*tokeny, ciastka do implementacji:
     * https://github.com/arekbor/stockband-api/blob/master/Stockband.Api/Services/AuthorizationUser.cs
    */
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

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
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtKey"]!)),
                    ValidateIssuer = true,
                    ValidIssuer = builder.Configuration["JwtIssuer"],
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["JwtAudience"],
                };
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = ctx =>
                    {
                        string? cookieName = builder.Configuration["CookieName"];
                        if (cookieName == null)
                        {
                            throw new ObjectNotFound(nameof(cookieName));
                        }

                        ctx.Token = ctx.Request.Cookies[cookieName];
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
                    builder.WithOrigins(frontendURL).AllowAnyMethod().AllowAnyHeader();
                });
            });


            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors();

            app.UseAuthorization();
            app.UseAuthentication();


            app.MapControllers();

            app.Run();

        }
    }
}