using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.FileProviders;

namespace PhotoUploader
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            //builder.Services.Configure<FormOptions>(o =>
            //{
            //    o.MultipartBodyLengthLimit = 5000000000;
            //});

            //builder.WebHost.ConfigureKestrel(o =>
            //{
            //    o.Limits.MaxRequestBodySize = null;
            //});

            //builder.Services.Configure<IISServerOptions>(o =>
            //{
            //    o.MaxRequestBodySize = null;
            //    o.MaxRequestBodyBufferSize = int.MaxValue;
            //});


            var app = builder.Build();

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads")),
                RequestPath = "/uploads"
            });

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
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