using Janus.DAL;
using JanusWeb.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

namespace JanusWeb;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddRazorPages();
        builder.Services.AddServerSideBlazor();
        builder.Services.AddSingleton<WeatherForecastService>();
        builder.Services.AddTransient<VideoMergerService>(sp => new VideoMergerService(
            Path.Combine(Directory.GetCurrentDirectory(), @"SharedBinares\ffmpeg\ffmpeg.exe")));
        builder.Services.AddTransient<ScreenService>();
        builder.Services.AddTransient<AdSlotService>();
        builder.Services.AddSignalR();
        // builder.Services.AddTransient<JanusDbContext>();
        builder.Services.AddDbContext<JanusDbContext>(options =>
            options.UseSqlite(builder.Configuration.GetConnectionString("janusDB")));

        var app = builder.Build();

        var scope = app.Services.CreateScope();
        var janusDbContext = scope.ServiceProvider.GetRequiredService<JanusDbContext>();
        janusDbContext.Database.EnsureCreated();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseStaticFiles();

        if (!Path.Exists(Path.Combine(Directory.GetCurrentDirectory(), @"MyStaticFiles")))
            Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), @"MyStaticFiles"));

        app.UseFileServer(new FileServerOptions
        {
            FileProvider = new PhysicalFileProvider(
                Path.Combine(Directory.GetCurrentDirectory(), @"MyStaticFiles")),
            RequestPath = new PathString("/StaticFiles"),
            EnableDirectoryBrowsing = false
        });
        app.UseRouting();
        app.MapBlazorHub();
        app.MapHub<SocketHub>("ScreenSocket");
        app.MapFallbackToPage("/_Host");

        app.Run();
    }
}