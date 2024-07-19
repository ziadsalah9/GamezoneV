using GamezoneV.Data;
using GamezoneV.Services;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace GamezoneV
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var connectionstring = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("No connection string Was found");
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(connectionstring);
            });

            builder.Services.AddScoped<ICategoriesService,CategoriesService>();
            builder.Services.AddScoped<IDevicesService,DevicesService>();
            builder.Services.AddScoped<IGamesServices,GamesServices>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
