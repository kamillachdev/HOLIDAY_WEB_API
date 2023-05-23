using HOLIDAY_WEB_API.data_access;
using HOLIDAY_WEB_API.Services;
using Microsoft.EntityFrameworkCore;
using Classes;

namespace HOLIDAY_WEB_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<UserDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("UserConnection")));
            builder.Services.AddScoped<IUserServices, UserServices>();
            builder.Services.AddScoped<IRequestServices, RequestServices>();
            builder.Services.AddScoped<IAuthDL, AuthDL>();
            //builder.Services.AddDbContext<>

            var provider = builder.Services.BuildServiceProvider();
            var configuration = provider.GetRequiredService<IConfiguration>();  

            builder.Services.AddCors(options =>
            {
                var frontendURL = configuration.GetValue<string>("frontend_url");

                options.AddDefaultPolicy(builder =>
                {
                    builder.WithOrigins(frontendURL).AllowAnyMethod().AllowAnyHeader();
                });
            });
            
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}