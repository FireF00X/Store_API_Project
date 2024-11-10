using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using System.Net;
using System.Text;
using Talabat.API.CustomMiddleware;
using Talabat.API.Errors;
using Talabat.API.Helper;
using Talabat.Core.Entities.IdentityEntities;
using Talabat.Core.RepositoryInterfaces;
using Talabat.Core.ServicesInterfaces;
using Talabat.Repository.Data;
using Talabat.Repository.Data.IdentityData;
using Talabat.Repository.RepositoryLogics;
using Talabat.Service;

namespace Talabat.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<TalabatDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddDbContext<AppIdentityDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
            });
            builder.Services.AddSingleton<IConnectionMultiplexer>(config =>
            {
                var connection = builder.Configuration.GetConnectionString("Redis");
                return ConnectionMultiplexer.Connect(connection);
            });
            builder.Services.AddSingleton<ICashService,CashService>();
            builder.Services.AddAuthentication().AddJwtBearer("Bearer",
                options => 
                {
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:SecurityKey"]?? string.Empty)),
                        ClockSkew = TimeSpan.Zero,
                        ValidAudience= builder.Configuration.GetSection("JWT").GetSection("ValidAud").Value,
                        ValidIssuer = builder.Configuration["JWT:ValidIss"],
                    };
                }            
            );
            builder.Services.AddScoped(typeof(IUnitOfWork),typeof(UnitOfWork));
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IOrderingService, OrderingService>();
            builder.Services.AddScoped<IRedisRepository, RedisRepository>();
            builder.Services.AddScoped<IPaymentService,PaymentService>();
            builder.Services.AddIdentity<AppUser, IdentityRole>(options => { }).AddEntityFrameworkStores<AppIdentityDbContext>();
            builder.Services.AddAutoMapper(typeof(MappingProfiles));
            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (actionContext =>
                {
                    var errors = actionContext.ModelState.Where(p=>p.Value.Errors.Count()>0)
                                                         .SelectMany(p=>p.Value.Errors)
                                                         .Select(msg=>msg.ErrorMessage)
                                                         .ToList();
                    var response = new ApiValidationResponse
                    {
                        Errors = errors
                    };
                    return new BadRequestObjectResult(response);
                });
            });

            var app = builder.Build();

            #region auto Migrate
            var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var _dbContext = services.GetRequiredService<TalabatDbContext>();
            var _identityContext = services.GetRequiredService<AppIdentityDbContext>();
            var _userManger = services.GetRequiredService<UserManager<AppUser>>();
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            try
            {
                await _dbContext.Database.MigrateAsync();
                await StoreContextSeed.SeedAsync(_dbContext);
                await _identityContext.Database.MigrateAsync();
                await StoreIdentityContextSeed.SeedIdentityAsync(_userManger, _identityContext);
            }
            catch (Exception ex)
            {

                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "An Error Has Occured While Migrate to database");
            }
            finally
            {
                _dbContext.Dispose();
            }
            #endregion

            app.UseMiddleware<ExceptionMiddleware>();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStatusCodePagesWithReExecute("/ErrorPagesToGet/{0}");
            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
