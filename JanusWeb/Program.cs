using Janus.DAL;
using JanusWeb.Data;
using Microsoft.Extensions.FileProviders;

namespace JanusWeb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddSingleton<WeatherForecastService>();
            builder.Services.AddScoped<VideoMergerService>();
            builder.Services.AddScoped<ScreenService>();
            builder.Services.AddScoped<AdSlotService>();
            builder.Services.AddSignalR();
            builder.Services.AddDbContext<JanusDbContext>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseFileServer(new FileServerOptions()
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
            var context = app.Services.GetRequiredService<JanusDbContext>();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }
    }
}