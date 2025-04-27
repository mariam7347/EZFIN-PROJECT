
using Microsoft.EntityFrameworkCore;
using EZFIN_PROJECT.Data; // Use your updated namespace
using EZFIN_PROJECT.Model;
using Microsoft.AspNetCore.Identity;
using EZFIN_PROJECT.Services;

namespace EZFIN_PROJECT
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // ✅ Configure the connection to SQL Server
            builder.Services.AddDbContext<FinanceContext>(options =>
                  options.UseSqlServer("Server=DESKTOP-FAVJNN1;Database=FinanceDB;Trusted_Connection=True;TrustServerCertificate=True;"));
            builder.Services.AddDataProtection();
            // ✅ Add Identity (User authentication)
            builder.Services.AddIdentityCore<User>()
                .AddEntityFrameworkStores<FinanceContext>()
                .AddDefaultTokenProviders();

            // ✅ Add services to the container
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(); // Optional: Swagger for testing APIs
            builder.Services.AddScoped<UserService>();  // Registering UserService

            var app = builder.Build();

            // ✅ Configure the HTTP request pipeline
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication(); // ✅ Important if you use Identity
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}