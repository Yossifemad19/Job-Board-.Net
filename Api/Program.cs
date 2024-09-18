using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Services;
using LMS.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Text.Json.Serialization;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Api.DTOs.CompanyDtos;
using Api.DTOs.UserDtos;

namespace Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);



            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["jwt:SecretKey"])),
                        ValidIssuer = builder.Configuration["jwt:Issuer"],
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidAudience = builder.Configuration["jwt:Audience"],
                        ValidateLifetime=true,
                    });
            // Add services to the container.
            builder.Services.AddDbContext<ApplicationDbContext>(opt=>
                opt.UseSqlServer(builder.Configuration.GetConnectionString("default"))
            );
            builder.Services.AddScoped(typeof(IGenericRepository<,>),typeof(GenericRepository<,>));
            builder.Services.AddScoped<IUserRepository,UserRepository>();
            builder.Services.AddScoped<ICompanyRepository,CompanyRepository>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
            builder.Services.AddSingleton<ITokenService,TokenService>();
            builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

            builder.Services.AddControllers()
                .AddFluentValidation(f => {
                    f.RegisterValidatorsFromAssemblyContaining<CompanyDto>();
                    f.RegisterValidatorsFromAssemblyContaining<UserResiterDto>();
                })
                .AddJsonOptions(options=>
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
