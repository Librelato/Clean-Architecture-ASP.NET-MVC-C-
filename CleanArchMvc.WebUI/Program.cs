using CleanArchMvc.Domain.Accounts;
using CleanArchMvc.Infra.Data.Identity;
using CleanArchMvc.Infra.IoC;
using Microsoft.CodeAnalysis.Host.Mef;
using Microsoft.Extensions.Configuration;

namespace CleanArchMvc.WebUI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddInfrastructure(builder.Configuration);
        builder.Services.AddControllersWithViews();
        
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

        SeedUserRoles(app);

        app.UseAuthentication(); //IMPORTANTE... autenticação vem antes da autorização.
        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }

    static void SeedUserRoles(IApplicationBuilder app)
    {
        using (var serviceScope = app.ApplicationServices.CreateScope())
        {
            var seed = serviceScope.ServiceProvider.GetService<ISeedUserRoleInitial>();
            seed.SeedRoles();
            seed.SeedUsers();
        }
    }
}