using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Mvc.Controllers;
using Unity;
using MicroServiceSample.nWebGraph;
using MicroServiceSample.nCustomDI;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using System.Text;

namespace MicroServiceSample
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration _Configuration)
        {
            Configuration = _Configuration;
        }



        public IServiceProvider ConfigureServices(IServiceCollection _Services)
        {
            _Services.AddMvc();

            _Services.Configure<KestrelServerOptions>(_Options =>
            {
                _Options.AllowSynchronousIO = true;
            });

            _Services.Configure<IISServerOptions>(_Options =>
            {
                _Options.AllowSynchronousIO = true;
            });

            IUnityContainer __DependencyContainer = new UnityContainer().EnableDiagnostic();


            __DependencyContainer.RegisterInstance<IConfiguration>(Configuration);
            __DependencyContainer.RegisterInstance<cEventGraph>(new cEventGraph());

            var __UnityServiceProvider = new cUnityServiceProvider(__DependencyContainer);

            _Services.AddSingleton<IControllerActivator>(new cUnityControllerActivator(__DependencyContainer));
            var __DefaultProvider = _Services.BuildServiceProvider();

            __DependencyContainer.AddExtension(new cUnityFallbackProviderExtension(__DefaultProvider));

            return __UnityServiceProvider;
        }




        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder _App, IWebHostEnvironment _Env)
        {

            if (_Env.IsDevelopment())
            {
                _App.UseDeveloperExceptionPage();
            }

            _App.UseHttpsRedirection();

            _App.UseRouting();


            _App.UseEndpoints(_Endpoints =>
            {
                _Endpoints.MapControllers();
                _Endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Microservice Started...");
                });
            });
        }
    }
}
