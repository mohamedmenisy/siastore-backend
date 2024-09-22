
using Microsoft.EntityFrameworkCore;
using System;
using e_commerce.Context;
using e_commerce.Interfaces;
using e_commerce.Repositories;
using Microsoft.Extensions.FileProviders;
using StackExchange.Redis;
using e_commerce.Models;
using Microsoft.AspNetCore.Identity;
using e_commerce.Email;
using e_commerce.Services;

namespace e_commerce
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            string txt = "Cors";

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddDbContext<StoreContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("StoreConnection")));
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<StoreContext>();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<IFilters, FiltersRepo>();
            builder.Services.AddScoped<IBrand, BrandRepo>();
            builder.Services.AddScoped<IType, TypeRepo>();
            builder.Services.AddScoped<IProduct, ProductRepo>();
            builder.Services.AddScoped<ICart, CartRepo>();
            builder.Services.AddScoped<IOrder,OrderRepo>();
            builder.Services.AddScoped<IFavorite,FavoriteRepo>();
            builder.Services.AddScoped<IPayment,PaymentRepo>();
            builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
            builder.Services.AddTransient<IMailingService, MailingService>();
            //add services cors here 
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(txt,
                builder =>
                {
                    builder.AllowAnyOrigin();
                    builder.AllowAnyMethod();
                    builder.AllowAnyHeader();
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")),
                RequestPath = "/wwwroot"
            });

            //allow meddilware cors here
            app.UseCors(txt);
            app.UseAuthentication();

            app.UseAuthorization();


            app.MapControllers();

            

            app.Run();
        }
    }
}
