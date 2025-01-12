using Microsoft.EntityFrameworkCore;
using Personal_Library.Models;

namespace Personal_Library
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // �]�m SQL Server ����Ʈw�s���r��
            var connectionString = builder.Configuration.GetConnectionString("PersonalBookCollectionDatabase");
            builder.Services.AddDbContext<PersonalBookCollectionContext>(options =>
                options.UseSqlServer(connectionString));


            // ���U Session �M��L�A��
            builder.Services.AddSession();
            builder.Services.AddControllersWithViews();
            builder.Services.AddHttpClient<ExternalApiService>(client =>
            {
                client.BaseAddress = new Uri("http://127.0.0.1:5050");
            });

            var app = builder.Build();

            // �t�m HTTP �ШD�޽u
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
