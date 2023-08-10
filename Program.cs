using IdentitySonProje.Data;
using IdentitySonProje.Entities;
using IdentitySonProje.Models.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IdentitySonProje
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("ConnStr");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

                //        builder.Services.AddDefaultIdentity<AppUser>(options => options.SignIn.RequireConfirmedAccount = true)
                //.AddEntityFrameworkStores<ApplicationDbContext>();
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            //Identity
            builder.Services.AddIdentity<AppUser, AppRole>(options => options.SignIn.RequireConfirmedAccount = false)
                            .AddRoles<AppRole>()
                            .AddEntityFrameworkStores<ApplicationDbContext>();
            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                  name: "areas",
                  pattern: "{area:exists}/{controller=Panel}/{action=Index}/{id?}"
                );
            });

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Hamburger}/{action=Index}/{id?}");
            app.MapRazorPages();

            //you must uncommend on first run
            //var scope = app.Services.CreateScope();
            //var userManagers = (UserManager<AppUser>)scope.ServiceProvider.GetService(typeof(UserManager<AppUser>));
            //ForLogin.AddSuperUserAsync(userManagers);


            app.Run();
        }
    }
}