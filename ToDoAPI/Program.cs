using Microsoft.EntityFrameworkCore;

namespace ToDoAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.WithOrigins("OriginPolicy", "http://localhost:3000").AllowAnyMethod().AllowAnyHeader();
                });
            });

            // Add services to the container.
            builder.Services.AddDbContext<ToDoAPI.Models.ToDoContext>(
                options =>
                {
                    options.UseSqlServer(builder.Configuration.GetConnectionString("ToDoDB"));                    
                }
                );

            builder.Services.AddControllers();
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

            app.UseAuthorization();


            app.MapControllers();

            app.UseCors();

            app.Run();
        }
    }
}