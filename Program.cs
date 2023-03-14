using IndustryIncident;
using IndustryIncident.Helpers;
using IndustryIncident.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Formatting.Compact;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var settings = Settings.GetSettings(builder.Configuration);
        Log.Logger = new LoggerConfiguration()
               .Enrich.FromLogContext()
               .WriteTo.Console(new RenderedCompactJsonFormatter())
               .WriteTo.File(settings.logPath, rollingInterval: RollingInterval.Day)
               .CreateLogger();
        builder.Services.AddSingleton(Log.Logger);

        builder.Services.AddSingleton(settings);
        // Add services to the container.
        builder.Services.AddControllersWithViews();
        builder.Services.AddDbContext<IndustryIncidentContext>(options => options.UseSqlServer(settings.connectionString));
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