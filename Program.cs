using IndustryIncident.Helpers;
using IndustryIncident.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.EntityFrameworkCore;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();
        builder.Services.AddDbContext<IndustryIncidentContext>(options => options.UseSqlServer("Data Source=LAPTOP-RMHSDIOF;Initial Catalog=Industry_Incident;persist security info=True;Trust Server Certificate=true;User ID=TestLogin;Password=TestLogin"));
        builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme)
           .AddNegotiate();
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddAuthorization(options =>
        {
            // By default, all incoming requests will be authorized according to the default policy.
            options.FallbackPolicy = options.DefaultPolicy;
        });
        builder.Services.AddScoped<IClaimsTransformation, MyClaimsTransformation>();
/*        builder.Services.ConfigureApplicationCookie(options => options.LoginPath = "/home");
*/


        builder.Services.AddRazorPages();

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

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Incidents}/{action=Index}/{id?}");

        app.Run();
    }
}