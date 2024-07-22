using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using OnlineStoreManagement.API.Middleware;
using OnlineStoreManagement.Helpers;
using Serilog;
using Microsoft.OpenApi.Models;
using FluentValidation;
using FluentValidation.AspNetCore;
using OnlineStoreManagement.Validators;
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        // Add FluentValidation
        builder.Services.AddFluentValidationAutoValidation();
        builder.Services.AddValidatorsFromAssemblyContaining<ProductDTOValidator>();
        // Cấu hình Serilog
        //Thêm cấu hình Serilog để cung cấp logging cho ứng dụng.
        //Serilog sẽ ghi log vào console và file `app.log` trong thư mục `logs`.
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console()
            .WriteTo.File("logs/app.log", rollingInterval: RollingInterval.Day)
            .CreateLogger();
        // Add services to the container.

        // Thêm dịch vụ xác thực và ủy quyền sử dụng JWT.Các tham số xác thực được lấy từ file cấu hình.
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                ValidAudience = builder.Configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
            };
        });
        builder.Services.AddAuthorization();

        // Thêm các dịch vụ khác
        builder.Services.AddControllers(options =>
        {
            //Thêm ValidationFilterAttribute để xử lý validation cho các đầu vào.
            options.Filters.Add(new ValidationFilterAttribute());
        });
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            //Cấu hình Swagger để hỗ trợ tài liệu hóa API và xác thực sử dụng JWT.  
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Online Store Management API", Version = "v1" });
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter a valid token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "Bearer"
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }
            });
        });
        builder.Services.AddInfrastructure(builder.Configuration);

        var app = builder.Build();

        // Cấu hình pipeline
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        //Gọi app.UseAuthentication() để kích hoạt xác thực.
        app.UseAuthentication();
        //Sử dụng ExceptionMiddleware để xử lý các ngoại lệ.
        app.UseMiddleware<ExceptionMiddleware>();
        app.MapControllers();
        app.Run();
    }
}