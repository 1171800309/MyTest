using APITest1.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace APITest1
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
            services.AddControllers();

            services.AddMvc(options => {
                options.Filters.Add<ResponseResultFilter>();
                options.Filters.Add<ExceptionFilter>();
            }).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.Configure<ResponseCodeConfig>(Configuration.GetSection("ResponseCode"));
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, 
                              IWebHostEnvironment env,
                              IOptions<DefaultRegisterOption> config)
        { 
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            if (env.IsProduction())
            { 

                using (HttpClient client = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(config.Value.RegisterParams));
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    HttpResponseMessage message = client.PostAsync(config.Value.Server.Address, content).Result;
                }
            }

            app.UseEndpoints(endpoints =>
            { 

                endpoints.MapControllers();
            });
        }
        public class ServerOption
        {
            public string Address { get; set; }
        }

        public class RegisterParamas
        {
            public string Name { get; set; }

            public string Code { get; set; }

            public string Version { get; set; }

            public bool Is_Page { get; set; }

            public string Icon { get; set; }

            public List<Portal> Portals { get; set; }

            public string Descript { get; set; }

            public string Introducation { get; set; }
        }

        public class Portal
        {
            public string Name { get; set; }

            public string Version { get; set; }
        }

        public class DefaultRegisterOption
        {
            public RegisterParamas RegisterParams { get; set; }

            public ServerOption Server { get; set; }
        }
    }
}
