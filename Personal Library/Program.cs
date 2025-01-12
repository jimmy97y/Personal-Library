using Microsoft.EntityFrameworkCore;
using Personal_Library.Models;

namespace Personal_Library
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // 設置 SQL Server 的資料庫連接字串
            var connectionString = builder.Configuration.GetConnectionString("PersonalBookCollectionDatabase");
            builder.Services.AddDbContext<PersonalBookCollectionContext>(options =>
                options.UseSqlServer(connectionString));


            // 註冊 Session 和其他服務
            builder.Services.AddSession();
            builder.Services.AddControllersWithViews();
            builder.Services.AddHttpClient<ExternalApiService>(client =>
            {
                client.BaseAddress = new Uri("http://127.0.0.1:5050");
            });

            var app = builder.Build();

            // 配置 HTTP 請求管線
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseSession();
            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
