using System;
using System.Linq;
using System.Net.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.ResponseCompression;

namespace GuidLove.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddNewtonsoftJson();
            services.AddResponseCompression(opts =>
            {
                opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                    new[] { "application/octet-stream" });
            });

            services.AddScoped<HttpClient>(s =>
            {
                var navigationManager = s.GetRequiredService<NavigationManager>();
                return new HttpClient
                {
                    BaseAddress = new Uri(navigationManager.BaseUri)
                };
            });

            //services.AddRazorPages();
            //services.AddServerSideBlazor();
            //services.AddSingleton<WeatherForecastService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
            public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
            {
                app.UseResponseCompression();

                if (env.IsDevelopment())
                {
                    app.UseDeveloperExceptionPage();
                    app.UseBlazorDebugging();
                }

                app.UseClientSideBlazorFiles<GuidLove.Startup>();

                app.UseStaticFiles();

                app.UseRouting();

                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapDefaultControllerRoute();
                    endpoints.MapFallbackToPage("/_Host");
                });
            }

            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}
            //else
            //{
            //    app.UseExceptionHandler("/Error");
            //    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            //    app.UseHsts();
            //}

            //app.UseHttpsRedirection();
            //app.UseStaticFiles();

            //app.UseRouting();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapBlazorHub();
            //    endpoints.MapFallbackToPage("/_Host");
            //});
    }
}