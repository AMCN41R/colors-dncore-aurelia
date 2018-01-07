using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ColorsTest.Core.Colors;
using ColorsTest.Core.Repositories.People;
using ColorsTest.Infrastructure.Repositories.Colors;
using ColorsTest.Infrastructure.Repositories.People;
using ColorsTest.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ColorsTest
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IConnectionFactory, SqlConnectionFactory>();

            services.AddTransient<IPersonRepository, PersonRepository>();
            services.AddTransient<IColorRepository, ColorRepository>();
            services.AddTransient<IColorService, ColorService>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseMvc();
        }
    }
}
